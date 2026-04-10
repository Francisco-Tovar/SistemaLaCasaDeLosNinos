namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Nota de seguimiento cualitativo sobre un niño.
/// La marca de tiempo es inmutable — nunca se edita una vez registrada.
/// </summary>
public class Observacion
{
    public int Id { get; set; }
    public int IdNino { get; set; }         // FK plana
    public int IdUsuario { get; set; }      // FK plana — quién escribió la nota
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public string Contenido { get; set; } = string.Empty;
}
