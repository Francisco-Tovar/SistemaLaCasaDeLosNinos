namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Registro diario de asistencia de un niño.
/// Una fila por niño por fecha.
/// </summary>
public class Asistencia
{
    public int Id { get; set; }
    public int IdNino { get; set; }         // FK plana
    public DateTime Fecha { get; set; } = DateTime.Today;
    public bool Presente { get; set; } = false;
    public string Observacion { get; set; } = string.Empty;
    public int IdUsuario { get; set; }      // FK plana — quién registró
}
