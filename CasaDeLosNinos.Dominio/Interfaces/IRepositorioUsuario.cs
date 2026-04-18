using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioUsuario
{
    Task<IEnumerable<Usuario>> ObtenerTodosAsync();
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
    Task<bool> NombreUsuarioExisteAsync(string nombreUsuario, int? idExcluido = null);
    Task<bool> ExisteAdminAsync();
    Task<int> InsertarAsync(Usuario usuario);
    Task<bool> ActualizarAsync(Usuario usuario);
    Task<bool> CambiarEstadoAsync(int id, bool estado);
}
