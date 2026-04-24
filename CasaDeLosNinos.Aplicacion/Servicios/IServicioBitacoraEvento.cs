using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public interface IServicioBitacoraEvento
{
    Task<int> RegistrarEventoAsync(BitacoraEvento evento, IEnumerable<byte[]> fotos);
    Task ActualizarEventoAsync(BitacoraEvento evento, IEnumerable<byte[]> fotosNuevas, IEnumerable<int> idsFotosAEliminar);
    Task EliminarEventoAsync(int id);
    Task<BitacoraEvento?> ObtenerEventoAsync(int id);
    Task<IEnumerable<BitacoraEvento>> ObtenerTodosAsync();
    Task<IEnumerable<FotoEvento>> ObtenerFotosEventoAsync(int idEvento);
}
