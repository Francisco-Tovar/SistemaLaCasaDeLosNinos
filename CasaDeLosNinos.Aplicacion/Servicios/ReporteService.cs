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
        private readonly IConfiguration _configuracion;

        public ReporteService(
            IRepositorioAsistencia repositorioAsistencia,
            IRepositorioCajaChica repositorioCajaChica,
            IRepositorioVoluntario repositorioVoluntario,
            IRepositorioRegistroHoras repositorioRegistroHoras,
            IRepositorioNino repositorioNino,
            IConfiguration configuracion)
        {
            _repositorioAsistencia = repositorioAsistencia;
            _repositorioCajaChica = repositorioCajaChica;
            _repositorioVoluntario = repositorioVoluntario;
            _repositorioRegistroHoras = repositorioRegistroHoras;
            _repositorioNino = repositorioNino;
            _configuracion = configuracion;

            // Configurar licencia de QuestPDF (TCU - Uso Comunitario)
            QuestPDF.Settings.License = LicenseType.Community;
        }

        #region Reportes PDF

        public async Task<byte[]> GenerarReporteAsistenciaPdfAsync(int anio, int mes)
        {
            var periodName = new DateTime(anio, mes, 1).ToString("MMMM yyyy").ToUpper();
            var ninos = await _repositorioNino.ObtenerTodosAsync();
            var asistencia = await _repositorioAsistencia.ObtenerPorMesAsync(anio, mes);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DE ASISTENCIA MENSUAL", periodName);

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

        public async Task<byte[]> GenerarReporteCajaChicaPdfAsync(int anio, int mes)
        {
            var periodName = new DateTime(anio, mes, 1).ToString("MMMM yyyy").ToUpper();
            var movimientos = await _repositorioCajaChica.ObtenerPorMesAsync(anio, mes);
            var saldo = await _repositorioCajaChica.ObtenerSaldoMensualAsync(anio, mes);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DE FISCALIZACIÓN CAJA CHICA", periodName);

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

                            foreach (var mov in movimientos.OrderBy(m => m.Fecha))
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
            });

            return document.GeneratePdf();
        }

        public async Task<byte[]> GenerarReporteVoluntariosResumidoPdfAsync(DateTime inicio, DateTime fin)
        {
            var periodText = $"{inicio:dd/MM/yyyy} Al {fin:dd/MM/yyyy}".ToUpper();
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync();
            
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE RESUMIDO DE VOLUNTARIOS", periodText);

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(1);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Nombre del Voluntario");
                            header.Cell().Element(CellStyle).Text("Total Horas");

                            static IContainer CellStyle(IContainer container) => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                        });

                        foreach (var vol in voluntarios)
                        {
                            var horas = _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id).Result; // Simplificado para este ejemplo
                            var horasPeriodo = horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin).Sum(h => h.HorasAportadas);

                            table.Cell().Element(RowStyle).Text(vol.NombreCompleto);
                            table.Cell().Element(RowStyle).Text(horasPeriodo.ToString("N1"));

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
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    ConfigurarCabecera(page, "REPORTE DETALLADO DE VOLUNTARIOS", periodText);

                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        foreach (var vol in voluntarios)
                        {
                            var horas = _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id).Result
                                .Where(h => h.Fecha >= inicio && h.Fecha <= fin).ToList();

                            if (!horas.Any()) continue;

                            col.Item().PaddingVertical(5).Text(vol.NombreCompleto).SemiBold().FontSize(12).FontColor(Colors.Indigo.Medium);
                            
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(80);
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(60);
                                });

                                foreach (var h in horas)
                                {
                                    table.Cell().Element(RowStyle).Text(h.Fecha.ToString("dd/MM/yyyy"));
                                    table.Cell().Element(RowStyle).Text(h.Descripcion);
                                    table.Cell().Element(RowStyle).Text(h.HorasAportadas.ToString("N1"));

                                    static IContainer RowStyle(IContainer container) => container.PaddingVertical(2).BorderBottom(1).BorderColor(Colors.Grey.Lighten4);
                                }
                            });
                            
                            col.Item().PaddingBottom(10).AlignRight().Text($"Subtotal: {horas.Sum(h => h.HorasAportadas):N1} horas").FontSize(10).Italic();
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
            sb.AppendLine("Beneficiario,Presentes,Ausentes");
            
            var ninos = await _repositorioNino.ObtenerTodosAsync();
            var asistencia = await _repositorioAsistencia.ObtenerPorMesAsync(anio, mes);

            foreach (var nino in ninos.OrderBy(n => n.NombreCompleto))
            {
                var registros = asistencia.Where(a => a.IdNino == nino.Id).ToList();
                sb.AppendLine($"{nino.NombreCompleto},{registros.Count(a => a.Presente)},{registros.Count(a => !a.Presente)}");
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

        public async Task<string> GenerarReporteVoluntariosCsvAsync(DateTime inicio, DateTime fin)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Voluntario,Fecha,Actividad,Horas");

            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync();
            foreach (var vol in voluntarios)
            {
                var horas = await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id);
                foreach (var h in horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin))
                {
                    sb.AppendLine($"{vol.NombreCompleto},{h.Fecha:yyyy-MM-dd},\"{h.Descripcion.Replace("\"", "'")}\",{h.HorasAportadas}");
                }
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

        private void ConfigurarCabecera(PageDescriptor page, string titulo, string subtitulo)
        {
            page.Header().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text(titulo).FontSize(16).SemiBold().FontColor(Colors.Indigo.Medium);
                    col.Item().Text(subtitulo).FontSize(10).FontColor(Colors.Grey.Medium);
                    col.Item().PaddingTop(5).Text("ASOCIACIÓN LA CASA DE LOS NIÑOS").FontSize(9).Italic();
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
            var ninos = await _repositorioNino.ObtenerTodosAsync();
            var asistencia = await _repositorioAsistencia.ObtenerPorMesAsync(anio, mes);

            return ninos.OrderBy(n => n.NombreCompleto).Select(n => {
                var registros = asistencia.Where(a => a.IdNino == n.Id).ToList();
                return new {
                    Beneficiario = n.NombreCompleto,
                    Presentes = registros.Count(a => a.Presente),
                    Ausentes = registros.Count(a => !a.Presente),
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

        public async Task<IEnumerable<object>> ObtenerDatosVoluntariosResumidoAsync(DateTime inicio, DateTime fin)
        {
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync();
            var results = new List<object>();

            foreach (var vol in voluntarios)
            {
                var horas = await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id);
                var horasPeriodo = horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin).Sum(h => h.HorasAportadas);
                
                results.Add(new {
                    Voluntario = vol.NombreCompleto,
                    TotalHoras = horasPeriodo
                });
            }
            return results;
        }

        public async Task<IEnumerable<object>> ObtenerDatosVoluntariosDetalladoAsync(DateTime inicio, DateTime fin)
        {
            var voluntarios = await _repositorioVoluntario.ObtenerTodosAsync();
            var results = new List<object>();

            foreach (var vol in voluntarios)
            {
                var horas = await _repositorioRegistroHoras.ObtenerPorVoluntarioAsync(vol.Id);
                foreach (var h in horas.Where(h => h.Fecha >= inicio && h.Fecha <= fin))
                {
                    results.Add(new {
                        Voluntario = vol.NombreCompleto,
                        h.Fecha,
                        Actividad = h.Descripcion,
                        Horas = h.HorasAportadas
                    });
                }
            }
            return results;
        }

        #endregion
    }
}
