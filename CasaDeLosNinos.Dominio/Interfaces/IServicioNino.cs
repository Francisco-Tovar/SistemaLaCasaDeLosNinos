using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Contrato de caso de uso para la gestión de niños beneficiarios.
/// Vive en Dominio para que Interfaz lo consuma sin acoplarse a la implementación.
/// </summary>
public interface IServicioNino
{
    /// <summary>Retorna todos los niños (activos e inactivos).</summary>
    Task<IEnumerable<Nino>> ObtenerTodosAsync();

    /// <summary>Retorna únicamente los niños activos.</summary>
    Task<IEnumerable<Nino>> ObtenerActivosAsync();

    /// <summary>
    /// Guarda un niño (insertar si Id == 0, actualizar si Id > 0).
    /// </summary>
    Task<(bool Exito, string Mensaje)> GuardarAsync(Nino nino, int idUsuario);

    /// <summary>Activa o desactiva un niño (borrado lógico reversible).</summary>
    Task CambiarEstadoAsync(int id, bool activo, int idUsuario);
}
