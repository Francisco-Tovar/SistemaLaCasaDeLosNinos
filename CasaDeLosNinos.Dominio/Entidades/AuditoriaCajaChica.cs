namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Registro inmutable de eventos de auditoría para movimientos monetarios.
/// Garantiza la trazabilidad exigida por regulaciones contables.
/// </summary>
public class AuditoriaCajaChica
{
    public int Id { get; set; }
    public int IdMovimiento { get; set; } // Referencia al ID original en CajaChica
    public int IdUsuario { get; set; } // Qué usuario realizó el cambio
    public DateTime FechaHoraCambio { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Descripción textual de los cambios realizados. Ej: "Monto: 50 -> 60. Concepto: X -> Y"
    /// </summary>
    public string DetallesDelCambio { get; set; } = string.Empty;
}
