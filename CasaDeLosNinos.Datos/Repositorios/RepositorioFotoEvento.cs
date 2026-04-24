using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioFotoEvento : IRepositorioFotoEvento
{
    private readonly string _cadenaConexion;

    public RepositorioFotoEvento(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatosFotos")
            ?? throw new InvalidOperationException("Falta 'BaseDatosFotos' en appsettings.json.");
    }

    public async Task AgregarAsync(FotoEvento foto)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO FotosEventos (IdEvento, Imagen, FechaCreacion)
            VALUES (@IdEvento, @Imagen, @FechaCreacion);";
        
        await conexion.ExecuteAsync(sql, new
        {
            foto.IdEvento,
            foto.Imagen,
            FechaCreacion = foto.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss")
        });
    }

    public async Task EliminarAsync(int id)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        await conexion.ExecuteAsync("DELETE FROM FotosEventos WHERE Id = @id;", new { id });
    }

    public async Task EliminarPorEventoAsync(int idEvento)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        await conexion.ExecuteAsync("DELETE FROM FotosEventos WHERE IdEvento = @idEvento;", new { idEvento });
    }

    public async Task<IEnumerable<FotoEvento>> ObtenerPorEventoAsync(int idEvento)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM FotosEventos WHERE IdEvento = @idEvento ORDER BY Id ASC;";
        return await conexion.QueryAsync<FotoEvento>(sql, new { idEvento });
    }

    public async Task<FotoEvento?> ObtenerPorIdAsync(int id)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        return await conexion.QueryFirstOrDefaultAsync<FotoEvento>("SELECT * FROM FotosEventos WHERE Id = @id;", new { id });
    }
}
