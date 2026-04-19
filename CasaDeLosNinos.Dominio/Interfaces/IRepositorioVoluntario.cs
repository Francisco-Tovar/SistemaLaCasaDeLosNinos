using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioVoluntario
{
    Task<IEnumerable<Voluntario>> ObtenerTodosAsync(bool incluirInactivos = false);
    Task<Voluntario?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(Voluntario voluntario);
    Task<bool> ActualizarAsync(Voluntario voluntario);
    Task CambiarEstadoAsync(int id, bool activo);
}
