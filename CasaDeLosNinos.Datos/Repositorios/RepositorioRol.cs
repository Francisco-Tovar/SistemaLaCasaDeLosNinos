using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioRol : IRepositorioRol
{
    private readonly string _cadenaConexion;

    public RepositorioRol(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos") 
            ?? throw new InvalidOperationException("Cadena de conexión no encontrada.");
    }

    public async Task<IEnumerable<Rol>> ObtenerTodosAsync()
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM Roles";
        return await conexion.QueryAsync<Rol>(sql);
    }

    public async Task<Rol?> ObtenerPorIdAsync(int id)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM Roles WHERE Id = @id";
        return await conexion.QueryFirstOrDefaultAsync<Rol>(sql, new { id });
    }
}
