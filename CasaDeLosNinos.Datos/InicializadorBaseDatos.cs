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
    private readonly string _cadenaConexion;

    public InicializadorBaseDatos(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'BaseDatos' no está definida en appsettings.json.");
    }

    public async Task InicializarAsync()
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        await conexion.OpenAsync();

        // Habilitar claves foráneas en SQLite (desactivadas por defecto)
        await conexion.ExecuteAsync("PRAGMA foreign_keys = ON;");

        await CrearTablasAsync(conexion);
        await InsertarDatosInicialesAsync(conexion);
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
                Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                IdNino      INTEGER NOT NULL REFERENCES Ninos(Id),
                Fecha       TEXT    NOT NULL,
                Presente    INTEGER NOT NULL DEFAULT 0,
                Observacion TEXT    NOT NULL DEFAULT '',
                IdUsuario   INTEGER NOT NULL REFERENCES Usuarios(Id)
            );");

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
