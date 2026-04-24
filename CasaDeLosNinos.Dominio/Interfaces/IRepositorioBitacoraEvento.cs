using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IRepositorioBitacoraEvento
{
    Task<int> AgregarAsync(BitacoraEvento evento);
    Task ActualizarAsync(BitacoraEvento evento);
    Task EliminarAsync(int id);
    Task<BitacoraEvento?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<BitacoraEvento>> ObtenerTodosAsync();
    Task<IEnumerable<BitacoraEvento>> ObtenerPorRangoFechaAsync(DateTime inicio, DateTime fin);
}
