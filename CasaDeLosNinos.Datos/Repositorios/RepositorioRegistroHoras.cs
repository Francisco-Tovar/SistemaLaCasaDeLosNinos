using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioRegistroHoras : IRepositorioRegistroHoras
{
    private readonly string _cadenaConexion;

    public RepositorioRegistroHoras(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException("Falta 'BaseDatos' en appsettings.json.");
    }

    public async Task<IEnumerable<RegistroHoras>> ObtenerPorVoluntarioAsync(int idVoluntario)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM RegistroHoras WHERE IdVoluntario = @IdVoluntario ORDER BY Fecha DESC;";
        return await conexion.QueryAsync<RegistroHoras>(sql, new { IdVoluntario = idVoluntario });
    }

    public async Task<int> CrearAsync(RegistroHoras registro)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO RegistroHoras (IdVoluntario, Fecha, HorasAportadas, Descripcion, IdUsuario)
            VALUES (@IdVoluntario, @Fecha, @HorasAportadas, @Descripcion, @IdUsuario);
            SELECT last_insert_rowid();";
            
        return await conexion.ExecuteScalarAsync<int>(sql, registro);
    }

    public async Task EliminarAsync(int id)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "DELETE FROM RegistroHoras WHERE Id = @Id;";
        await conexion.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<decimal> ObtenerTotalHorasVoluntarioAsync(int idVoluntario)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT COALESCE(SUM(HorasAportadas), 0) FROM RegistroHoras WHERE IdVoluntario = @IdVoluntario;";
        return await conexion.ExecuteScalarAsync<decimal>(sql, new { IdVoluntario = idVoluntario });
    }
}
