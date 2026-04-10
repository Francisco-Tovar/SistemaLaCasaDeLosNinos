namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Registro de horas aportadas por un voluntario en una fecha específica.
/// </summary>
public class RegistroHoras
{
    public int Id { get; set; }
    public int IdVoluntario { get; set; }   // FK plana
    public DateTime Fecha { get; set; } = DateTime.Today;
    public decimal HorasAportadas { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public int IdUsuario { get; set; }      // FK plana — quién registró
}
