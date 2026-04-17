using System.Threading.Tasks;

namespace CasaDeLosNinos.Dominio.Interfaces
{
    /// <summary>
    /// Repositorio especializado para el manejo de imágenes binarias (BLOB)
    /// almacenadas en la base de datos secundaria.
    /// </summary>
    public interface IRepositorioFoto
    {
        Task<byte[]?> ObtenerFotoAsync(int idNino);
        Task GuardarFotoAsync(int idNino, byte[] imagen);
        Task EliminarFotoAsync(int idNino);
        Task<bool> ExisteFotoAsync(int idNino);
    }
}
