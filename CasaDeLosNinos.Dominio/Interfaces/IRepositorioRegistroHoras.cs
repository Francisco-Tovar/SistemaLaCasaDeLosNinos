using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioRegistroHoras
{
    Task<IEnumerable<RegistroHoras>> ObtenerPorVoluntarioAsync(int idVoluntario);
    Task<int> CrearAsync(RegistroHoras registro);
    Task EliminarAsync(int id);
    Task<decimal> ObtenerTotalHorasVoluntarioAsync(int idVoluntario);
}
