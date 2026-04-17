namespace CasaDeLosNinos.Dominio.Dtos;

/// <summary>
/// DTO de lectura que combina datos de Nino y Asistencia para la pantalla de toma de asistencia.
/// Vive en Dominio.Dtos para ser accesible desde la interfaz IServicioAsistencia sin crear
/// dependencias circulares entre capas.
/// No es una entidad persistida; es solo un objeto de transferencia de vista.
/// </summary>
public class NinoAsistenciaDto
{
    public int IdNino { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public bool Presente { get; set; } = false;
    public string Observacion { get; set; } = string.Empty;
}
