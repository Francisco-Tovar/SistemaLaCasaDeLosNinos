using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmReportes : FormBase
    {
        private readonly IServicioReporte _servicioReporte;
        private readonly IRepositorioNino _repositorioNino;
        private readonly IRepositorioVoluntario _repositorioVoluntario;

        public FrmReportes(IServicioReporte servicioReporte,
            IRepositorioNino repositorioNino,
            IRepositorioVoluntario repositorioVoluntario,
            ThemeColors theme)
        {
            InitializeComponent();
            _servicioReporte = servicioReporte;
            _repositorioNino = repositorioNino;
            _repositorioVoluntario = repositorioVoluntario;
            _theme = theme;

            RefreshTheme(_theme);

            ConfigurarListas();
            VincularEventos();
        }

        private void ConfigurarListas()
        {
            cboTipoReporte.Items.Add("Asistencia Mensual");
            cboTipoReporte.Items.Add("Asistencia Individual (Niño)");
            cboTipoReporte.Items.Add("Fiscalización Caja Chica");
            cboTipoReporte.Items.Add("Voluntarios (Resumido)");
            cboTipoReporte.Items.Add("Voluntarios (Detallado)");
            cboTipoReporte.Items.Add("Actividades Individual (Voluntario)");
            cboTipoReporte.Items.Add("Auditoría Caja Chica");
            cboTipoReporte.Items.Add("Altas y Bajas (Niños)");
            cboTipoReporte.SelectedIndex = 0;

            var meses = new[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
            foreach (var m in meses) cboMes.Items.Add(m);
            cboMes.SelectedIndex = DateTime.Today.Month - 1;

            int anioActual = DateTime.Today.Year;
            for (int i = anioActual - 5; i <= anioActual + 1; i++)
                cboAnio.Items.Add(i.ToString());
            cboAnio.SelectedItem = anioActual.ToString();

            dtpInicio.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpFin.Value = DateTime.Today;
        }

        private void VincularEventos()
        {
            cboTipoReporte.SelectedIndexChanged += AlCambiarTipoReporte;
            btnGenerarPdf.Click += async (s, e) => await GenerarReporteAsync("PDF");
            btnGenerarCsv.Click += async (s, e) => await GenerarReporteAsync("CSV");
            btnVistaPrevia.Click += async (s, e) => await MostrarVistaPreviaAsync();
        }

        private async void AlCambiarTipoReporte(object? sender, EventArgs e)
        {
            string tipo = cboTipoReporte.Text;
            bool esVoluntarioGeneral = tipo == "Voluntarios (Resumido)" || tipo == "Voluntarios (Detallado)";
            bool esIndividual = tipo.Contains("Individual");
            bool esCajaChica = tipo == "Fiscalización Caja Chica";
            bool esAuditoria = tipo == "Auditoría Caja Chica";
            bool esAltasBajas = tipo == "Altas y Bajas (Niños)";

            // Visibilidad de controles de periodo
            // Altas y Bajas y Voluntarios usan rango fecha libre; Asistencia y Caja Chica usan Mes/Año
            bool usaRangoFecha = esVoluntarioGeneral || esIndividual || esAltasBajas;
            lblFiltro1.Text = usaRangoFecha ? "Desde:" : "Periodo:";
            cboMes.Visible = !usaRangoFecha;
            cboAnio.Visible = !usaRangoFecha;
            dtpInicio.Visible = usaRangoFecha;
            lblFiltro2.Visible = usaRangoFecha;
            dtpFin.Visible = usaRangoFecha;

            // Persona ComboBox
            lblPersona.Visible = esIndividual;
            cboPersona.Visible = esIndividual;

            // Checkbox para imágenes en Caja Chica
            chkIncluirFotos.Visible = esCajaChica;

            if (esIndividual)
            {
                await CargarPersonasAsync(tipo.Contains("Niño"));
            }

            // Ajuste de posiciones
            lblFiltro1.Location = usaRangoFecha ? new System.Drawing.Point(261, 12) : new System.Drawing.Point(10, 12);
        }

        private async Task CargarPersonasAsync(bool esNino)
        {
            cboPersona.Items.Clear();
            if (esNino)
            {
                var ninos = await _repositorioNino.ObtenerTodosAsync();
                foreach (var n in ninos.OrderBy(x => x.NombreCompleto))
                {
                    cboPersona.Items.Add(new { Id = n.Id, Nombre = (n.Activo ? "" : "[INACTIVO] ") + n.NombreCompleto });
                }
            }
            else
            {
                var vols = await _repositorioVoluntario.ObtenerTodosAsync();
                foreach (var v in vols.OrderBy(x => x.NombreCompleto))
                {
                    cboPersona.Items.Add(new { Id = v.Id, Nombre = (v.Activo ? "" : "[INACTIVO] ") + v.NombreCompleto });
                }
            }
            cboPersona.DisplayMember = "Nombre";
            cboPersona.ValueMember = "Id";
            if (cboPersona.Items.Count > 0) cboPersona.SelectedIndex = 0;
        }

        private async Task GenerarReporteAsync(string formato)
        {
            try
            {
                byte[]? contenidoPdf = null;
                string? contenidoCsv = null;

                string titulo = cboTipoReporte.Text;
                int mes = cboMes.SelectedIndex + 1;
                int anio = int.Parse(cboAnio.SelectedItem?.ToString() ?? DateTime.Today.Year.ToString());
                DateTime inicio = dtpInicio.Value;
                DateTime fin = dtpFin.Value;

                using var sfd = new SaveFileDialog();
                sfd.Title = $"Guardar Reporte {formato}";
                sfd.FileName = GenerarNombreSugerido(titulo, cboPersona.Visible ? cboPersona.Text : null, inicio, fin, anio, mes);
                sfd.Filter = formato == "PDF" ? "Archivo PDF (*.pdf)|*.pdf" : "Archivo CSV (*.csv)|*.csv";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;

                if (formato == "PDF")
                {
                    if (titulo == "Asistencia Mensual")
                        contenidoPdf = await _servicioReporte.GenerarReporteAsistenciaPdfAsync(anio, mes);
                    else if (titulo == "Fiscalización Caja Chica")
                        contenidoPdf = await _servicioReporte.GenerarReporteCajaChicaPdfAsync(anio, mes, chkIncluirFotos.Checked);
                    else if (titulo == "Asistencia Individual (Niño)")
                        contenidoPdf = await _servicioReporte.GenerarReporteAsistenciaIndividualPdfAsync(ObtenerIdPersonaSeleccionada(), inicio, fin);
                    else if (titulo == "Voluntarios (Resumido)")
                        contenidoPdf = await _servicioReporte.GenerarReporteVoluntariosResumidoPdfAsync(inicio, fin);
                    else if (titulo == "Voluntarios (Detallado)")
                        contenidoPdf = await _servicioReporte.GenerarReporteVoluntariosDetalladoPdfAsync(inicio, fin);
                    else if (titulo == "Actividades Individual (Voluntario)")
                        contenidoPdf = await _servicioReporte.GenerarReporteActividadesVoluntarioPdfAsync(ObtenerIdPersonaSeleccionada(), inicio, fin);
                    else if (titulo == "Auditoría Caja Chica")
                        contenidoPdf = await _servicioReporte.GenerarReporteAuditoriaCajaChicaPdfAsync(anio, mes);
                    else if (titulo == "Altas y Bajas (Niños)")
                        contenidoPdf = await _servicioReporte.GenerarReporteFlujoBeneficiariosPdfAsync(inicio, fin);

                    if (contenidoPdf != null)
                        await File.WriteAllBytesAsync(sfd.FileName, contenidoPdf);
                }
                else
                {
                    if (titulo == "Asistencia Mensual")
                        contenidoCsv = await _servicioReporte.GenerarReporteAsistenciaCsvAsync(anio, mes);
                    else if (titulo == "Fiscalización Caja Chica")
                    {
                        contenidoCsv = await _servicioReporte.GenerarReporteCajaChicaCsvAsync(anio, mes);
                        if (chkIncluirFotos.Checked)
                        {
                            await ExportarZipConImagenesAsync(sfd.FileName.Replace(".csv", ".zip"), contenidoCsv, anio, mes);
                            contenidoCsv = null; // Para que no intente guardar el CSV solo
                        }
                    }
                    else if (titulo == "Asistencia Individual (Niño)")
                    {
                        var datos = await _servicioReporte.ObtenerDatosAsistenciaIndividualAsync(ObtenerIdPersonaSeleccionada(), inicio, fin);
                        contenidoCsv = GenerarCsvDeLista(datos);
                    }
                    else if (titulo.Contains("Voluntarios"))
                        contenidoCsv = await _servicioReporte.GenerarReporteVoluntariosCsvAsync(inicio, fin);
                    else if (titulo == "Actividades Individual (Voluntario)")
                    {
                        var datos = await _servicioReporte.ObtenerDatosActividadesVoluntarioAsync(ObtenerIdPersonaSeleccionada(), inicio, fin);
                        contenidoCsv = GenerarCsvDeLista(datos);
                    }
                    else if (titulo == "Auditoría Caja Chica")
                    {
                        contenidoCsv = await _servicioReporte.GenerarReporteAuditoriaCajaChicaCsvAsync(anio, mes);
                    }
                    else if (titulo == "Altas y Bajas (Niños)")
                    {
                        contenidoCsv = await _servicioReporte.GenerarReporteFlujoBeneficiariosCsvAsync(inicio, fin);
                    }

                    if (contenidoCsv != null)
                        await File.WriteAllTextAsync(sfd.FileName, contenidoCsv, System.Text.Encoding.UTF8);
                }

                Cursor = Cursors.Default;
                MessageBox.Show("Reporte generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al generar reporte: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void RefreshTheme(ThemeColors theme)
        {
            base.RefreshTheme(theme);

            // Forzar iconos con el color de acento del tema activo
            btnGenerarPdf.IconColor = theme.AccentColor;
            btnGenerarCsv.IconColor = theme.AccentColor;
            btnVistaPrevia.IconColor = theme.AccentColor;
        }

        private async Task MostrarVistaPreviaAsync()
        {
            try
            {
                string titulo = cboTipoReporte.Text;
                int mes = cboMes.SelectedIndex + 1;
                int anio = int.Parse(cboAnio.SelectedItem?.ToString() ?? DateTime.Today.Year.ToString());
                DateTime inicio = dtpInicio.Value;
                DateTime fin = dtpFin.Value;

                IEnumerable<object>? datos = null;

                Cursor = Cursors.WaitCursor;

                if (titulo == "Asistencia Mensual")
                    datos = await _servicioReporte.ObtenerDatosAsistenciaAsync(anio, mes);
                else if (titulo == "Fiscalización Caja Chica")
                    datos = await _servicioReporte.ObtenerDatosCajaChicaAsync(anio, mes);
                else if (titulo == "Asistencia Individual (Niño)")
                    datos = await _servicioReporte.ObtenerDatosAsistenciaIndividualAsync(ObtenerIdPersonaSeleccionada(), inicio, fin);
                else if (titulo == "Voluntarios (Resumido)")
                    datos = await _servicioReporte.ObtenerDatosVoluntariosResumidoAsync(inicio, fin);
                else if (titulo == "Voluntarios (Detallado)")
                    datos = await _servicioReporte.ObtenerDatosVoluntariosDetalladoAsync(inicio, fin);
                else if (titulo == "Actividades Individual (Voluntario)")
                    datos = await _servicioReporte.ObtenerDatosActividadesVoluntarioAsync(ObtenerIdPersonaSeleccionada(), inicio, fin);
                else if (titulo == "Auditoría Caja Chica")
                    datos = await _servicioReporte.ObtenerDatosAuditoriaCajaChicaAsync(anio, mes);
                else if (titulo == "Altas y Bajas (Niños)")
                    datos = await _servicioReporte.ObtenerDatosFlujoBeneficiariosAsync(inicio, fin);

                // Preparar metadatos para la vista previa
                var metadata = new Dictionary<string, string>();
                if (titulo.Contains("Individual"))
                {
                    metadata.Add("Selección", cboPersona.Text);
                    metadata.Add("Desde", inicio.ToString("dd/MM/yyyy"));
                    metadata.Add("Hasta", fin.ToString("dd/MM/yyyy"));
                }
                else if (titulo == "Asistencia Mensual" || titulo == "Fiscalización Caja Chica" || titulo == "Auditoría Caja Chica")
                {
                    metadata.Add("Mes", cboMes.Text);
                    metadata.Add("Año", anio.ToString());
                }
                else // Voluntarios generales
                {
                    metadata.Add("Desde", inicio.ToString("dd/MM/yyyy"));
                    metadata.Add("Hasta", fin.ToString("dd/MM/yyyy"));
                }

                Cursor = Cursors.Default;

                if (datos != null)
                {
                    using var frm = new FrmVistaPreviaReporte(titulo, datos, _theme, metadata);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al cargar vista previa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerIdPersonaSeleccionada()
        {
            if (cboPersona.SelectedItem == null) return 0;
            dynamic item = cboPersona.SelectedItem;
            return item.Id;
        }

        private string GenerarCsvDeLista(IEnumerable<object> datos)
        {
            var sb = new System.Text.StringBuilder();
            bool headersAdded = false;
            foreach (var item in datos)
            {
                var props = item.GetType().GetProperties();
                if (!headersAdded)
                {
                    sb.AppendLine(string.Join(",", props.Select(p => p.Name)));
                    headersAdded = true;
                }
                sb.AppendLine(string.Join(",", props.Select(p => p.GetValue(item)?.ToString()?.Replace("\"", "'") ?? "")));
            }
            return sb.ToString();
        }

        private async Task ExportarZipConImagenesAsync(string rutaZip, string contenidoCsv, int anio, int mes)
        {
            var imagenes = await _servicioReporte.ObtenerImagenesCajaChicaAsync(anio, mes);
            using (var archive = System.IO.Compression.ZipFile.Open(rutaZip, System.IO.Compression.ZipArchiveMode.Create))
            {
                // Agregar CSV
                var csvEntry = archive.CreateEntry("reporte.csv");
                using (var writer = new StreamWriter(csvEntry.Open()))
                {
                    await writer.WriteAsync(contenidoCsv);
                }

                // Agregar imágenes
                foreach (var img in imagenes)
                {
                    var imgEntry = archive.CreateEntry("comprobantes/" + img.Key);
                    using (var stream = imgEntry.Open())
                    {
                        await stream.WriteAsync(img.Value, 0, img.Value.Length);
                    }
                }
            }
            MessageBox.Show($"Se ha generado un archivo ZIP conteniendo el reporte y {imagenes.Count} comprobantes.", "Exportación Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GenerarNombreSugerido(string tipo, string? persona, DateTime inicio, DateTime fin, int anio, int mes)
        {
            // Limpiar tipo
            string nombreLimpio = tipo.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");

            // Si es individual, limpiar y agregar nombre de persona
            string partePersona = "";
            if (!string.IsNullOrEmpty(persona))
            {
                string p = persona.Replace("[INACTIVO] ", "").Replace(" ", "_");
                partePersona = $"_{p}";
            }

            // Formato de periodo - numérico para mejor orden
            string periodo = "";
            bool esIndividual = tipo.Contains("Individual") || tipo.Contains("Voluntarios");

            if (esIndividual)
            {
                periodo = $"{inicio:yyyy-MM-dd}_al_{fin:yyyy-MM-dd}";
            }
            else
            {
                periodo = $"{anio}-{mes:D2}";
            }

            return $"{nombreLimpio}{partePersona}_{periodo}";
        }
    }
}
