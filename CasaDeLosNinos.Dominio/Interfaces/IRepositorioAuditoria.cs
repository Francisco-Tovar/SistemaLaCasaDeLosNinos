using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioAuditoria
{
    /// <summary>Inserta un nuevo registro de auditoría.</summary>
    Task<int> InsertarAsync(AuditoriaSistema auditoria);

    /// <summary>Obtiene los registros más recientes (paginación simple).</summary>
    Task<IEnumerable<AuditoriaSistema>> ObtenerUltimosAsync(int limite = 100);

    /// <summary>Filtra la bitácora por criterios opcionales.</summary>
    Task<IEnumerable<AuditoriaSistema>> FiltrarAsync(DateTime desde, DateTime hasta, string? modulo = null, string? accion = null);

    /// <summary>Elimina registros antiguos para mantenimiento.</summary>
    Task<int> LimpiarHistorialAsync(DateTime antesDe);
}
