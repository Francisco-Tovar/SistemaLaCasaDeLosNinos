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

    public ServicioNino(IRepositorioNino repositorioNino)
    {
        _repositorioNino = repositorioNino;
    }

    public async Task<IEnumerable<Nino>> ObtenerTodosAsync()
        => await _repositorioNino.ObtenerTodosAsync();

    public async Task<IEnumerable<Nino>> ObtenerActivosAsync()
        => await _repositorioNino.ObtenerActivosAsync();

    public async Task<(bool Exito, string Mensaje)> GuardarAsync(Nino nino)
    {
        // ── Validaciones de negocio ──────────────────────────────────
        if (string.IsNullOrWhiteSpace(nino.NombreCompleto))
            return (false, "El nombre completo del niño es obligatorio.");

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
        }
        else
        {
            // Actualizar existente
            await _repositorioNino.ActualizarAsync(nino);
        }

        return (true, string.Empty);
    }

    public async Task CambiarEstadoAsync(int id, bool activo)
        => await _repositorioNino.CambiarEstadoAsync(id, activo);
}
