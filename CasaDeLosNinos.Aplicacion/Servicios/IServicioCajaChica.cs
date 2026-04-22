using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Dtos;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public interface IServicioCajaChica
{
    Task<IEnumerable<CajaChica>> ObtenerPorMesAsync(int anio, int mes);
    Task<decimal> ObtenerSaldoMensualAsync(int anio, int mes);
    Task<CajaChica?> ObtenerPorIdAsync(int id);
    
    // Registrar la creación original
    Task<int> RegistrarMovimientoAsync(CajaChica movimiento, int idUsuario);
    
    // Actualizar movimiento generando log inmutable
    Task<(bool Exito, string Mensaje)> ModificarMovimientoAsync(CajaChica movimientoEditado, int idUsuarioQueEdita);
    
    // Consultar el historial
    Task<IEnumerable<AuditoriaCajaChica>> ObtenerHistorialAuditoriaAsync(int idMovimiento);
    Task<IEnumerable<AuditoriaDetalleDTO>> ObtenerAuditoriaMensualAsync(int anio, int mes);
}
