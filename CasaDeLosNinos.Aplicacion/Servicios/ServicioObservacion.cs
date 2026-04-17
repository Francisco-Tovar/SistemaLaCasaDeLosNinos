using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

/// <summary>
/// Servicio que implementa las reglas de negocio del módulo de Observaciones:
///   1. El FechaHora es siempre DateTime.Now (sistema) — no lo ingresa el usuario.
///   2. El IdUsuario es siempre el del usuario en sesión — no lo selecciona el usuario.
///   3. El contenido no puede estar vacío ni superar 2000 caracteres.
/// </summary>
public class ServicioObservacion : IServicioObservacion
{
    private readonly IRepositorioObservacion _repositorio;

    public ServicioObservacion(IRepositorioObservacion repositorio)
    {
        _repositorio = repositorio;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ObservacionDetalleDto>> ObtenerHistorialAsync(int idNino)
        => await _repositorio.ObtenerPorNinoAsync(idNino);

    /// <inheritdoc/>
    public async Task<int> RegistrarAsync(int idNino, int idUsuarioSesion, string contenido, DateTime? fechaManual = null)
    {
        // ── Validaciones de negocio ───────────────────────────────
        if (idNino <= 0)
            throw new ArgumentException("El identificador del niño no es válido.", nameof(idNino));

        if (idUsuarioSesion <= 0)
            throw new ArgumentException("El identificador del usuario en sesión no es válido.", nameof(idUsuarioSesion));

        var texto = contenido?.Trim() ?? string.Empty;

        if (string.IsNullOrEmpty(texto))
            throw new InvalidOperationException("El contenido de la observación no puede estar vacío.");

        if (texto.Length > 2000)
            throw new InvalidOperationException("El contenido no puede superar los 2,000 caracteres.");

        // ── FechaHora asignada ───────────────────────────────────
        var observacion = new Observacion
        {
            IdNino    = idNino,
            IdUsuario = idUsuarioSesion,
            FechaHora = fechaManual ?? DateTime.Now, // <--- Usamos fecha manual si existe
            Contenido = texto
        };

        return await _repositorio.InsertarAsync(observacion);
    }

    /// <inheritdoc/>
    public async Task ActualizarAsync(int id, string contenido)
    {
        var texto = contenido?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(texto))
            throw new InvalidOperationException("El contenido no puede estar vacío.");

        if (texto.Length > 2000)
            throw new InvalidOperationException("El contenido no puede superar los 2,000 caracteres.");

        await _repositorio.ActualizarAsync(id, texto);
    }

    /// <inheritdoc/>
    public async Task EliminarAsync(int id)
    {
        if (id <= 0) return;
        await _repositorio.EliminarAsync(id);
    }
}
