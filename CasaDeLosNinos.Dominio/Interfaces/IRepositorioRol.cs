using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioRol
{
    Task<IEnumerable<Rol>> ObtenerTodosAsync();
    Task<Rol?> ObtenerPorIdAsync(int id);
}
