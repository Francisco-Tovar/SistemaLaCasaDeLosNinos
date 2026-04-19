
namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Movimiento de caja chica (ingreso o egreso de fondos).
/// El saldo líquido se calcula en tiempo real sumando/restando todos los registros.
/// </summary>
public class CajaChica
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Today;
    public string Concepto { get; set; } = string.Empty;
    public decimal Monto { get; set; }

    /// <summary>Valores válidos: "Ingreso" | "Egreso"</summary>
    public string TipoMovimiento { get; set; } = string.Empty;
    public int IdUsuario { get; set; }      // FK plana — quién registró
    public int? IdFotoRecibo { get; set; }  // Nullable, para comprobantes
}

