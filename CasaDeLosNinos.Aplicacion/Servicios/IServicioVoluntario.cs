using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public interface IServicioVoluntario
{
    Task<IEnumerable<Voluntario>> ObtenerTodosAsync(bool incluirInactivos = false);
    Task<Voluntario?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(Voluntario voluntario);
    Task ActualizarAsync(Voluntario voluntario);
    Task CambiarEstadoAsync(int id, bool activo);
}
