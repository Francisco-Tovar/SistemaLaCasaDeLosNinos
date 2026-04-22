namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Representa a un niño o niña beneficiaria de la organización.
/// Borrado lógico: nunca se elimina físicamente (Activo = false).
/// </summary>
public class Nino
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string Genero { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string NombreEncargado { get; set; } = string.Empty;
    public string TelefonoEncargado { get; set; } = string.Empty;
    public DateTime FechaIngreso { get; set; } = DateTime.Today;
    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    /// <summary>Fecha en que el niño fue desactivado. Null si está activo.</summary>
    public DateTime? FechaBaja { get; set; }
}
