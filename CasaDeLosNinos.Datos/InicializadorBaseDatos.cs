using System.IO;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos;

/// <summary>
/// Inicializa la base de datos SQLite creando el esquema completo 
/// (IF NOT EXISTS) y registrando la versión si es la primera ejecución.
/// Solo la capa Datos tiene acceso directo a SQLite y Dapper.
/// </summary>
public partial class InicializadorBaseDatos : IInicializadorBaseDatos
{
    private readonly string _cadenaConexionPrincipal;
    private readonly string _cadenaConexionFotos;

    public InicializadorBaseDatos(IConfiguration configuracion)
    {
        _cadenaConexionPrincipal = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException("Falta 'BaseDatos' en appsettings.json.");
            
        _cadenaConexionFotos = configuracion.GetConnectionString("BaseDatosFotos")
            ?? throw new InvalidOperationException("Falta 'BaseDatosFotos' en appsettings.json.");
    }

    public async Task InicializarAsync()
    {
        // Asegurar que el directorio de las bases de datos exista
        AsegurarDirectorioBaseDatos(_cadenaConexionPrincipal);
        AsegurarDirectorioBaseDatos(_cadenaConexionFotos);

        // 1. Inicializar Base de Datos Principal
        await using (var conexion = new SqliteConnection(_cadenaConexionPrincipal))
        {
            await conexion.OpenAsync();
            await conexion.ExecuteAsync("PRAGMA foreign_keys = ON;");
            await CrearTablasAsync(conexion);
            await RepararIntegridadBitacoraAsync(conexion);
            await InsertarDatosInicialesAsync(conexion);
        }

        // 2. Inicializar Base de Datos de Fotos (Separada)
        await using (var conexion = new SqliteConnection(_cadenaConexionFotos))
        {
            await conexion.OpenAsync();
            await CrearTablaFotosAsync(conexion);
        }
    }

    public async Task ReiniciarBaseDatosAsync()
    {
        await using (var conexion = new SqliteConnection(_cadenaConexionPrincipal))
        await using (var conexionFotos = new SqliteConnection(_cadenaConexionFotos))
        {
            await conexion.OpenAsync();
            await conexionFotos.OpenAsync();

            // 1. Desactivar FK antes de la transacción (SQLite requiere esto fuera de transacciones abiertas)
            await conexion.ExecuteAsync("PRAGMA foreign_keys = OFF;");

            using var transaction = conexion.BeginTransaction();
            try
            {
                // 2. Limpiar todas las tablas de negocio
                await conexion.ExecuteAsync("DELETE FROM Asistencia;", null, transaction);
                await conexion.ExecuteAsync("DELETE FROM Observaciones;", null, transaction);
                await conexion.ExecuteAsync("DELETE FROM Ninos;", null, transaction);
                await conexion.ExecuteAsync("DELETE FROM RegistroHoras;", null, transaction);
                await conexion.ExecuteAsync("DELETE FROM Voluntarios;", null, transaction);
                await conexion.ExecuteAsync("DELETE FROM AuditoriaCajaChica;", null, transaction);
                await conexion.ExecuteAsync("DELETE FROM CajaChica;", null, transaction);
                await conexion.ExecuteAsync("DELETE FROM AuditoriaSistema;", null, transaction);

                // 3. Limpiar permisos extra PRIMERO (para evitar violación de FK si no se desactivó correctamente)
                await conexion.ExecuteAsync("DELETE FROM PermisosModulo WHERE IdUsuario > 1;", null, transaction);

                // 4. Limpiar usuarios EXCEPTO el admin maestro
                await conexion.ExecuteAsync("DELETE FROM Usuarios WHERE Id > 1 AND NombreUsuario <> 'admin';", null, transaction);

                // 5. Resetear auto-incrementos
                string[] tablas = { "Asistencia", "Observaciones", "Ninos", "RegistroHoras", "Voluntarios", "CajaChica", "AuditoriaCajaChica", "Usuarios", "AuditoriaSistema", "PermisosModulo", "BitacoraEventos" };
                foreach (var tabla in tablas)
                {
                    await conexion.ExecuteAsync($"DELETE FROM sqlite_sequence WHERE name = '{tabla}';", null, transaction);
                }

                // 6. Limpiar fotos (Base de datos separada)
                await conexionFotos.ExecuteAsync("DELETE FROM FotosBeneficiarios;");
                await conexionFotos.ExecuteAsync("DELETE FROM FotosEventos;");
                await conexionFotos.ExecuteAsync("DELETE FROM sqlite_sequence WHERE name = 'FotosEventos';");

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                // 7. Reactivar siempre al terminar
                await conexion.ExecuteAsync("PRAGMA foreign_keys = ON;");
            }
        }
    }
    

