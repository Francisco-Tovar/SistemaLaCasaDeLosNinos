using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

/// <summary>
/// Implementación del repositorio de Nino usando Dapper sobre SQLite.
/// Es la única clase autorizada para escribir SQL relacionado con la tabla Ninos.
/// </summary>
public class RepositorioNino : IRepositorioNino
{
    private readonly string _cadenaConexion;

    public RepositorioNino(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'BaseDatos' no está definida en appsettings.json.");
    }

    public async Task<IEnumerable<Nino>> ObtenerTodosAsync()
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT Id, NombreCompleto, FechaNacimiento, Genero, Direccion,
                   NombreEncargado, TelefonoEncargado, FechaIngreso, Activo, FechaCreacion
            FROM Ninos
            ORDER BY NombreCompleto ASC;";
        return await conexion.QueryAsync<Nino>(sql);
    }

    public async Task<IEnumerable<Nino>> ObtenerActivosAsync()
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT Id, NombreCompleto, FechaNacimiento, Genero, Direccion,
                   NombreEncargado, TelefonoEncargado, FechaIngreso, Activo, FechaCreacion
            FROM Ninos
            WHERE Activo = 1
            ORDER BY NombreCompleto ASC;";
        return await conexion.QueryAsync<Nino>(sql);
    }

    public async Task<Nino?> ObtenerPorIdAsync(int id)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM Ninos WHERE Id = @Id;";
        return await conexion.QueryFirstOrDefaultAsync<Nino>(sql, new { Id = id });
    }

    public async Task<int> InsertarAsync(Nino nino)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO Ninos (NombreCompleto, FechaNacimiento, Genero, Direccion,
                               NombreEncargado, TelefonoEncargado, FechaIngreso, Activo, FechaCreacion)
            VALUES (@NombreCompleto, @FechaNacimiento, @Genero, @Direccion,
                    @NombreEncargado, @TelefonoEncargado, @FechaIngreso, @Activo, @FechaCreacion);
            SELECT last_insert_rowid();";
        return await conexion.ExecuteScalarAsync<int>(sql, nino);
    }

    public async Task ActualizarAsync(Nino nino)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            UPDATE Ninos SET
                NombreCompleto    = @NombreCompleto,
                FechaNacimiento   = @FechaNacimiento,
                Genero            = @Genero,
                Direccion         = @Direccion,
                NombreEncargado   = @NombreEncargado,
                TelefonoEncargado = @TelefonoEncargado,
                FechaIngreso      = @FechaIngreso
            WHERE Id = @Id;";
        await conexion.ExecuteAsync(sql, nino);
    }

    public async Task CambiarEstadoAsync(int id, bool activo)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "UPDATE Ninos SET Activo = @Activo WHERE Id = @Id;";
        await conexion.ExecuteAsync(sql, new { Activo = activo ? 1 : 0, Id = id });
    }
}
