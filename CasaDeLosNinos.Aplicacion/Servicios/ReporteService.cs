using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace CasaDeLosNinos.Aplicacion.Servicios
{
    public class ReporteService : IServicioReporte
    {
        private readonly IRepositorioAsistencia _repositorioAsistencia;
        private readonly IRepositorioCajaChica _repositorioCajaChica;
        private readonly IRepositorioVoluntario _repositorioVoluntario;
        private readonly IRepositorioRegistroHoras _repositorioRegistroHoras;
        private readonly IRepositorioNino _repositorioNino;
        private readonly IRepositorioFoto _repositorioFoto;
        private readonly IConfiguration _configuracion;

        public ReporteService(
            IRepositorioAsistencia repositorioAsistencia,
            IRepositorioCajaChica repositorioCajaChica,
            IRepositorioVoluntario repositorioVoluntario,
            IRepositorioRegistroHoras repositorioRegistroHoras,
            IRepositorioNino repositorioNino,
            IRepositorioFoto repositorioFoto,
            IConfiguration configuracion)
        {
            _repositorioAsistencia = repositorioAsistencia;
            _repositorioCajaChica = repositorioCajaChica;
            _repositorioVoluntario = repositorioVoluntario;
            _repositorioRegistroHoras = repositorioRegistroHoras;
            _repositorioNino = repositorioNino;
            _repositorioFoto = repositorioFoto;
            _configuracion = configuracion;

            // Configurar licencia de QuestPDF (TCU - Uso Comunitario)
            QuestPDF.Settings.License = LicenseType.Community;
        }

        #region Reportes PDF

        public async Task<byte[]> GenerarReporteAsistenciaPdfAsync(int anio, int mes)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("es-CR");
            var periodName = new DateTime(anio, mes, 1).ToString("MMMM yyyy", culture).ToUpper();
            var asistencia = await _repositorioAsistencia.ObtenerPorMesAsync(anio, mes);

            // Solo mostrar niños que tuvieron al menos un registro en el periodo
            var idsConRegistro = asistencia.Select(a => a.IdNino).Distinct().ToHashSet();
            var todosNinos = await _repositorioNino.ObtenerTodosAsync();
            var ninos = todosNinos.Where(n => n.Activo || idsConRegistro.Contains(n.Id));

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DE ASISTENCIA MENSUAL", periodName, new Dictionary<string, string> { 
                        { "Tipo", "Resumido Mensual" },
                        { "Alcance", "Todos los niños activos" }
                    });

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Beneficiario");
                            header.Cell().Element(CellStyle).Text("Presente");
                            header.Cell().Element(CellStyle).Text("Ausente");

                            static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                        });

                        foreach (var nino in ninos.OrderBy(n => n.NombreCompleto))
                        {
                            var registros = asistencia.Where(a => a.IdNino == nino.Id).ToList();
                            int presentes = registros.Count(a => a.Presente);
                            int ausentes = registros.Count(a => !a.Presente);

                            table.Cell().Element(RowStyle).Text(nino.NombreCompleto);
                            table.Cell().Element(RowStyle).Text(presentes.ToString());
                            table.Cell().Element(RowStyle).Text(ausentes.ToString());

                            static IContainer RowStyle(IContainer container) => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                        }
                    });

                    ConfigurarPiePagina(page);
                });
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerarReporteCajaChicaPdfAsync(int anio, int mes, bool incluirImagenes)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("es-CR");
            var periodName = new DateTime(anio, mes, 1).ToString("MMMM yyyy", culture).ToUpper();
            var movimientos = (await _repositorioCajaChica.ObtenerPorMesAsync(anio, mes)).OrderBy(m => m.Fecha).ToList();
            var saldo = await _repositorioCajaChica.ObtenerSaldoMensualAsync(anio, mes);

            // Pre-fetch de imágenes si es necesario
            var fotosMap = new Dictionary<int, byte[]>();
            if (incluirImagenes)
            {
                var movimientosConFoto = movimientos.Where(m => m.IdFotoRecibo.HasValue).ToList();
                foreach (var m in movimientosConFoto)
                {
                    var f = await _repositorioFoto.ObtenerFotoAsync(m.IdFotoRecibo!.Value);
                    if (f != null) fotosMap[m.IdFotoRecibo.Value] = f;
                }
            }

            var document = Document.Create(container =>
            {
                // Página Principal: Tabla de movimientos
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DE FISCALIZACIÓN CAJA CHICA", periodName, new Dictionary<string, string> {
                        { "Saldo Período", $"₡{saldo:N2}" },
                        { "Imágenes", incluirImagenes ? "Incluidas" : "No incluidas" }
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(80);
                                columns.RelativeColumn();
                                columns.ConstantColumn(100);
                                columns.ConstantColumn(100);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Fecha");
                                header.Cell().Element(CellStyle).Text("Concepto");
                                header.Cell().Element(CellStyle).Text("Ingreso");
                                header.Cell().Element(CellStyle).Text("Egreso");

                                static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });

                            foreach (var mov in movimientos)
                            {
                                table.Cell().Element(RowStyle).Text(mov.Fecha.ToString("dd/MM/yyyy"));
                                table.Cell().Element(RowStyle).Text(mov.Concepto);
                                table.Cell().Element(RowStyle).Text(mov.TipoMovimiento == "Ingreso" ? $"₡{mov.Monto:N2}" : "");
                                table.Cell().Element(RowStyle).Text(mov.TipoMovimiento == "Egreso" ? $"₡{mov.Monto:N2}" : "");

                                static IContainer RowStyle(IContainer container) => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                            }
                        });

                        col.Item().PaddingTop(10).AlignRight().Text(x =>
                        {
                            x.Span("SALDO FINAL DEL PERIODO: ").SemiBold();
                            x.Span($"₡{saldo:N2}").SemiBold().FontColor(saldo < 0 ? Colors.Red.Medium : Colors.Green.Medium);
                        });
                    });

                    ConfigurarPiePagina(page);
                });

                // Página(s) de Anexo: Comprobantes
                if (incluirImagenes && fotosMap.Any())
                {
                    container.Page(page =>
                    {
                        page.Margin(50);
                        ConfigurarCabecera(page, "ANEXO DE COMPROBANTES", periodName, new Dictionary<string, string> {
                            { "Origen", "Caja Chica" },
                            { "Total Fotos", fotosMap.Count.ToString() }
                        });

                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            var movsOrdenados = movimientos.Where(m => m.IdFotoRecibo.HasValue && fotosMap.ContainsKey(m.IdFotoRecibo.Value))
                                                           .OrderBy(m => m.Fecha);

                            foreach (var mov in movsOrdenados)
                            {
                                var foto = fotosMap[mov.IdFotoRecibo!.Value];
                                
                                // Bloque de información del movimiento
                                col.Item().PaddingTop(15).Text($"{mov.Fecha:dd/MM/yyyy} - {mov.Concepto} (₡{mov.Monto:N2})").SemiBold().FontSize(10);
                                
                                // Bloque de la imagen (separado para permitir saltos de página)
                                col.Item().PaddingTop(5).PaddingBottom(10).BorderBottom(1).BorderColor(Colors.Grey.Lighten3)
                                   .MaxHeight(400) 
                                   .AlignCenter()
                                   .Image(foto, ImageScaling.FitArea);
                            }
                        });

                        ConfigurarPiePagina(page);
                    });
                }
            });

            return document.GeneratePdf();
        }

        public async Task<Dictionary<string, byte[]>> ObtenerImagenesCajaChicaAsync(int anio, int mes)
        {
            var movimientos = (await _repositorioCajaChica.ObtenerPorMesAsync(anio, mes))
                                .Where(m => m.IdFotoRecibo.HasValue)
                                .OrderBy(m => m.Fecha);
            
            var dictionary = new Dictionary<string, byte[]>();
            foreach (var mov in movimientos)
            {
                var foto = await _repositorioFoto.ObtenerFotoAsync(mov.IdFotoRecibo!.Value);
                if (foto != null)
                {
                    string safeConcept = string.Join("_", mov.Concepto.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");
                    if (safeConcept.Length > 30) safeConcept = safeConcept.Substring(0, 30);
                    
                    string fileName = $"{mov.Fecha:yyyyMMdd}_{mov.Id}_{safeConcept}.jpg";
                    dictionary[fileName] = foto;
                }
            }
            return dictionary;
        }

        public async Task<byte[]> GenerarReporteAsistenciaIndividualPdfAsync(int idNino, DateTime inicio, DateTime fin)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("es-CR");
            var nino = await _repositorioNino.ObtenerPorIdAsync(idNino);
            if (nino == null) throw new ArgumentException("El niño especificado no existe.");

            var periodText = $"{inicio:dd/MM/yyyy} Al {fin:dd/MM/yyyy}".ToUpper();
            // Usamos ObtenerPorRangoAsync para soportar correctamente rangos multi-mes
            var asistencia = (await _repositorioAsistencia.ObtenerPorRangoAsync(inicio.Date, fin.Date))
                .Where(a => a.IdNino == idNino)
                .OrderBy(a => a.Fecha).ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, $"REPORTE DE ASISTENCIA INDIVIDUAL: {nino.NombreCompleto.ToUpper()}", "REGISTRO DE ASISTENCIA DETALLADO", new Dictionary<string, string> {
                        { "DNI/ID", nino.Id.ToString() },
                        { "Desde", inicio.ToString("dd/MM/yyyy") },
                        { "Hasta", fin.ToString("dd/MM/yyyy") }
                    });
                    
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Periodo: {periodText}").Italic();
                        
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100);
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Fecha");
                                header.Cell().Element(CellStyle).Text("Estado");
                                static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });

                            foreach (var reg in asistencia)
                            {
                                table.Cell().Element(RowStyle).Text(reg.Fecha.ToString("dd/MM/yyyy"));
                                table.Cell().Element(RowStyle).Text(reg.Presente ? "PRESENTE" : "AUSENTE").FontColor(reg.Presente ? Colors.Green.Medium : Colors.Red.Medium);
                                
                                static IContainer RowStyle(IContainer container) => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                            }
                        });

                        col.Item().PaddingTop(10).AlignRight().Text(x =>
                        {
                            int total = asistencia.Count;
                            int presentes = asistencia.Count(a => a.Presente);
                            double porcentaje = total > 0 ? (double)presentes / total * 100 : 0;
                            
                            x.Span($"Total Días: {total} | Presente: {presentes} | Porcentaje: ").SemiBold();
                            x.Span($"{porcentaje:N1}%").SemiBold().FontColor(porcentaje >= 80 ? Colors.Green.Medium : Colors.Orange.Medium);
                        });
                    });

                    ConfigurarPiePagina(page);
                });
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerarReporteActividadesVoluntarioPdfAsync(int idVoluntario, DateTime inicio, DateTime fin)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("es-CR");
            var vol = await _repositorioVoluntario.ObtenerPorIdAsync(idVoluntario);
            if (vol == null) throw new ArgumentException("El voluntario especificado no existe.");

            var periodText = $"{inicio:dd/MM/yyyy} Al {fin:dd/MM/yyyy}".ToUpper();
            var horas = (await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(idVoluntario))
                .Where(h => h.Fecha >= inicio.Date && h.Fecha <= fin.Date)
                .OrderBy(h => h.Fecha).ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, $"REPORTE DE ACTIVIDADES: {vol.NombreCompleto.ToUpper()}", "BITÁCORA DE VOLUNTARIADO INDIVIDUAL", new Dictionary<string, string> {
                        { "Voluntario ID", vol.Id.ToString() },
                        { "Desde", inicio.ToString("dd/MM/yyyy") },
                        { "Hasta", fin.ToString("dd/MM/yyyy") }
                    });
                    
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text($"Periodo: {periodText}").Italic();
                        
                        col.Item().PaddingTop(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(80);
                                columns.RelativeColumn();
                                columns.ConstantColumn(60);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Fecha");
                                header.Cell().Element(CellStyle).Text("Actividad/Descripción");
                                header.Cell().Element(CellStyle).AlignRight().Text("Horas");
                                static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });

                            foreach (var h in horas)
                            {
                                table.Cell().Element(RowStyle).Text(h.Fecha.ToString("dd/MM/yyyy"));
                                table.Cell().Element(RowStyle).Text(h.Descripcion);
                                table.Cell().Element(RowStyle).AlignRight().Text(h.HorasAportadas.ToString("N1"));
                                
                                static IContainer RowStyle(IContainer container) => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                            }
                        });

                        col.Item().PaddingTop(10).AlignRight().Text(x =>
                        {
                            double totalHoras = (double)horas.Sum(h => h.HorasAportadas);
                            x.Span("TOTAL HORAS APORTADAS: ").SemiBold();
                            x.Span($"{totalHoras:N1}").SemiBold().FontColor(Colors.Indigo.Medium);
                        });
                    });

                    ConfigurarPiePagina(page);
                });
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerarReporteFlujoBeneficiariosPdfAsync(DateTime inicio, DateTime fin)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("es-CR");
            var periodText = $"{inicio:dd/MM/yyyy} Al {fin:dd/MM/yyyy}".ToUpper();
            var todosNinos = await _repositorioNino.ObtenerTodosAsync();

            // Altas: niños cuya FechaIngreso cae dentro del periodo
            var altas = todosNinos
                .Where(n => n.FechaIngreso >= inicio.Date && n.FechaIngreso <= fin.Date)
                .OrderBy(n => n.FechaIngreso).ToList();

            // Bajas: niños cuya FechaBaja cae dentro del periodo
            var bajas = todosNinos
                .Where(n => n.FechaBaja.HasValue && n.FechaBaja.Value >= inicio.Date && n.FechaBaja.Value <= fin.Date)
                .OrderBy(n => n.FechaBaja).ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DE ALTAS Y BAJAS DE BENEFICIARIOS", periodText, new Dictionary<string, string> {
                        { "Altas en el periodo", altas.Count.ToString() },
                        { "Bajas en el periodo", bajas.Count.ToString() }
                    });

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Text("ALTAS (Nuevos Ingresos)").SemiBold().FontSize(12).FontColor(Colors.Green.Darken2);
                        col.Item().PaddingTop(5).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(100);
                                columns.ConstantColumn(70);
                            });
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Nombre");
                                header.Cell().Element(CellStyle).Text("Fecha Ingreso");
                                header.Cell().Element(CellStyle).Text("Estado");
                                static IContainer CellStyle(IContainer c) => c.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });
                            foreach (var n in altas)
                            {
                                table.Cell().Element(RowStyle).Text(n.NombreCompleto);
                                table.Cell().Element(RowStyle).Text(n.FechaIngreso.ToString("dd/MM/yyyy"));
                                table.Cell().Element(RowStyle).Text(n.Activo ? "Activo" : "Inactivo").FontColor(n.Activo ? Colors.Green.Medium : Colors.Grey.Medium);
                                static IContainer RowStyle(IContainer c) => c.PaddingVertical(4).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                            }
                        });

                        col.Item().PaddingTop(20).Text("BAJAS (Desactivaciones)").SemiBold().FontSize(12).FontColor(Colors.Red.Darken2);
                        col.Item().PaddingTop(5).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(100);
                                columns.ConstantColumn(100);
                            });
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Nombre");
                                header.Cell().Element(CellStyle).Text("Fecha Ingreso");
                                header.Cell().Element(CellStyle).Text("Fecha Baja");
                                static IContainer CellStyle(IContainer c) => c.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                            });
                            foreach (var n in bajas)
                            {
                                table.Cell().Element(RowStyle).Text(n.NombreCompleto);
                                table.Cell().Element(RowStyle).Text(n.FechaIngreso.ToString("dd/MM/yyyy"));
                                table.Cell().Element(RowStyle).Text(n.FechaBaja!.Value.ToString("dd/MM/yyyy"));
                                static IContainer RowStyle(IContainer c) => c.PaddingVertical(4).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                            }
                        });
                    });

                    ConfigurarPiePagina(page);
                });
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerarReporteVoluntariosResumidoPdfAsync(DateTime inicio, DateTime fin)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("es-CR");
            var periodText = $"{inicio:dd/MM/yyyy} Al {fin:dd/MM/yyyy}".ToUpper();
            // Incluir inactivos: un voluntario que participó en el periodo debe aparecer
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync(incluirInactivos: true);
            
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE RESUMIDO DE VOLUNTARIOS", periodText, new Dictionary<string, string> {
                        { "Rango", periodText },
                        { "Total Voluntarios", voluntarios.Count().ToString() }
                    });

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                        });

                        // Pre-fetch de horas para evitar .Result
                        var voluntariosConHoras = new List<(Voluntario Vol, double Horas)>();
                        foreach (var vol in voluntarios)
                        {
                            var horas = Task.Run(() => _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id)).Result;
                            var horasPeriodo = horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin).Sum(h => h.HorasAportadas);
                            voluntariosConHoras.Add((vol, (double)horasPeriodo));
                        }

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Nombre del Voluntario");
                            header.Cell().Element(CellStyle).Text("Total Horas");

                            static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                        });

                        foreach (var item in voluntariosConHoras)
                        {
                            table.Cell().Element(RowStyle).Text(item.Vol.NombreCompleto);
                            table.Cell().Element(RowStyle).Text(item.Horas.ToString("N1"));

                            static IContainer RowStyle(IContainer container) => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                        }
                    });

                    ConfigurarPiePagina(page);
                });
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerarReporteVoluntariosDetalladoPdfAsync(DateTime inicio, DateTime fin)
        {
            var periodText = $"{inicio:dd/MM/yyyy} Al {fin:dd/MM/yyyy}".ToUpper();
            // Incluir inactivos: un voluntario que participó en el periodo debe aparecer
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync(incluirInactivos: true);
            // Pre-fetch de datos para detalle
            var datosVoluntarios = new List<(Voluntario Vol, List<RegistroHoras> Horas)>();
            var document = Document.Create(container =>
            {
                container.Page(async page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DETALLADO DE VOLUNTARIOS", periodText, new Dictionary<string, string> {
                        { "Detalle", "Actividades por fecha" },
                        { "Total con Actividad", datosVoluntarios.Count.ToString() }
                    });

                   
                    foreach (var vol in voluntarios)
                    {
                        var horas = ( await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id))
                            .Where(h => h.Fecha >= inicio && h.Fecha <= fin).OrderBy(h => h.Fecha).ToList();
                        if (horas.Any()) datosVoluntarios.Add((vol, horas));
                    }

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        foreach (var item in datosVoluntarios)
                        {
                            col.Item().PaddingVertical(5).Text(item.Vol.NombreCompleto).SemiBold().FontSize(12).FontColor(Colors.Indigo.Medium);
                            
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(80);
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(60);
                                });

                                foreach (var h in item.Horas)
                                {
                                    table.Cell().Element(RowStyle).Text(h.Fecha.ToString("dd/MM/yyyy"));
                                    table.Cell().Element(RowStyle).Text(h.Descripcion);
                                    table.Cell().Element(RowStyle).Text(h.HorasAportadas.ToString("N1"));

                                    static IContainer RowStyle(IContainer container) => container.PaddingVertical(2).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                                }
                            });
                            
                            col.Item().PaddingBottom(10).AlignRight().Text($"Subtotal: {item.Horas.Sum(h => h.HorasAportadas):N1} horas").FontSize(10).Italic();
                        }
                    });

                    ConfigurarPiePagina(page);
                });
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerarReporteAuditoriaCajaChicaPdfAsync(int anio, int mes)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("es-CR");
            var periodName = new DateTime(anio, mes, 1).ToString("MMMM yyyy", culture).ToUpper();
            var auditorias = await _repositorioCajaChica.ObtenerAuditoriasDetalladasPorMesAsync(anio, mes);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DE AUDITORÍA: CAJA CHICA", periodName, new Dictionary<string, string> {
                        { "Tipo", "Rastreo de Cambios" },
                        { "Alcance", "Movimientos modificados" }
                    });

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(80);
                            columns.ConstantColumn(80);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Fecha/Hora");
                            header.Cell().Element(CellStyle).Text("Usuario");
                            header.Cell().Element(CellStyle).Text("Referencia");
                            header.Cell().Element(CellStyle).Text("Detalles del Cambio");

                            static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold().FontSize(9)).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                        });

                        foreach (var aud in auditorias.OrderByDescending(a => a.FechaHoraCambio))
                        {
                            table.Cell().Element(RowStyle).Text(aud.FechaHoraCambio.ToString("dd/MM HH:mm")).FontSize(8);
                            table.Cell().Element(RowStyle).Text(aud.Usuario).FontSize(8);
                            table.Cell().Element(RowStyle).Text($"ID {aud.IdMovimiento}: {aud.ConceptoOriginal}").FontSize(8);
                            table.Cell().Element(RowStyle).Text(aud.DetallesDelCambio).FontSize(8);

                            static IContainer RowStyle(IContainer container) => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                        }
                    });

                    ConfigurarPiePagina(page);
                });
            });

            return document.GeneratePdf();
        }

        #endregion

        #region Reportes CSV

        public async Task<string> GenerarReporteAsistenciaCsvAsync(int anio, int mes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Beneficiario,Estado,Presentes,Ausentes");

            var asistencia = await _repositorioAsistencia.ObtenerPorMesAsync(anio, mes);
            var idsConRegistro = asistencia.Select(a => a.IdNino).Distinct().ToHashSet();
            var todosNinos = await _repositorioNino.ObtenerTodosAsync();
            var ninos = todosNinos.Where(n => n.Activo || idsConRegistro.Contains(n.Id));

            foreach (var nino in ninos.OrderBy(n => n.NombreCompleto))
            {
                var registros = asistencia.Where(a => a.IdNino == nino.Id).ToList();
                var estado = nino.Activo ? "Activo" : "Inactivo";
                sb.AppendLine($"{nino.NombreCompleto},{estado},{registros.Count(a => a.Presente)},{registros.Count(a => !a.Presente)}");
            }

            return sb.ToString();
        }

        public async Task<string> GenerarReporteCajaChicaCsvAsync(int anio, int mes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Fecha,Concepto,Tipo,Monto");

            var movimientos = await _repositorioCajaChica.ObtenerPorMesAsync(anio, mes);
            foreach (var mov in movimientos.OrderBy(m => m.Fecha))
            {
                sb.AppendLine($"{mov.Fecha:yyyy-MM-dd},\"{mov.Concepto.Replace("\"", "'")}\",{mov.TipoMovimiento},{mov.Monto}");
            }

            return sb.ToString();
        }

        public async Task<string> GenerarReporteFlujoBeneficiariosCsvAsync(DateTime inicio, DateTime fin)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Tipo,Nombre,FechaIngreso,FechaBaja,EstadoActual");

            var todosNinos = await _repositorioNino.ObtenerTodosAsync();

            var altas = todosNinos.Where(n => n.FechaIngreso >= inicio.Date && n.FechaIngreso <= fin.Date);
            foreach (var n in altas.OrderBy(n => n.FechaIngreso))
                sb.AppendLine($"Alta,{n.NombreCompleto},{n.FechaIngreso:yyyy-MM-dd},,{(n.Activo ? "Activo" : "Inactivo")}");

            var bajas = todosNinos.Where(n => n.FechaBaja.HasValue && n.FechaBaja.Value >= inicio.Date && n.FechaBaja.Value <= fin.Date);
            foreach (var n in bajas.OrderBy(n => n.FechaBaja))
                sb.AppendLine($"Baja,{n.NombreCompleto},{n.FechaIngreso:yyyy-MM-dd},{n.FechaBaja!.Value:yyyy-MM-dd},Inactivo");

            return sb.ToString();
        }

        public async Task<string> GenerarReporteVoluntariosCsvAsync(DateTime inicio, DateTime fin)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Voluntario,Estado,Fecha,Actividad,Horas");

            // Incluir inactivos: un voluntario con horas en el periodo debe aparecer
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync(incluirInactivos: true);
            foreach (var vol in voluntarios)
            {
                var horas = await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id);
                foreach (var h in horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin))
                {
                    sb.AppendLine($"{vol.NombreCompleto},{(vol.Activo ? "Activo" : "Inactivo")},{h.Fecha:yyyy-MM-dd},\"{h.Descripcion.Replace("\"", "'")}\",{h.HorasAportadas}");
                }
            }

            return sb.ToString();
        }

        public async Task<string> GenerarReporteAuditoriaCajaChicaCsvAsync(int anio, int mes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("ID_Movimiento,FechaHoraCambio,Usuario,ConceptoOriginal,Detalles");

            var auditorias = await _repositorioCajaChica.ObtenerAuditoriasDetalladasPorMesAsync(anio, mes);
            foreach (var aud in auditorias.OrderBy(a => a.FechaHoraCambio))
            {
                sb.AppendLine($"{aud.IdMovimiento},{aud.FechaHoraCambio:yyyy-MM-dd HH:mm},\"{aud.Usuario}\",\"{aud.ConceptoOriginal.Replace("\"", "'")}\",\"{aud.DetallesDelCambio.Replace("\"", "'")}\"");
            }

            return sb.ToString();
        }

        #endregion

        #region Respaldo

        public async Task RespaldarSistemaFullAsync(string rutaDestino)
        {
            if (!Directory.Exists(rutaDestino))
                Directory.CreateDirectory(rutaDestino);

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var nombreArchivoZip = $"Respaldo_Full_{timestamp}.zip";
            var rutaZipFinal = Path.Combine(rutaDestino, nombreArchivoZip);

            var dbPrincipal = ExtraerRutaDb(_configuracion.GetConnectionString("BaseDatos")!);
            var dbFotos = ExtraerRutaDb(_configuracion.GetConnectionString("BaseDatosFotos")!);

            using (var zip = ZipFile.Open(rutaZipFinal, ZipArchiveMode.Create))
            {
                // Respaldar DBs usando SQLite Backup API para no bloquear archivos en uso
                await RespaldarDbSegura(dbPrincipal, "casaninos_bak.db", zip);
                await RespaldarDbSegura(dbFotos, "casafotos_bak.db", zip);
                
                // Incluir logs si existen
                var rutaLogs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                if (Directory.Exists(rutaLogs))
                {
                    foreach (var file in Directory.GetFiles(rutaLogs))
                    {
                        zip.CreateEntryFromFile(file, Path.Combine("logs", Path.GetFileName(file)));
                    }
                }
            }
        }

        public async Task RestaurarSistemaFullAsync(string rutaZip)
        {
            if (!File.Exists(rutaZip))
                throw new FileNotFoundException("El archivo de respaldo no existe.");

            var dbPrincipal = ExtraerRutaDb(_configuracion.GetConnectionString("BaseDatos")!);
            var dbFotos = ExtraerRutaDb(_configuracion.GetConnectionString("BaseDatosFotos")!);

            using (var zip = ZipFile.OpenRead(rutaZip))
            {
                var entryPrincipal = zip.GetEntry("casaninos_bak.db");
                var entryFotos = zip.GetEntry("casafotos_bak.db");

                if (entryPrincipal == null || entryFotos == null)
                    throw new InvalidOperationException("El archivo ZIP no parece ser un respaldo válido o completo de este sistema.");

                // 1. Limpiar pools para permitir sobrescribir los archivos en disco
                SqliteConnection.ClearAllPools();
                
                // Pequeña espera para asegurar que el SO libere los descriptores
                await Task.Delay(500);

                // 2. Restaurar Bases de Datos (Sobrescribir)
                entryPrincipal.ExtractToFile(dbPrincipal, true);
                entryFotos.ExtractToFile(dbFotos, true);

                // 3. Restaurar logs si están presentes
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var entry in zip.Entries.Where(e => e.FullName.StartsWith("logs/")))
                {
                    var destPath = Path.Combine(baseDir, entry.FullName);
                    var dir = Path.GetDirectoryName(destPath);
                    if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
                    entry.ExtractToFile(destPath, true);
                }
            }
        }

        private async Task RespaldarDbSegura(string rutaDbOriginal, string nombreEnZip, ZipArchive zip)
        {
            var tempFile = Path.GetTempFileName();
            try
            {
                // Usamos Pooling=False para asegurar que el archivo se libere inmediatamente tras el Dispose
                using (var source = new SqliteConnection($"Data Source={rutaDbOriginal}"))
                using (var destination = new SqliteConnection($"Data Source={tempFile};Pooling=False"))
                {
                    await source.OpenAsync();
                    await destination.OpenAsync();
                    source.BackupDatabase(destination);
                }
                
                // Forzamos limpieza de pools por seguridad adicional
                SqliteConnection.ClearAllPools();

                zip.CreateEntryFromFile(tempFile, nombreEnZip);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    // Delay mínimo para permitir al SO liberar el descriptor de archivo
                    await Task.Delay(200);
                    try { File.Delete(tempFile); } catch { /* Limpieza silenciosa */ }
                }
            }
        }

        private string ExtraerRutaDb(string connectionString)
        {
            var parts = connectionString.Split(';');
            var dataSource = parts.FirstOrDefault(p => p.Trim().StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase));
            if (dataSource == null) return connectionString;
            
            var path = dataSource.Substring(dataSource.IndexOf('=') + 1).Trim();
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
            return path;
        }

        #endregion

        #region Helpers QuestPDF

        private void ConfigurarCabecera(PageDescriptor page, string titulo, string subtitulo, Dictionary<string, string>? filtros = null)
        {
            page.Header().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text(titulo).FontSize(16).SemiBold().FontColor(Colors.Indigo.Medium);
                    col.Item().Text(subtitulo).FontSize(10).FontColor(Colors.Grey.Medium);
                    col.Item().PaddingTop(2).Text("ASOCIACIÓN LA CASA DE LOS NIÑOS").FontSize(9).Italic();

                    if (filtros != null && filtros.Any())
                    {
                        col.Item().PaddingTop(5).Text(t =>
                        {
                            foreach (var f in filtros)
                            {
                                t.Span($"{f.Key}: ").SemiBold().FontSize(8);
                                t.Span($"{f.Value}  ").FontSize(8);
                            }
                        });
                    }
                });

                row.ConstantItem(100).AlignRight().Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(8).FontColor(Colors.Grey.Lighten1);
            });
        }

        private void ConfigurarPiePagina(PageDescriptor page)
        {
            page.Footer().AlignCenter().Text(x =>
            {
                x.Span("Página ");
                x.CurrentPageNumber();
                x.Span(" de ");
                x.TotalPages();
            });
        }

        #endregion
        
        #region Preview Methods (Grid Data)
        
        public async Task<IEnumerable<object>> ObtenerDatosAsistenciaAsync(int anio, int mes)
        {
            var asistencia = await _repositorioAsistencia.ObtenerPorMesAsync(anio, mes);
            var idsConRegistro = asistencia.Select(a => a.IdNino).Distinct().ToHashSet();
            var todosNinos = await _repositorioNino.ObtenerTodosAsync();
            var ninos = todosNinos.Where(n => n.Activo || idsConRegistro.Contains(n.Id));

            return ninos.OrderBy(n => n.NombreCompleto).Select(n => {
                var registros = asistencia.Where(a => a.IdNino == n.Id).ToList();
                return (object)new {
                    Beneficiario = n.NombreCompleto,
                    Estado       = n.Activo ? "Activo" : "Inactivo",
                    Presentes    = registros.Count(a => a.Presente),
                    Ausentes     = registros.Count(a => !a.Presente),
                    TotalRegistros = registros.Count
                };
            }).ToList();
        }

        public async Task<IEnumerable<object>> ObtenerDatosCajaChicaAsync(int anio, int mes)
        {
            var movimientos = await _repositorioCajaChica.ObtenerPorMesAsync(anio, mes);
            return movimientos.OrderBy(m => m.Fecha).Select(m => new {
                m.Fecha,
                m.Concepto,
                m.TipoMovimiento,
                Monto = $"₡{m.Monto:N2}"
            }).ToList();
        }

        public async Task<IEnumerable<object>> ObtenerDatosFlujoBeneficiariosAsync(DateTime inicio, DateTime fin)
        {
            var todosNinos = await _repositorioNino.ObtenerTodosAsync();
            var results = new List<object>();

            var altas = todosNinos.Where(n => n.FechaIngreso >= inicio.Date && n.FechaIngreso <= fin.Date);
            foreach (var n in altas.OrderBy(x => x.FechaIngreso))
                results.Add(new { Tipo = "Alta", n.NombreCompleto, FechaEvento = n.FechaIngreso.ToString("dd/MM/yyyy"), EstadoActual = n.Activo ? "Activo" : "Inactivo" });

            var bajas = todosNinos.Where(n => n.FechaBaja.HasValue && n.FechaBaja.Value >= inicio.Date && n.FechaBaja.Value <= fin.Date);
            foreach (var n in bajas.OrderBy(x => x.FechaBaja))
                results.Add(new { Tipo = "Baja", n.NombreCompleto, FechaEvento = n.FechaBaja!.Value.ToString("dd/MM/yyyy"), EstadoActual = "Inactivo" });

            return results;
        }

        public async Task<IEnumerable<object>> ObtenerDatosVoluntariosResumidoAsync(DateTime inicio, DateTime fin)
        {
            // Incluir inactivos: un voluntario con horas en el periodo debe aparecer
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync(incluirInactivos: true);
            var results = new List<object>();

            foreach (var vol in voluntarios)
            {
                var horas = await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id);
                var horasPeriodo = horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin).Sum(h => h.HorasAportadas);
                // Solo incluir en el resumen si tuvo horas o si está activo
                if (horasPeriodo > 0 || vol.Activo)
                    results.Add(new {
                        Voluntario = vol.NombreCompleto,
                        Estado     = vol.Activo ? "Activo" : "Inactivo",
                        TotalHoras = horasPeriodo
                    });
            }
            return results;
        }

        public async Task<IEnumerable<object>> ObtenerDatosVoluntariosDetalladoAsync(DateTime inicio, DateTime fin)
        {
            // Incluir inactivos: un voluntario que trabajó en el periodo debe aparecer
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync(incluirInactivos: true);
            var results = new List<object>();

            foreach (var vol in voluntarios)
            {
                var horas = await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id);
                foreach (var h in horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin))
                {
                    results.Add(new {
                        Voluntario = vol.NombreCompleto,
                        Estado     = vol.Activo ? "Activo" : "Inactivo",
                        h.Fecha,
                        Actividad  = h.Descripcion,
                        Horas      = h.HorasAportadas
                    });
                }
            }
            return results;
        }

        public async Task<IEnumerable<object>> ObtenerDatosAsistenciaIndividualAsync(int idNino, DateTime inicio, DateTime fin)
        {
            // Usar rango real para soportar consultas multi-mes
            var asistencia = (await _repositorioAsistencia.ObtenerPorRangoAsync(inicio.Date, fin.Date))
                .Where(a => a.IdNino == idNino)
                .OrderBy(a => a.Fecha);

            return asistencia.Select(a => (object)new {
                Fecha  = a.Fecha.ToString("dd/MM/yyyy"),
                Estado = a.Presente ? "Presente" : "Ausente"
            }).ToList();
        }

        public async Task<IEnumerable<object>> ObtenerDatosActividadesVoluntarioAsync(int idVoluntario, DateTime inicio, DateTime fin)
        {
            var horas = (await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(idVoluntario))
                .Where(h => h.Fecha >= inicio.Date && h.Fecha <= fin.Date)
                .OrderBy(h => h.Fecha);
            
            return horas.Select(h => new {
                Fecha = h.Fecha.ToString("dd/MM/yyyy"),
                Actividad = h.Descripcion,
                Horas = h.HorasAportadas
            }).ToList();
        }

        public async Task<IEnumerable<object>> ObtenerDatosAuditoriaCajaChicaAsync(int anio, int mes)
        {
            var auditorias = await _repositorioCajaChica.ObtenerAuditoriasDetalladasPorMesAsync(anio, mes);
            return auditorias.OrderByDescending(a => a.FechaHoraCambio).Select(a => new {
                Fecha = a.FechaHoraCambio.ToString("dd/MM HH:mm"),
                a.Usuario,
                Referencia = $"ID {a.IdMovimiento}",
                a.ConceptoOriginal,
                a.DetallesDelCambio
            }).ToList();
        }

        #endregion
    }
}
