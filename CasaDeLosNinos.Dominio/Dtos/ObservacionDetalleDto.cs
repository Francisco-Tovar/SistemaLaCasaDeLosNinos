namespace CasaDeLosNinos.Dominio.Dtos;

/// <summary>
/// DTO de lectura para mostrar una observación con el nombre del autor.
/// Generado por un JOIN en la capa Datos — solo para lectura en la UI.
/// </summary>
public class ObservacionDetalleDto
{
    public int      Id            { get; set; }
    public int      IdNino        { get; set; }
    public string   NombreAutor   { get; set; } = string.Empty; // JOIN con Usuarios
    public DateTime FechaHora     { get; set; }
    public string   Contenido     { get; set; } = string.Empty;
}
