using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

/// <summary>
/// Repositorio para la entidad Observacion usando Dapper + SQLite.
/// Único punto del sistema con acceso directo a la tabla Observaciones.
/// </summary>
public class RepositorioObservacion : IRepositorioObservacion
{
    private readonly string _cadenaConexion;

    public RepositorioObservacion(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException("Cadena de conexión 'BaseDatos' no definida.");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ObservacionDetalleDto>> ObtenerPorNinoAsync(int idNino)
    {
        const string sql = @"
            SELECT
                o.Id,
                o.IdNino,
                u.NombreCompleto  AS NombreAutor,
                o.FechaHora,
                o.Contenido
            FROM  Observaciones o
            JOIN  Usuarios      u ON u.Id = o.IdUsuario
            WHERE o.IdNino = @idNino
            ORDER BY o.FechaHora ASC;";

        await using var conexion = new SqliteConnection(_cadenaConexion);
        return await conexion.QueryAsync<ObservacionDetalleDto>(sql, new { idNino });
    }

    /// <inheritdoc/>
    public async Task<int> InsertarAsync(Observacion observacion)
    {
        const string sql = @"
            INSERT INTO Observaciones (IdNino, IdUsuario, FechaHora, Contenido)
            VALUES (@IdNino, @IdUsuario, @FechaHora, @Contenido);
            SELECT last_insert_rowid();";

        await using var conexion = new SqliteConnection(_cadenaConexion);
        return await conexion.QuerySingleAsync<int>(sql, observacion);
    }

    /// <inheritdoc/>
    public async Task ActualizarAsync(int id, string contenido)
    {
        const string sql = "UPDATE Observaciones SET Contenido = @contenido WHERE Id = @id;";
        await using var conexion = new SqliteConnection(_cadenaConexion);
        await conexion.ExecuteAsync(sql, new { id, contenido });
    }

    /// <inheritdoc/>
    public async Task EliminarAsync(int id)
    {
        const string sql = "DELETE FROM Observaciones WHERE Id = @id;";
        await using var conexion = new SqliteConnection(_cadenaConexion);
        await conexion.ExecuteAsync(sql, new { id });
    }
}
