using System.Threading.Tasks;

namespace CasaDeLosNinos.Dominio.Interfaces
{
    public interface IServicioFoto
    {
        Task<byte[]?> ObtenerFotoAsync(int idNino);
        
        /// <summary>
        /// Procesa una imagen (redimensionar, comprimir) y la guarda vinculada al niño.
        /// </summary>
        Task<bool> GuardarFotoAsync(int idNino, byte[] imagenOriginal);
        
        Task EliminarFotoAsync(int idNino);
    }
}
