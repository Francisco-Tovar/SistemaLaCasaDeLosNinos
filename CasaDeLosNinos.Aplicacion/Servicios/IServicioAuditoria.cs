using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public interface IServicioAuditoria
{
    /// <summary>Registra una acción de negocio en la bitácora.</summary>
    Task RegistrarAccionAsync(int? idUsuario, string modulo, string accion, string detalle);

    /// <summary>Registra un error técnico en la bitácora.</summary>
    Task RegistrarErrorAsync(Exception ex, int? idUsuario = null, string? modulo = "Sistema");

    /// <summary>Consulta los últimos registros.</summary>
    Task<IEnumerable<AuditoriaSistema>> ObtenerUltimosAsync(int limite = 100);

    /// <summary>Consulta filtrada para reportes.</summary>
    Task<IEnumerable<AuditoriaSistema>> FiltrarAsync(DateTime desde, DateTime hasta, string? modulo = null, string? accion = null);

    /// <summary>Mantenimiento preventivo.</summary>
    Task LimpiarHistorialAsync(int diasAntiguedad = 90);
}
