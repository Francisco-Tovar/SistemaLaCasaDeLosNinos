using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioFotoEvento
{
    Task AgregarAsync(FotoEvento foto);
    Task EliminarAsync(int id);
    Task EliminarPorEventoAsync(int idEvento);
    Task<IEnumerable<FotoEvento>> ObtenerPorEventoAsync(int idEvento);
    Task<FotoEvento?> ObtenerPorIdAsync(int id);
}
