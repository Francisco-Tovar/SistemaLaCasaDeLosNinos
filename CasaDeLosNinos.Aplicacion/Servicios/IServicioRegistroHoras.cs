using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public interface IServicioRegistroHoras
{
    Task<IEnumerable<RegistroHoras>> ObtenerPorVoluntarioAsync(int idVoluntario);
    Task<int> RegistrarHorasAsync(RegistroHoras registro);
    Task EliminarAsync(int id);
    Task<decimal> ObtenerTotalHorasVoluntarioAsync(int idVoluntario);
}
