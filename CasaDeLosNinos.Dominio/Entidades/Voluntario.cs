namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Persona voluntaria que colabora con la organización.
/// </summary>
public class Voluntario
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Especialidad { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public DateTime FechaIngreso { get; set; } = DateTime.Today;
}
