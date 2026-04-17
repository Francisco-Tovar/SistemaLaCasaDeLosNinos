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
public class InicializadorBaseDatos : IInicializadorBaseDatos
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

    private static async Task CrearTablaFotosAsync(SqliteConnection conexion)
    {
        // Esta tabla es 1:1 con Ninos. El ID es el mismo.
        await conexion.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS FotosBeneficiarios (
                IdNino          INTEGER PRIMARY KEY,
                Imagen          BLOB    NOT NULL,
                FechaActualizacion TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP
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

        // ── Ninos ─────────────────────────────────────────────────
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
                FechaCreacion       TEXT    NOT NULL DEFAULT CURRENT_TIMESTAMP
            );");

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
                Correo          TEXT    NOT NULL DEFAULT '',
                Telefono        TEXT    NOT NULL DEFAULT '',
                Especialidad    TEXT    NOT NULL DEFAULT '',
                Activo          INTEGER NOT NULL DEFAULT 1,
                FechaIngreso    TEXT    NOT NULL DEFAULT CURRENT_DATE
            );");

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
                IdUsuario       INTEGER NOT NULL REFERENCES Usuarios(Id)
            );");
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
    }
}
