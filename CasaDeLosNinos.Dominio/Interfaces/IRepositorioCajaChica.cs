using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Dtos;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioCajaChica
{
    // Métodos Operativos
    Task<int> CrearAsync(CajaChica movimiento);
    Task<bool> ActualizarAsync(CajaChica movimiento);
    Task<IEnumerable<CajaChica>> ObtenerPorMesAsync(int anio, int mes);
    Task<decimal> ObtenerSaldoMensualAsync(int anio, int mes);
    Task<CajaChica?> ObtenerPorIdAsync(int id);

    // Métodos de Auditoría Inmutable
    Task InsertarAuditoriaAsync(AuditoriaCajaChica auditoria);
    Task<IEnumerable<AuditoriaCajaChica>> ObtenerAuditoriasPorMovimientoAsync(int idMovimiento);
    Task<IEnumerable<AuditoriaDetalleDTO>> ObtenerAuditoriasDetalladasPorMesAsync(int anio, int mes);
}