    private void AsegurarDirectorioBaseDatos(string cadenaConexion)
    {
        try
        {
            var builder = new SqliteConnectionStringBuilder(cadenaConexion);
            string? dataSource = builder.DataSource;

            if (!string.IsNullOrEmpty(dataSource))
            {
                string? directorio = Path.GetDirectoryName(dataSource);
                if (!string.IsNullOrEmpty(directorio) && !Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }
            }
        }
        catch { /* Ignorar errores aquí */ }
    }

    private static async Task CrearTablaFotosAsync(SqliteConnection conexion)
    {
        // Esta tabla es 1:1 con Ninos. El ID es el mismo.
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS FotosBeneficiarios (
                IdNino          INTEGER PRIMARY KEY,
                Imagen          BLOB    NOT NULL,
                FechaActualizacion TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP
            );");

        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS FotosEventos (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                IdEvento        INTEGER NOT NULL,
                Imagen          BLOB    NOT NULL,
                FechaCreacion   TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP
            );");
    }

    /// <summary>
    /// Detecta si hay registros de asistencia compartiendo la misma observación
    /// (error de integridad) y los independiza creando clones de la nota.
    /// También añade un índice único para prevenir reaparición.
    /// </summary>
    private static async Task RepararIntegridadBitacoraAsync(SqliteConnection conexion)
    {
        // 1. Encontrar IDs de observación compartidos
        const string sqlDuplicados = @"
            SELECT IdObservacion 
            FROM Asistencia 
            WHERE IdObservacion IS NOT NULL 
            GROUP BY IdObservacion 
            HAVING COUNT(*) > 1;";

        var idsCompartidos = (await conexion.QueryAsync<int>(sqlDuplicados)).ToList();

        foreach (var idObs in idsCompartidos)
        {
            // Obtener todos los registros de asistencia que usan este ID, excepto el primero
            const string sqlMismos = @"
                SELECT Id, IdNino 
                FROM Asistencia 
                WHERE IdObservacion = @idObs 
                ORDER BY Id ASC;";
            
            var registros = (await conexion.QueryAsync(sqlMismos, new { idObs })).ToList();
            
            // Saltamos el primero (él se queda con el ID original)
            for (int i = 1; i < registros.Count; i++)
            {
                var reg = registros[i];
                
                // Clonar la observación en la tabla Observaciones
                const string sqlClonar = @"
                    INSERT INTO Observaciones (IdNino, IdUsuario, FechaHora, Contenido)
                    SELECT IdNino, IdUsuario, FechaHora, Contenido 
                    FROM Observaciones WHERE Id = @idObs;
                    SELECT last_insert_rowid();";
                
                int nuevoId = await conexion.QuerySingleAsync<int>(sqlClonar, new { idObs });

                // Vincular el registro de asistencia específico al nuevo ID clonado
                await conexion.ExecuteAsync(
                    "UPDATE Asistencia SET IdObservacion = @nuevoId WHERE Id = @idAsistencia;",
                    new { nuevoId, idAsistencia = reg.Id });
            }
        }

        // 2. Blindaje: Crear índice único para evitar que esto vuelva a pasar físicamente
        await conexion.ExecuteAsync(@"
            CREATE UNIQUE INDEX IF NOT EXISTS ux_asistencia_observacion_unica
            ON Asistencia(IdObservacion) 
            WHERE IdObservacion IS NOT NULL;");
    }

    // ══════════════════════════════════════════════════════════════
    // CREACIÓN DE TABLAS (idempotente — IF NOT EXISTS)
    // ══════════════════════════════════════════════════════════════

    private static async Task CrearTablasAsync(SqliteConnection conexion)
    {
        // ── VersionBD ─────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS VersionBD (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                NumeroVersion   TEXT    NOT NULL,
                FechaAplicacion TEXT    NOT NULL,
                Descripcion     TEXT    NOT NULL DEFAULT ''
            );");

        // ── Roles ─────────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Roles (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre      TEXT    NOT NULL UNIQUE,
                Descripcion TEXT    NOT NULL DEFAULT ''
            );");

