using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Contrato de persistencia para la entidad Observacion.
/// Solo la capa Datos implementa este contrato.
/// </summary>
public interface IRepositorioObservacion
{
    /// <summary>
    /// Obtiene el historial completo de observaciones de un niño,
    /// enriquecido con el nombre del autor (JOIN con Usuarios).
    /// Orden: más reciente primero.
    /// </summary>
    Task<IEnumerable<ObservacionDetalleDto>> ObtenerPorNinoAsync(int idNino);

    /// <summary>
    /// Inserta una nueva observación. FechaHora se asigna en el servicio,
    /// nunca en la UI.
    /// </summary>
    Task InsertarAsync(Observacion observacion);
}
