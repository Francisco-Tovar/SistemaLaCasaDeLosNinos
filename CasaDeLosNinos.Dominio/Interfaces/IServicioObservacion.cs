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

    /// <summary>
    /// Registra una nueva observación.
    /// El servicio asigna FechaHora = DateTime.Now automaticamente.
    /// El idUsuarioSesion se inyecta desde la UI sin que el usuario lo edite.
    /// </summary>
    Task RegistrarAsync(int idNino, int idUsuarioSesion, string contenido);
}
