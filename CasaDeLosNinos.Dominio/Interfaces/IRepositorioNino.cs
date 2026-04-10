using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Contrato de persistencia para la entidad Nino.
/// Solo define operaciones; la implementación vive en la capa Datos.
/// </summary>
public interface IRepositorioNino
{
    /// <summary>Obtiene todos los niños (activos e inactivos) para la pantalla de gestión.</summary>
    Task<IEnumerable<Nino>> ObtenerTodosAsync();

    /// <summary>Obtiene solo los niños con Activo = 1, para la toma de asistencia.</summary>
    Task<IEnumerable<Nino>> ObtenerActivosAsync();

    /// <summary>Busca un niño por su Id. Retorna null si no existe.</summary>
    Task<Nino?> ObtenerPorIdAsync(int id);

    /// <summary>Inserta un nuevo niño y retorna el Id generado.</summary>
    Task<int> InsertarAsync(Nino nino);

    /// <summary>Actualiza los datos de un niño existente.</summary>
    Task ActualizarAsync(Nino nino);

    /// <summary>
    /// Cambia el estado lógico del niño (borrado lógico).
    /// Nunca elimina el registro físicamente.
    /// </summary>
    Task CambiarEstadoAsync(int id, bool activo);
}
