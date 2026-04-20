using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

/// <summary>
/// Implementación del repositorio de Asistencia usando Dapper sobre SQLite.
/// El guardado masivo usa una transacción explícita para garantizar atomicidad.
/// INSERT OR REPLACE explota el índice único (IdNino, Fecha) para upsert seguro.
/// </summary>
public class RepositorioAsistencia : IRepositorioAsistencia
{
    private readonly string _cadenaConexion;

    public RepositorioAsistencia(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'BaseDatos' no está definida en appsettings.json.");
    }

    public async Task<bool> ExisteRegistroParaFechaAsync(DateTime fecha)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT COUNT(1) FROM Asistencia WHERE Fecha = @Fecha;";
        var cantidad = await conexion.ExecuteScalarAsync<int>(sql,
            new { Fecha = fecha.ToString("yyyy-MM-dd") });
        return cantidad > 0;
    }

    public async Task<IEnumerable<Asistencia>> ObtenerPorFechaAsync(DateTime fecha)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT 
                a.Id, a.IdNino, a.Fecha, a.Presente, a.IdUsuario, a.IdObservacion,
                o.Contenido AS ObservacionTexto
            FROM Asistencia a
            LEFT JOIN Observaciones o ON o.Id = a.IdObservacion
            WHERE a.Fecha = @Fecha;";
        
        return await conexion.QueryAsync<Asistencia>(sql,
            new { Fecha = fecha.ToString("yyyy-MM-dd") });
    }

    public async Task GuardarAsistenciaMasivaAsync(IEnumerable<Asistencia> registros)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        await conexion.OpenAsync();

        await using var transaccion = await conexion.BeginTransactionAsync();
        try
        {
            const string sql = @"
                INSERT OR REPLACE INTO Asistencia (IdNino, Fecha, Presente, IdObservacion, IdUsuario)
                VALUES (@IdNino, @Fecha, @Presente, @IdObservacion, @IdUsuario);";

            foreach (var registro in registros)
            {
                await conexion.ExecuteAsync(sql, new
                {
                    registro.IdNino,
                    Fecha      = registro.Fecha.ToString("yyyy-MM-dd"),
                    Presente   = registro.Presente ? 1 : 0,
                    registro.IdObservacion,
                    registro.IdUsuario
                }, transaccion);
            }

            await transaccion.CommitAsync();
        }
        catch
        {
            await transaccion.RollbackAsync();
            throw; 
        }
    }

    public async Task<IEnumerable<Asistencia>> ObtenerPorMesAsync(int anio, int mes)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT Id, IdNino, Fecha, Presente, IdUsuario, IdObservacion
            FROM Asistencia
            WHERE strftime('%Y', Fecha) = @Anio AND strftime('%m', Fecha) = @Mes;";
        
        return await conexion.QueryAsync<Asistencia>(sql, new 
        { 
            Anio = anio.ToString(), 
            Mes = mes.ToString("D2") 
        });
    }
}
