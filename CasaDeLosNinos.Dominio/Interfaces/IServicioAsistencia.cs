using CasaDeLosNinos.Dominio.Dtos;

namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Contrato de caso de uso para la toma de asistencia diaria masiva.
/// </summary>
public interface IServicioAsistencia
{
    /// <summary>
    /// Carga la lista de niños activos para una fecha dada.
    /// Si ya existen registros de asistencia para esa fecha, los hidrata (Presente = valor guardado).
    /// </summary>
    Task<IEnumerable<NinoAsistenciaDto>> ObtenerNinosParaAsistenciaAsync(DateTime fecha);

    /// <summary>
    /// Guarda la asistencia masiva del día en una transacción.
    /// Retorna (true, "resumen") si fue exitoso o (false, "motivo") si hay un error de validación.
    /// </summary>
    Task<(bool Exito, string Mensaje)> GuardarAsistenciaAsync(
        DateTime fecha,
        IEnumerable<NinoAsistenciaDto> lista,
        int idUsuarioActual);
}
