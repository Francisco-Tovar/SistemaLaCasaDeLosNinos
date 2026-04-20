using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmReportes : FormBase
    {
        private readonly IServicioReporte _servicioReporte;

        public FrmReportes(IServicioReporte servicioReporte, ThemeColors theme)
        {
            InitializeComponent();
            _servicioReporte = servicioReporte;
            _theme = theme;

            RefreshTheme(_theme);

            ConfigurarListas();
            VincularEventos();
        }

        private void ConfigurarListas()
        {
            cboTipoReporte.Items.Add("Asistencia Mensual");
            cboTipoReporte.Items.Add("Fiscalización Caja Chica");
            cboTipoReporte.Items.Add("Voluntarios (Resumido)");
            cboTipoReporte.Items.Add("Voluntarios (Detallado)");
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
            btnRespaldo.Click += async (s, e) => await RealizarRespaldoAsync();
            btnImportar.Click += async (s, e) => await RealizarRestauracionAsync();
        }

        private void AlCambiarTipoReporte(object? sender, EventArgs e)
        {
            bool esVoluntario = cboTipoReporte.Text.Contains("Voluntarios");

            lblFiltro1.Text = esVoluntario ? "Desde:" : "Periodo:";
            cboMes.Visible = !esVoluntario;
            cboAnio.Visible = !esVoluntario;
            dtpInicio.Visible = esVoluntario;

            lblFiltro2.Visible = esVoluntario;
            dtpFin.Visible = esVoluntario;

            // Mover lblFiltro1 cuando dtpInicio es visible (Reporte de Voluntarios)
            lblFiltro1.Location = esVoluntario ? new System.Drawing.Point(170, 12) : new System.Drawing.Point(10, 12);
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
                sfd.FileName = $"{titulo.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}";
                sfd.Filter = formato == "PDF" ? "Archivo PDF (*.pdf)|*.pdf" : "Archivo CSV (*.csv)|*.csv";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;

                if (formato == "PDF")
                {
                    if (titulo == "Asistencia Mensual")
                        contenidoPdf = await _servicioReporte.GenerarReporteAsistenciaPdfAsync(anio, mes);
                    else if (titulo == "Fiscalización Caja Chica")
                        contenidoPdf = await _servicioReporte.GenerarReporteCajaChicaPdfAsync(anio, mes);
                    else if (titulo == "Voluntarios (Resumido)")
                        contenidoPdf = await _servicioReporte.GenerarReporteVoluntariosResumidoPdfAsync(inicio, fin);
                    else if (titulo == "Voluntarios (Detallado)")
                        contenidoPdf = await _servicioReporte.GenerarReporteVoluntariosDetalladoPdfAsync(inicio, fin);

                    if (contenidoPdf != null)
                        await File.WriteAllBytesAsync(sfd.FileName, contenidoPdf);
                }
                else
                {
                    if (titulo == "Asistencia Mensual")
                        contenidoCsv = await _servicioReporte.GenerarReporteAsistenciaCsvAsync(anio, mes);
                    else if (titulo == "Fiscalización Caja Chica")
                        contenidoCsv = await _servicioReporte.GenerarReporteCajaChicaCsvAsync(anio, mes);
                    else if (titulo.Contains("Voluntarios"))
                        contenidoCsv = await _servicioReporte.GenerarReporteVoluntariosCsvAsync(inicio, fin);

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
            btnRespaldo.IconColor = theme.AccentColor;
            btnImportar.IconColor = theme.AccentColor;
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
                else if (titulo == "Voluntarios (Resumido)")
                    datos = await _servicioReporte.ObtenerDatosVoluntariosResumidoAsync(inicio, fin);
                else if (titulo == "Voluntarios (Detallado)")
                    datos = await _servicioReporte.ObtenerDatosVoluntariosDetalladoAsync(inicio, fin);

                Cursor = Cursors.Default;

                if (datos != null)
                {
                    using var frm = new FrmVistaPreviaReporte(titulo, datos, _theme);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al cargar vista previa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RealizarRespaldoAsync()
        {
            using var fbd = new FolderBrowserDialog();
            fbd.Description = "Seleccione la carpeta donde desea guardar el respaldo.";

            if (fbd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                await _servicioReporte.RespaldarSistemaFullAsync(fbd.SelectedPath);
                Cursor = Cursors.Default;

                MessageBox.Show("Respaldo completado exitosamente.\n\nSe ha generado un archivo .zip con la base de datos y archivos críticos.", "Respaldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al realizar el respaldo: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RealizarRestauracionAsync()
        {
            var confirm = MessageBox.Show("¡ADVERTENCIA CRÍTICA!\n\nAl importar un respaldo se SOBREESCRIBIRÁN todos los datos actuales del sistema. Esta acción no se puede deshacer.\n\n¿Desea continuar?", "Advertencia de Seguridad", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            using var ofd = new OpenFileDialog();
            ofd.Title = "Seleccionar archivo de respaldo (.zip)";
            ofd.Filter = "Respaldo del Sistema (*.zip)|*.zip";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                await _servicioReporte.RestaurarSistemaFullAsync(ofd.FileName);
                Cursor = Cursors.Default;

                MessageBox.Show("Restauración completada.\n\nEl sistema se ha restablecido a partir del respaldo seleccionado. Se recomienda reiniciar la aplicación para asegurar la integridad de las conexiones activas.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error al restaurar el respaldo: {ex.Message}\n\nAsegúrese de que el archivo ZIP sea un respaldo legítimo generado por este sistema.", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
