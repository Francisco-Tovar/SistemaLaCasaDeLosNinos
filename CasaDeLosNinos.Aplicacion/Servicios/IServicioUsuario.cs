using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public interface IServicioUsuario
{
    Task<IEnumerable<Usuario>> ObtenerTodosAsync();
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(Usuario usuario, string contrasenaPlana);
    Task<bool> ActualizarAsync(Usuario usuario, string? nuevaContrasenaPlana = null);
    Task<bool> CambiarEstadoAsync(int id, bool estado);
}
