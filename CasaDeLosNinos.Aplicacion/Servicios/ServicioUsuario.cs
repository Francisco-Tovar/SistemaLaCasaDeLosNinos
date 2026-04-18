using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using BCrypt.Net;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioUsuario : IServicioUsuario
{
    private readonly IRepositorioUsuario _repositorioUsuario;

    public ServicioUsuario(IRepositorioUsuario repositorioUsuario)
    {
        _repositorioUsuario = repositorioUsuario;
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        return await _repositorioUsuario.ObtenerTodosAsync();
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _repositorioUsuario.ObtenerPorIdAsync(id);
    }

    public async Task<int> CrearAsync(Usuario usuario, string contrasenaPlana)
    {
        if (await _repositorioUsuario.NombreUsuarioExisteAsync(usuario.NombreUsuario))
        {
            throw new InvalidOperationException("El nombre de usuario ya está en uso.");
        }

        if (string.IsNullOrWhiteSpace(contrasenaPlana))
        {
            throw new InvalidOperationException("La contraseña es requerida para nuevos usuarios.");
        }

        usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(contrasenaPlana);
        return await _repositorioUsuario.InsertarAsync(usuario);
    }

    public async Task<bool> ActualizarAsync(Usuario usuario, string? nuevaContrasenaPlana = null)
    {
        if (await _repositorioUsuario.NombreUsuarioExisteAsync(usuario.NombreUsuario, usuario.Id))
        {
            throw new InvalidOperationException("El nombre de usuario ya está en uso por otra cuenta.");
        }

        var usuarioExistente = await _repositorioUsuario.ObtenerPorIdAsync(usuario.Id);
        if (usuarioExistente == null) return false;

        if (!string.IsNullOrWhiteSpace(nuevaContrasenaPlana))
        {
            usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(nuevaContrasenaPlana);
        }
        else
        {
            usuario.ContrasenaHash = usuarioExistente.ContrasenaHash;
        }

        return await _repositorioUsuario.ActualizarAsync(usuario);
    }

    public async Task<bool> CambiarEstadoAsync(int id, bool estado)
    {
        if (!estado) // intentando desactivar
        {
            var usuarioDb = await _repositorioUsuario.ObtenerPorIdAsync(id);
            if (usuarioDb != null && usuarioDb.IdRol == 1)
            {
                // check if it's the last active admin
                var usuarios = await _repositorioUsuario.ObtenerTodosAsync();
                int adminsActivos = usuarios.Count(u => u.IdRol == 1 && u.Activo && u.Id != id);
                if (adminsActivos == 0)
                {
                    throw new InvalidOperationException("No se puede desactivar al único administrador activo del sistema.");
                }
            }
        }

        return await _repositorioUsuario.CambiarEstadoAsync(id, estado);
    }
}
