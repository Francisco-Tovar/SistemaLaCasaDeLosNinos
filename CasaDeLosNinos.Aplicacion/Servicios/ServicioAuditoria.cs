using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioAuditoria : IServicioAuditoria
{
    private readonly IRepositorioAuditoria _repositorioAuditoria;
    private readonly IRepositorioUsuario _repositorioUsuario;

    public ServicioAuditoria(IRepositorioAuditoria repositorioAuditoria, IRepositorioUsuario repositorioUsuario)
    {
        _repositorioAuditoria = repositorioAuditoria;
        _repositorioUsuario = repositorioUsuario;
    }

    public async Task RegistrarAccionAsync(int? idUsuario, string modulo, string accion, string detalle)
    {
        string nombreUsuario = "Sistema";
        if (idUsuario.HasValue)
        {
            var user = await _repositorioUsuario.ObtenerPorIdAsync(idUsuario.Value);
            if (user != null) nombreUsuario = user.NombreUsuario;
        }

        var auditoria = new AuditoriaSistema
        {
            FechaHora = DateTime.Now,
            IdUsuario = idUsuario,
            NombreUsuario = nombreUsuario,
            Modulo = modulo,
            Accion = accion,
            Detalle = detalle
        };

        await _repositorioAuditoria.InsertarAsync(auditoria);
    }

    public async Task RegistrarErrorAsync(Exception ex, int? idUsuario = null, string? modulo = "Sistema")
    {
        string nombreUsuario = "Sistema";
        if (idUsuario.HasValue)
        {
            var user = await _repositorioUsuario.ObtenerPorIdAsync(idUsuario.Value);
            if (user != null) nombreUsuario = user.NombreUsuario;
        }

        var auditoria = new AuditoriaSistema
        {
            FechaHora = DateTime.Now,
            IdUsuario = idUsuario,
            NombreUsuario = nombreUsuario,
            Modulo = modulo,
            Accion = "Error",
            Detalle = $"[{ex.GetType().Name}] {ex.Message} | StackTrace: {ex.StackTrace}"
        };

        await _repositorioAuditoria.InsertarAsync(auditoria);
    }

    public async Task<IEnumerable<AuditoriaSistema>> ObtenerUltimosAsync(int limite = 100)
    {
        return await _repositorioAuditoria.ObtenerUltimosAsync(limite);
    }

    public async Task<IEnumerable<AuditoriaSistema>> FiltrarAsync(DateTime desde, DateTime hasta, string? modulo = null, string? accion = null)
    {
        return await _repositorioAuditoria.FiltrarAsync(desde, hasta, modulo, accion);
    }

    public async Task LimpiarHistorialAsync(int diasAntiguedad = 90)
    {
        var fechaLimite = DateTime.Now.AddDays(-diasAntiguedad);
        await _repositorioAuditoria.LimpiarHistorialAsync(fechaLimite);
    }
}
