using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDeLosNinos.Aplicacion.Servicios
{
    public interface IServicioReporte
    {
        Task<byte[]> GenerarReporteAsistenciaPdfAsync(int anio, int mes);
        Task<string> GenerarReporteAsistenciaCsvAsync(int anio, int mes);

        Task<byte[]> GenerarReporteCajaChicaPdfAsync(int anio, int mes, bool incluirImagenes);
        Task<string> GenerarReporteCajaChicaCsvAsync(int anio, int mes);
        Task<Dictionary<string, byte[]>> ObtenerImagenesCajaChicaAsync(int anio, int mes);

        Task<byte[]> GenerarReporteAsistenciaIndividualPdfAsync(int idNino, DateTime inicio, DateTime fin);
        Task<byte[]> GenerarReporteActividadesVoluntarioPdfAsync(int idVoluntario, DateTime inicio, DateTime fin);

        Task<byte[]> GenerarReporteVoluntariosResumidoPdfAsync(DateTime inicio, DateTime fin);
        Task<byte[]> GenerarReporteVoluntariosDetalladoPdfAsync(DateTime inicio, DateTime fin);
        Task<string> GenerarReporteVoluntariosCsvAsync(DateTime inicio, DateTime fin);

        // Nuevo: Altas y Bajas de Niños
        Task<byte[]> GenerarReporteFlujoBeneficiariosPdfAsync(DateTime inicio, DateTime fin);
        Task<string> GenerarReporteFlujoBeneficiariosCsvAsync(DateTime inicio, DateTime fin);

        Task RespaldarSistemaFullAsync(string rutaDestino);
        Task RestaurarSistemaFullAsync(string rutaZip);

        // Métodos para vista previa en Grid
        Task<IEnumerable<object>> ObtenerDatosAsistenciaAsync(int anio, int mes);
        Task<IEnumerable<object>> ObtenerDatosCajaChicaAsync(int anio, int mes);
        Task<IEnumerable<object>> ObtenerDatosVoluntariosResumidoAsync(DateTime inicio, DateTime fin);
        Task<IEnumerable<object>> ObtenerDatosVoluntariosDetalladoAsync(DateTime inicio, DateTime fin);
        Task<IEnumerable<object>> ObtenerDatosAsistenciaIndividualAsync(int idNino, DateTime inicio, DateTime fin);
        Task<IEnumerable<object>> ObtenerDatosActividadesVoluntarioAsync(int idVoluntario, DateTime inicio, DateTime fin);
        Task<IEnumerable<object>> ObtenerDatosFlujoBeneficiariosAsync(DateTime inicio, DateTime fin);
        
        // Métodos de Auditoría
        Task<byte[]> GenerarReporteAuditoriaCajaChicaPdfAsync(int anio, int mes);
        Task<IEnumerable<object>> ObtenerDatosAuditoriaCajaChicaAsync(int anio, int mes);
        Task<string> GenerarReporteAuditoriaCajaChicaCsvAsync(int anio, int mes);
    }
}
