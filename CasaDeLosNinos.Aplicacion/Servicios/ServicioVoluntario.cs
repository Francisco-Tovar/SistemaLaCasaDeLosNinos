using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioVoluntario : IServicioVoluntario
{
    private readonly IRepositorioVoluntario _repositorio;

    public ServicioVoluntario(IRepositorioVoluntario repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<Voluntario>> ObtenerTodosAsync(bool incluirInactivos = false)
    {
        return await _repositorio.ObtenerTodosAsync(incluirInactivos);
    }

    public async Task<Voluntario?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("El ID debe ser mayor a cero.", nameof(id));
        return await _repositorio.ObtenerPorIdAsync(id);
    }

    public async Task<int> CrearAsync(Voluntario voluntario)
    {
        ValidarVoluntario(voluntario);
        return await _repositorio.CrearAsync(voluntario);
    }

    public async Task<bool> ActualizarAsync(Voluntario voluntario)
    {
        ValidarVoluntario(voluntario);
        return await _repositorio.ActualizarAsync(voluntario);
    }

    public async Task CambiarEstadoAsync(int id, bool activo)
    {
        if (id <= 0) throw new ArgumentException("El ID debe ser mayor a cero.", nameof(id));
        await _repositorio.CambiarEstadoAsync(id, activo);
    }

    private void ValidarVoluntario(Voluntario voluntario)
    {
        if (voluntario == null) throw new ArgumentNullException(nameof(voluntario));
        if (string.IsNullOrWhiteSpace(voluntario.NombreCompleto))
            throw new ArgumentException("El nombre del voluntario es obligatorio.");
        
        // Se requiere al menos un medio de contacto (Correo o Teléfono)
        if (string.IsNullOrWhiteSpace(voluntario.Correo) && string.IsNullOrWhiteSpace(voluntario.Telefono))
            throw new ArgumentException("Debe proporcionar al menos un medio de contacto (Correo o Teléfono).");
    }
}
