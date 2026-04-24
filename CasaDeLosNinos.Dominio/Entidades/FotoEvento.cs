namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Representa una fotografía asociada a un evento de la bitácora.
/// </summary>
public class FotoEvento
{
    public int Id { get; set; }
    public int IdEvento { get; set; }
    public byte[] Imagen { get; set; } = Array.Empty<byte>();
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}
