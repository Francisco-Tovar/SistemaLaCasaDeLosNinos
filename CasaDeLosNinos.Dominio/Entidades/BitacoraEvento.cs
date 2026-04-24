namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Representa un evento registrado en la bitácora institucional.
/// </summary>
public class BitacoraEvento
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public int IdUsuario { get; set; }       // Quién registró el evento
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    // Propiedad calculada para el reporte o UI si es necesario
    public string NombreUsuario { get; set; } = string.Empty;
}
