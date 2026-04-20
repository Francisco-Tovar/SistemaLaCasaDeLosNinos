using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Contrato de persistencia para la entidad Asistencia.
/// El método de guardado masivo usa una transacción interna.
/// </summary>
public interface IRepositorioAsistencia
{
    /// <summary>
    /// Verifica si ya existe al menos un registro de asistencia para la fecha dada.
    /// Se usa como primera barrera de validación en el servicio.
    /// </summary>
    Task<bool> ExisteRegistroParaFechaAsync(DateTime fecha);

    /// <summary>Obtiene todos los registros de asistencia de una fecha específica.</summary>
    Task<IEnumerable<Asistencia>> ObtenerPorFechaAsync(DateTime fecha);

    /// <summary>
    /// Guarda la lista completa de asistencia en una sola transacción.
    /// Usa INSERT OR REPLACE para ser idempotente sobre el índice único (IdNino, Fecha).
    /// </summary>
    Task GuardarAsistenciaMasivaAsync(IEnumerable<Asistencia> registros);

    /// <summary>Obtiene todos los registros de asistencia de un mes y año específicos.</summary>
    Task<IEnumerable<Asistencia>> ObtenerPorMesAsync(int anio, int mes);
}
