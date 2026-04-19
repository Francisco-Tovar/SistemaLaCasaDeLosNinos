using System.Data;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioCajaChica : IRepositorioCajaChica
{
    private readonly string _cadenaConexion;

    public RepositorioCajaChica(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos") 
                          ?? "Data Source=casaninos.db";
    }

    public async Task<int> CrearAsync(CajaChica movimiento)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO CajaChica (Fecha, Concepto, Monto, TipoMovimiento, IdUsuario, IdFotoRecibo)
            VALUES (@Fecha, @Concepto, @Monto, @TipoMovimiento, @IdUsuario, @IdFotoRecibo);
            SELECT last_insert_rowid();";
            
        return await conexion.ExecuteScalarAsync<int>(sql, movimiento);
    }

    public async Task<bool> ActualizarAsync(CajaChica movimiento)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            UPDATE CajaChica 
            SET Fecha = @Fecha, 
                Concepto = @Concepto, 
                Monto = @Monto, 
                TipoMovimiento = @TipoMovimiento, 
                IdFotoRecibo = @IdFotoRecibo
            WHERE Id = @Id;";
            
        var afectados = await conexion.ExecuteAsync(sql, movimiento);
        return afectados > 0;
    }

    public async Task<IEnumerable<CajaChica>> ObtenerPorMesAsync(int anio, int mes)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT * FROM CajaChica 
            WHERE strftime('%Y', Fecha) = @Anio 
              AND strftime('%m', Fecha) = @Mes 
            ORDER BY Fecha DESC, Id DESC;";
            
        return await conexion.QueryAsync<CajaChica>(sql, new 
        { 
            Anio = anio.ToString("D4"), 
            Mes = mes.ToString("D2") 
        });
    }

    public async Task<decimal> ObtenerSaldoMensualAsync(int anio, int mes)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT 
                COALESCE(SUM(CASE WHEN TipoMovimiento = 'Ingreso' THEN Monto ELSE 0 END), 0) -
                COALESCE(SUM(CASE WHEN TipoMovimiento = 'Egreso' THEN Monto ELSE 0 END), 0)
            FROM CajaChica 
            WHERE strftime('%Y', Fecha) = @Anio 
              AND strftime('%m', Fecha) = @Mes;";
              
        return await conexion.ExecuteScalarAsync<decimal>(sql, new 
        { 
            Anio = anio.ToString("D4"), 
            Mes = mes.ToString("D2") 
        });
    }

    public async Task<CajaChica?> ObtenerPorIdAsync(int id)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM CajaChica WHERE Id = @Id;";
        return await conexion.QuerySingleOrDefaultAsync<CajaChica>(sql, new { Id = id });
    }

    public async Task InsertarAuditoriaAsync(AuditoriaCajaChica auditoria)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO AuditoriaCajaChica (IdMovimiento, IdUsuario, FechaHoraCambio, DetallesDelCambio)
            VALUES (@IdMovimiento, @IdUsuario, @FechaHoraCambio, @DetallesDelCambio);";
            
        // Forzamos timestamp actual en C# 
        auditoria.FechaHoraCambio = DateTime.Now;
        await conexion.ExecuteAsync(sql, auditoria);
    }

    public async Task<IEnumerable<AuditoriaCajaChica>> ObtenerAuditoriasPorMovimientoAsync(int idMovimiento)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT * FROM AuditoriaCajaChica 
            WHERE IdMovimiento = @IdMovimiento 
            ORDER BY FechaHoraCambio DESC;";
            
        return await conexion.QueryAsync<AuditoriaCajaChica>(sql, new { IdMovimiento = idMovimiento });
    }

    public async Task<IEnumerable<AuditoriaDetalleDTO>> ObtenerAuditoriasDetalladasPorMesAsync(int anio, int mes)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            SELECT 
                a.IdMovimiento,
                a.FechaHoraCambio, 
                c.Concepto as ConceptoOriginal, 
                a.DetallesDelCambio, 
                u.NombreCompleto as Usuario
            FROM AuditoriaCajaChica a
            INNER JOIN CajaChica c ON a.IdMovimiento = c.Id
            INNER JOIN Usuarios u ON a.IdUsuario = u.Id
            WHERE strftime('%Y', c.Fecha) = @Anio 
              AND strftime('%m', c.Fecha) = @Mes
            ORDER BY a.FechaHoraCambio DESC;";
            
        return await conexion.QueryAsync<AuditoriaDetalleDTO>(sql, new 
        { 
            Anio = anio.ToString("D4"), 
            Mes = mes.ToString("D2") 
        });
    }
}
