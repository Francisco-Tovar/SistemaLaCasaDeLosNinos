using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioUsuario
{
    Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
    Task<bool> ExisteAdminAsync();
    Task<int> InsertarAsync(Usuario usuario);
}