        // ── Usuarios ──────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Usuarios (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                NombreCompleto  TEXT    NOT NULL,
                NombreUsuario   TEXT    NOT NULL UNIQUE,
                ContrasenaHash  TEXT    NOT NULL,
                IdRol           INTEGER NOT NULL REFERENCES Roles(Id),
                Activo          INTEGER NOT NULL DEFAULT 1,
                FechaCreacion   TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP
            );");

        // ── Ninos ─────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Ninos (
                Id                  INTEGER PRIMARY KEY AUTOINCREMENT,
                NombreCompleto      TEXT    NOT NULL,
                FechaNacimiento     TEXT,
                Genero              TEXT    NOT NULL DEFAULT '',
                Direccion           TEXT    NOT NULL DEFAULT '',
                NombreEncargado     TEXT    NOT NULL DEFAULT '',
                TelefonoEncargado   TEXT    NOT NULL DEFAULT '',
                FechaIngreso        TEXT    NOT NULL DEFAULT CURRENT_DATE,
                Activo              INTEGER NOT NULL DEFAULT 1,
                FechaCreacion       TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP,
                FechaBaja           TEXT    NULL
            );");

        // Migración: Agregar columna FechaBaja si no existe (para DBs existentes)
        try {
            await conexion.ExecuteAsync("ALTER TABLE Ninos ADD COLUMN FechaBaja TEXT NULL;");
        } catch { /* Ignorar si ya existe */ }

        // ── Asistencia ────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Asistencia (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                IdNino          INTEGER NOT NULL REFERENCES Ninos(Id),
                Fecha           TEXT    NOT NULL,
                Presente        INTEGER NOT NULL DEFAULT 0,
                IdObservacion   INTEGER REFERENCES Observaciones(Id),
                IdUsuario       INTEGER NOT NULL REFERENCES Usuarios(Id)
            );");

        // Migración: Agregar columna IdObservacion si no existe (para DBs existentes)
        try {
            await conexion.ExecuteAsync("ALTER TABLE Asistencia ADD COLUMN IdObservacion INTEGER REFERENCES Observaciones(Id);");
        } catch { /* Ignorar si ya existe */ }

        // Índice parcial: evita duplicados por niño/fecha
        await conexion.ExecuteAsync(@"
            CREATE UNIQUE INDEX IF NOT EXISTS ux_asistencia_nino_fecha
            ON Asistencia(IdNino, Fecha);");

        // ── Observaciones ─────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Observaciones (
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                IdNino      INTEGER NOT NULL REFERENCES Ninos(Id),
                IdUsuario   INTEGER NOT NULL REFERENCES Usuarios(Id),
                FechaHora   TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP,
                Contenido   TEXT    NOT NULL
            );");

        // ── Voluntarios ───────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS Voluntarios (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                NombreCompleto  TEXT    NOT NULL,
                Cedula          TEXT    NOT NULL DEFAULT '',
                Correo          TEXT    NOT NULL DEFAULT '',
                Telefono        TEXT    NOT NULL DEFAULT '',
                Especialidad    TEXT    NOT NULL DEFAULT '',
                Institucion     TEXT    NOT NULL DEFAULT '',
                ContactoSupervisor TEXT NOT NULL DEFAULT '',
                Activo          INTEGER NOT NULL DEFAULT 1,
                FechaIngreso    TEXT    NOT NULL DEFAULT CURRENT_DATE,
                FechaBaja       TEXT    NULL
            );");

        // Migración: Agregar columna FechaBaja a Voluntarios si no existe (para DBs existentes)
        try {
            await conexion.ExecuteAsync("ALTER TABLE Voluntarios ADD COLUMN FechaBaja TEXT NULL;");
        } catch { /* Ignorar si ya existe */ }

        // Migraciones: Agregar columnas si no existen
        try {
            await conexion.ExecuteAsync("ALTER TABLE Voluntarios ADD COLUMN Cedula TEXT NOT NULL DEFAULT '';");
        } catch { }

        try {
            await conexion.ExecuteAsync("ALTER TABLE Voluntarios ADD COLUMN Institucion TEXT NOT NULL DEFAULT '';");
        } catch { }

        try {
            await conexion.ExecuteAsync("ALTER TABLE Voluntarios ADD COLUMN ContactoSupervisor TEXT NOT NULL DEFAULT '';");
        } catch { }

        // ── RegistroHoras ─────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS RegistroHoras (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                IdVoluntario    INTEGER NOT NULL REFERENCES Voluntarios(Id),
                Fecha           TEXT    NOT NULL,
                HorasAportadas  REAL    NOT NULL CHECK(HorasAportadas > 0),
                Descripcion     TEXT    NOT NULL DEFAULT '',
                IdUsuario       INTEGER NOT NULL REFERENCES Usuarios(Id)
            );");

        // ── CajaChica ─────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS CajaChica (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                Fecha           TEXT    NOT NULL DEFAULT CURRENT_DATE,
                Concepto        TEXT    NOT NULL,
                Monto           REAL    NOT NULL CHECK(Monto > 0),
                TipoMovimiento  TEXT    NOT NULL 
                                    CHECK(TipoMovimiento IN ('Ingreso', 'Egreso')),
                IdUsuario       INTEGER NOT NULL REFERENCES Usuarios(Id),
                IdFotoRecibo    INTEGER NULL -- Nueva columna (referencia pseudo a casafotos.db)
            );");

        // Migración: Agregar columna IdFotoRecibo por requerimiento tardío
        try {
            await conexion.ExecuteAsync("ALTER TABLE CajaChica ADD COLUMN IdFotoRecibo INTEGER NULL;");
        } catch { /* Ignora error si ya existe */ }

        // ── AuditoriaCajaChica ────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS AuditoriaCajaChica (
                Id                  INTEGER PRIMARY KEY AUTOINCREMENT,
                IdMovimiento        INTEGER NOT NULL REFERENCES CajaChica(Id),
                IdUsuario           INTEGER NOT NULL REFERENCES Usuarios(Id),
                FechaHoraCambio     TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP,
                DetallesDelCambio   TEXT    NOT NULL
            );");
        // ── PermisosModulo ────────────────────────────────────────────────
        // Los 5 módulos configurables: Ninos, Asistencia, Voluntarios, CajaChica, Reportes.
        // GestionUsuarios y Mantenimiento son exclusivos del rol y no se almacenan aquí.
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS PermisosModulo (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                IdUsuario       INTEGER NOT NULL REFERENCES Usuarios(Id),
                NombreModulo    TEXT    NOT NULL,
                UNIQUE(IdUsuario, NombreModulo)
            );");

        // ── AuditoriaSistema ─────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS AuditoriaSistema (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                FechaHora       TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP,
                IdUsuario       INTEGER NULL,
                NombreUsuario   TEXT    NOT NULL DEFAULT 'Sistema',
                Modulo          TEXT    NOT NULL,
                Accion          TEXT    NOT NULL,
                Detalle         TEXT    NOT NULL
            );");

        // ── BitacoraEventos ─────────────────────────────────────────────
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS BitacoraEventos (
                Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                Fecha           TEXT    NOT NULL,
                Titulo          TEXT    NOT NULL,
                Descripcion     TEXT    NOT NULL,
                IdUsuario       INTEGER NOT NULL REFERENCES Usuarios(Id),
                FechaCreacion   TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP
            );");

        // Índice para búsquedas rápidas por fecha
        await conexion.ExecuteAsync(@"
            CREATE INDEX IF NOT EXISTS idx_auditoria_fechahora ON AuditoriaSistema(FechaHora);");
    }

    // ══════════════════════════════════════════════════════════════
    // DATOS INICIALES (solo si la BD está vacía)
    // ══════════════════════════════════════════════════════════════

    private static async Task InsertarDatosInicialesAsync(SqliteConnection conexion)
    {
        // Registrar versión 1.0.0 solo si no existe ninguna
        var versionExistente = await conexion.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM VersionBD;");

        if (versionExistente == 0)
        {
            await conexion.ExecuteAsync(@"
                INSERT INTO VersionBD (NumeroVersion, FechaAplicacion, Descripcion)
                VALUES (@NumeroVersion, @FechaAplicacion, @Descripcion);",
                new
                {
                    NumeroVersion   = "1.0.0",
                    FechaAplicacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Descripcion     = "Esquema inicial completo — 8 tablas del dominio."
                });
        }

        // Roles base (si no existen)
        var rolesExistentes = await conexion.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM Roles;");

        if (rolesExistentes == 0)
        {
            await conexion.ExecuteAsync(@"
                INSERT INTO Roles (Nombre, Descripcion) VALUES
                ('Administrador', 'Acceso total al sistema'),
                ('Funcionario',   'Registro de asistencia y observaciones');");
        }

        // Permisos del administrador maestro (Id=1) — todos los módulos configurables
        // Se insertan cada vez usando INSERT OR IGNORE para que sea idempotente en migraciones
        var adminExiste = await conexion.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM Usuarios WHERE Id = 1;");

        if (adminExiste > 0)
        {
            await conexion.ExecuteAsync(@"
                INSERT OR IGNORE INTO PermisosModulo (IdUsuario, NombreModulo) VALUES
                (1, 'Ninos'),
                (1, 'Asistencia'),
                (1, 'Voluntarios'),
                (1, 'CajaChica'),
                (1, 'Reportes'),
                (1, 'BitacoraEventos');");
        }

        // ── Migración de permisos para usuarios existentes ───────────────────
        // Para cada usuario sin ningún permiso registrado, otorgar los permisos
        // por defecto según su rol:
        //   - Administrador (IdRol=1): todos los 5 módulos
        //   - Funcionario   (IdRol=2): Niños + Asistencia
        // Usa INSERT OR IGNORE para ser idempotente y no pisar permisos ya configurados.
        var usuariosSinPermisos = await conexion.QueryAsync<(int Id, int IdRol)>(@"
            SELECT u.Id, u.IdRol
            FROM Usuarios u
            WHERE NOT EXISTS (
                SELECT 1 FROM PermisosModulo p WHERE p.IdUsuario = u.Id
            );");

        foreach (var (id, idRol) in usuariosSinPermisos)
        {
            if (idRol == 1)
            {
                // Administrador: acceso completo
                await conexion.ExecuteAsync(@"
                    INSERT OR IGNORE INTO PermisosModulo (IdUsuario, NombreModulo) VALUES
                    (@id, 'Ninos'),
                    (@id, 'Asistencia'),
                    (@id, 'Voluntarios'),
                    (@id, 'CajaChica'),
                    (@id, 'Reportes'),
                    (@id, 'BitacoraEventos');", new { id });
            }
            else
            {
                // Funcionario: solo Niños y Asistencia por defecto
                await conexion.ExecuteAsync(@"
                    INSERT OR IGNORE INTO PermisosModulo (IdUsuario, NombreModulo) VALUES
                    (@id, 'Ninos'),
                    (@id, 'Asistencia');", new { id });
            }
        }


    }
}
