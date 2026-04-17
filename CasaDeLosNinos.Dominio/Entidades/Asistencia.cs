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
    public int? IdObservacion { get; set; } // Referencia a Bitácora
    public int IdUsuario { get; set; }      // FK plana — quién registró

    // Campo informativo (cargado vía JOIN desde Bitácora)
    public string ObservacionTexto { get; set; } = string.Empty;
}
