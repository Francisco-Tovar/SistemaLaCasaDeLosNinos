using CasaDeLosNinos.Dominio.Dtos;

namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Contrato de servicio para el módulo de Seguimiento Cualitativo.
/// Orquesta la regla de negocio: FechaHora del sistema, IdUsuario de la sesión.
/// </summary>
public interface IServicioObservacion
{
    /// <summary>
    /// Retorna el historial de observaciones de un niño (más reciente primero).
    /// </summary>
    Task<IEnumerable<ObservacionDetalleDto>> ObtenerHistorialAsync(int idNino);

    /// <summary>Registra una nueva observación y retorna el ID.</summary>
    Task<int> RegistrarAsync(int idNino, int idUsuarioSesion, string contenido, DateTime? fechaManual = null);

    /// <summary>Actualiza una observación existente.</summary>
    Task ActualizarAsync(int id, string contenido);

    /// <summary>Elimina una observación.</summary>
    Task EliminarAsync(int id);
}
