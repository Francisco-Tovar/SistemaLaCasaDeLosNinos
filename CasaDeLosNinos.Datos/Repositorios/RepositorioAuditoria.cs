using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioAuditoria : IRepositorioAuditoria
{
    private readonly string _cadenaConexion;

    public RepositorioAuditoria(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos") 
            ?? throw new InvalidOperationException("Cadena de conexión no encontrada.");
    }

    public async Task<int> InsertarAsync(AuditoriaSistema auditoria)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO AuditoriaSistema (FechaHora, IdUsuario, NombreUsuario, Modulo, Accion, Detalle)
            VALUES (@FechaHora, @IdUsuario, @NombreUsuario, @Modulo, @Accion, @Detalle);
            SELECT last_insert_rowid();";
        
        return await conexion.ExecuteScalarAsync<int>(sql, auditoria);
    }

    public async Task<IEnumerable<AuditoriaSistema>> ObtenerUltimosAsync(int limite = 100)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM AuditoriaSistema ORDER BY FechaHora DESC LIMIT @limite";
        return await conexion.QueryAsync<AuditoriaSistema>(sql, new { limite });
    }

    public async Task<IEnumerable<AuditoriaSistema>> FiltrarAsync(DateTime desde, DateTime hasta, string? modulo = null, string? accion = null)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        var sql = "SELECT * FROM AuditoriaSistema WHERE FechaHora BETWEEN @desde AND @hasta";
        
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("@desde", desde.ToString("yyyy-MM-dd HH:mm:ss"));
        dynamicParameters.Add("@hasta", hasta.ToString("yyyy-MM-dd HH:mm:ss"));

        if (!string.IsNullOrEmpty(modulo))
        {
            sql += " AND Modulo = @modulo";
            dynamicParameters.Add("@modulo", modulo);
        }

        if (!string.IsNullOrEmpty(accion))
        {
            sql += " AND Accion = @accion";
            dynamicParameters.Add("@accion", accion);
        }

        sql += " ORDER BY FechaHora DESC";
        
        return await conexion.QueryAsync<AuditoriaSistema>(sql, dynamicParameters);
    }

    public async Task<int> LimpiarHistorialAsync(DateTime antesDe)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "DELETE FROM AuditoriaSistema WHERE FechaHora < @antesDe";
        return await conexion.ExecuteAsync(sql, new { antesDe = antesDe.ToString("yyyy-MM-dd HH:mm:ss") });
    }
}
