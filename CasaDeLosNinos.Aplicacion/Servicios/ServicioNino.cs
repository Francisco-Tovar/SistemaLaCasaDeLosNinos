using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

/// <summary>
/// Caso de uso: Gestión de niños beneficiarios.
/// Orquesta el repositorio y aplica validaciones de negocio antes de persistir.
/// </summary>
public class ServicioNino : IServicioNino
{
    private readonly IRepositorioNino _repositorioNino;
    private readonly IServicioAuditoria _servicioAuditoria;

    public ServicioNino(IRepositorioNino repositorioNino, IServicioAuditoria servicioAuditoria)
    {
        _repositorioNino = repositorioNino;
        _servicioAuditoria = servicioAuditoria;
    }

    public async Task<IEnumerable<Nino>> ObtenerTodosAsync()
        => await _repositorioNino.ObtenerTodosAsync();

    public async Task<IEnumerable<Nino>> ObtenerActivosAsync()
        => await _repositorioNino.ObtenerActivosAsync();

    public async Task<(bool Exito, string Mensaje)> GuardarAsync(Nino nino, int idUsuario)
    {
        // ── Validaciones de negocio ──────────────────────────────────
        if (string.IsNullOrWhiteSpace(nino.NombreCompleto))
            return (false, "El nombre completo del niño es obligatorio.");

        if (string.IsNullOrWhiteSpace(nino.NombreEncargado))
            return (false, "El nombre del encargado es obligatorio.");

        if (!string.IsNullOrWhiteSpace(nino.TelefonoEncargado) && nino.TelefonoEncargado.Length != 8)
            return (false, "El teléfono debe tener exactamente 8 dígitos.");

        if (nino.FechaNacimiento.HasValue && nino.FechaNacimiento.Value > DateTime.Today)
            return (false, "La fecha de nacimiento no puede ser una fecha futura.");

        // ── Persistencia ─────────────────────────────────────────────
        if (nino.Id == 0)
        {
            // Insertar nuevo
            nino.FechaCreacion = DateTime.Now;
            nino.FechaIngreso  = DateTime.Today;
            nino.Activo        = true;
            await _repositorioNino.InsertarAsync(nino);
            
            await _servicioAuditoria.RegistrarAccionAsync(idUsuario, "Niños", "Creación", 
                $"Se registró al niño: {nino.NombreCompleto}");
        }
        else
        {
            // Actualizar existente
            await _repositorioNino.ActualizarAsync(nino);
            
            await _servicioAuditoria.RegistrarAccionAsync(idUsuario, "Niños", "Modificación", 
                $"Se actualizaron los datos de: {nino.NombreCompleto}");
        }

        return (true, string.Empty);
    }

    public async Task CambiarEstadoAsync(int id, bool activo, int idUsuario)
    {
        var nino = (await _repositorioNino.ObtenerTodosAsync()).FirstOrDefault(n => n.Id == id);
        string nombre = nino?.NombreCompleto ?? $"ID {id}";
        
        await _repositorioNino.CambiarEstadoAsync(id, activo);
        
        string accion = activo ? "Activación" : "Desactivación";
        await _servicioAuditoria.RegistrarAccionAsync(idUsuario, "Niños", accion, 
            $"Se cambió el estado de {nombre} a {(activo ? "Activo" : "Inactivo")}");
    }
}
