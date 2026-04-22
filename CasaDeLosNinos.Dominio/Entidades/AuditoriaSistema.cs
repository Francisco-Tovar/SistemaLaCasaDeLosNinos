namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Representa una entrada en la bitácora universal del sistema.
/// Registra acciones de negocio y errores técnicos para auditoría.
/// </summary>
public class AuditoriaSistema
{
    public int Id { get; set; }
    
    /// <summary>Fecha y hora exacta del evento.</summary>
    public DateTime FechaHora { get; set; } = DateTime.Now;
    
    /// <summary>ID del usuario que realizó la acción. Puede ser NULL para errores del sistema antes del login.</summary>
    public int? IdUsuario { get; set; }
    
    /// <summary>Nombre del usuario (denormalizado para persistencia histórica).</summary>
    public string NombreUsuario { get; set; } = "Sistema";
    
    /// <summary>Módulo donde ocurrió el evento (Niños, Voluntarios, Caja Chica, Usuarios, Sistema).</summary>
    public string Modulo { get; set; } = string.Empty;
    
    /// <summary>Tipo de acción (Creación, Modificación, Baja, Login, Logout, Error).</summary>
    public string Accion { get; set; } = string.Empty;
    
    /// <summary>Detalles descriptivos de lo ocurrido.</summary>
    public string Detalle { get; set; } = string.Empty;
}
