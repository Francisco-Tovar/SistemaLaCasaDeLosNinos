using System;

namespace CasaDeLosNinos.Dominio.Dtos
{
    public class AuditoriaDetalleDTO
    {
        public int IdMovimiento { get; set; }
        public DateTime FechaHoraCambio { get; set; }
        public string ConceptoOriginal { get; set; } = string.Empty;
        public string DetallesDelCambio { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;

        // Propiedad calculada para el grid
        public string FechaFormateada => FechaHoraCambio.ToString("dd/MM HH:mm");
    }
}
