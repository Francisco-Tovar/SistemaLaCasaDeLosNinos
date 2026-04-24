using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioBitacoraEvento : IRepositorioBitacoraEvento
{
    private readonly string _cadenaConexion;

    public RepositorioBitacoraEvento(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException("Falta 'BaseDatos' en appsettings.json.");
    }

    public async Task<int> AgregarAsync(BitacoraEvento evento)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO BitacoraEventos (Fecha, Titulo, Descripcion, IdUsuario, FechaCreacion)
            VALUES (@Fecha, @Titulo, @Descripcion, @IdUsuario, @FechaCreacion);
            SELECT last_insert_rowid();";
        
        return await conexion.QuerySingleAsync<int>(sql, new
        {
            Fecha = evento.Fecha.ToString("yyyy-MM-dd"),
            evento.Titulo,
            evento.Descripcion,
            evento.IdUsuario,
            FechaCreacion = evento.FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss")
        });
    }

    public async Task ActualizarAsync(BitacoraEvento evento)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            UPDATE BitacoraEventos 
            SET Fecha = @Fecha, Titulo = @Titulo, Descripcion = @Descripcion
            WHERE Id = @Id;";
        
        await conexion.ExecuteAsync(sql, new
        {
            Fecha = evento.Fecha.ToString("yyyy-MM-dd"),
            evento.Titulo,
            evento.Descripcion,
            evento.Id
        });
    }

    public async Task EliminarAsync(int id)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        await conexion.ExecuteAsync("DELETE FROM BitacoraEventos WHERE Id = @id;", new { id });
    }

    public async Task<BitacoraEvento?> ObtenerPorIdAsync(int id)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT b.*, u.NombreCompleto as NombreUsuario
            FROM BitacoraEventos b
            INNER JOIN Usuarios u ON b.IdUsuario = u.Id
            WHERE b.Id = @id;";
        
        return await conexion.QueryFirstOrDefaultAsync<BitacoraEvento>(sql, new { id });
    }

    public async Task<IEnumerable<BitacoraEvento>> ObtenerTodosAsync()
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT b.*, u.NombreCompleto as NombreUsuario
            FROM BitacoraEventos b
            INNER JOIN Usuarios u ON b.IdUsuario = u.Id
            ORDER BY b.Fecha DESC, b.Id DESC;";
        
        return await conexion.QueryAsync<BitacoraEvento>(sql);
    }

    public async Task<IEnumerable<BitacoraEvento>> ObtenerPorRangoFechaAsync(DateTime inicio, DateTime fin)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT b.*, u.NombreCompleto as NombreUsuario
            FROM BitacoraEventos b
            INNER JOIN Usuarios u ON b.IdUsuario = u.Id
            WHERE b.Fecha BETWEEN @inicio AND @fin
            ORDER BY b.Fecha ASC, b.Id ASC;";
        
        return await conexion.QueryAsync<BitacoraEvento>(sql, new
        {
            inicio = inicio.ToString("yyyy-MM-dd"),
            fin = fin.ToString("yyyy-MM-dd")
        });
    }
}
