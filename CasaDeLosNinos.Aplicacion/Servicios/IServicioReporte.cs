using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDeLosNinos.Aplicacion.Servicios
{
    public interface IServicioReporte
    {
        Task<byte[]> GenerarReporteAsistenciaPdfAsync(int anio, int mes);
        Task<string> GenerarReporteAsistenciaCsvAsync(int anio, int mes);

        Task<byte[]> GenerarReporteCajaChicaPdfAsync(int anio, int mes);
        Task<string> GenerarReporteCajaChicaCsvAsync(int anio, int mes);

        Task<byte[]> GenerarReporteVoluntariosResumidoPdfAsync(DateTime inicio, DateTime fin);
        Task<byte[]> GenerarReporteVoluntariosDetalladoPdfAsync(DateTime inicio, DateTime fin);
        Task<string> GenerarReporteVoluntariosCsvAsync(DateTime inicio, DateTime fin);

        Task RespaldarSistemaFullAsync(string rutaDestino);
        Task RestaurarSistemaFullAsync(string rutaZip);

        // Métodos para vista previa en Grid
        Task<IEnumerable<object>> ObtenerDatosAsistenciaAsync(int anio, int mes);
        Task<IEnumerable<object>> ObtenerDatosCajaChicaAsync(int anio, int mes);
        Task<IEnumerable<object>> ObtenerDatosVoluntariosResumidoAsync(DateTime inicio, DateTime fin);
        Task<IEnumerable<object>> ObtenerDatosVoluntariosDetalladoAsync(DateTime inicio, DateTime fin);
    }
}
