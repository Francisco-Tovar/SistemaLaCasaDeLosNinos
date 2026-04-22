using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmBitacoraSistema : FormBase
    {
        private readonly IServicioAuditoria _servicioAuditoria;
        private List<AuditoriaSistema> _datosOriginales = new();

        public FrmBitacoraSistema(IServicioAuditoria servicioAuditoria, ThemeColors theme)
        {
            InitializeComponent();
            _servicioAuditoria = servicioAuditoria;
            _theme = theme;
            
            this.TieneBordeAcento = true;
            this.EsRedimensionable = false;
            
            ConfigurarGrilla();
            ConfigurarFiltros();
            RefreshTheme(_theme);
        }

        public override void RefreshTheme(ThemeColors theme)
        {
            base.RefreshTheme(theme);
            
            // Forzar colores del tema en el grid si ApplyTheme no los capturó bien
            dgvBitacora.BackgroundColor = theme.ContentBackground;
            dgvBitacora.GridColor = theme.DividerColor;
            dgvBitacora.DefaultCellStyle.BackColor = theme.ContentBackground;
            dgvBitacora.DefaultCellStyle.ForeColor = theme.TextPrimary;
            dgvBitacora.DefaultCellStyle.SelectionBackColor = theme.AccentColor;
            dgvBitacora.DefaultCellStyle.SelectionForeColor = (theme == ThemeConfiguration.DarkTheme) ? Color.Black : Color.White;
            
            dgvBitacora.ColumnHeadersDefaultCellStyle.BackColor = theme.HeaderBackground;
            dgvBitacora.ColumnHeadersDefaultCellStyle.ForeColor = theme.TextPrimary;
            dgvBitacora.AlternatingRowsDefaultCellStyle.BackColor = theme.SurfaceColor;

            panelFooter.BackColor = theme.SurfaceColor;
            lblResultados.ForeColor = theme.TextSecondary;

            // Sincronizar iconos con texto en botones de acción
            btnFiltrar.IconColor = btnFiltrar.ForeColor;
            btnLimpiar.IconColor = btnLimpiar.ForeColor;
        }

        private void ConfigurarGrilla()
        {
            dgvBitacora.AutoGenerateColumns = false;
            dgvBitacora.Columns.Clear();
            
            // Estándar Premium: No redimensionable, cabecera alta, filas limpias
            dgvBitacora.ColumnHeadersHeight = 45;
            dgvBitacora.RowTemplate.Height = 35;
            dgvBitacora.AllowUserToResizeColumns = false;
            dgvBitacora.AllowUserToResizeRows = false;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.MultiSelect = false;
            
            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFecha",
                HeaderText = "Fecha/Hora",
                DataPropertyName = "FechaHora",
                FillWeight = 20,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });

            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colModulo",
                HeaderText = "Módulo",
                DataPropertyName = "Modulo",
                FillWeight = 15
            });

            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAccion",
                HeaderText = "Acción",
                DataPropertyName = "Accion",
                FillWeight = 15
            });

            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUsuario",
                HeaderText = "Usuario",
                DataPropertyName = "NombreUsuario",
                FillWeight = 20
            });

            dgvBitacora.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDetalle",
                HeaderText = "Detalle de la Actividad",
                DataPropertyName = "Detalle",
                FillWeight = 30
            });

            dgvBitacora.CellFormatting += AlFormatearCelda;
        }

        private void AlFormatearCelda(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var fila = dgvBitacora.Rows[e.RowIndex];
            var registro = fila.DataBoundItem as AuditoriaSistema;
            if (registro == null) return;

            // Por defecto, usar el color de texto primario del tema
            e.CellStyle.ForeColor = _theme.TextPrimary;

            // Solo destacar errores críticos
            if (registro.Accion == "Error")
            {
                e.CellStyle.ForeColor = _theme.StatusError;
            }
            
            // Mantener legibilidad absoluta en selección
            if (fila.Selected)
            {
                e.CellStyle.SelectionForeColor = (_theme == ThemeConfiguration.DarkTheme) ? Color.Black : Color.White;
                e.CellStyle.SelectionBackColor = _theme.AccentColor;
            }
        }

        private void ConfigurarFiltros()
        {
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

            cboModulo.Items.Clear();
            cboModulo.Items.Add("Todos");
            cboModulo.Items.AddRange(new[] { "Sistema", "Niños", "Voluntarios", "Usuarios", "Seguridad", "Asistencia", "Caja Chica" });
            cboModulo.SelectedIndex = 0;
        }

        private async void FrmBitacoraSistema_Load(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private async Task CargarDatos()
        {
            try
            {
                lblCargando.Visible = true;
                this.Cursor = Cursors.WaitCursor;

                _datosOriginales = (await _servicioAuditoria.FiltrarAsync(dtpDesde.Value, dtpHasta.Value)).ToList();
                AplicarFiltrosLocales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la bitácora: {ex.Message}", "Error");
            }
            finally
            {
                lblCargando.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void AplicarFiltrosLocales()
        {
            var modulo = cboModulo.Text;
            var busqueda = txtBuscar.Text.Trim().ToLower();

            var filtrados = _datosOriginales.Where(a =>
                (modulo == "Todos" || a.Modulo == modulo) &&
                (string.IsNullOrEmpty(busqueda) || 
                 a.NombreUsuario.ToLower().Contains(busqueda) || 
                 a.Detalle.ToLower().Contains(busqueda) ||
                 a.Accion.ToLower().Contains(busqueda))
            ).ToList();

            dgvBitacora.DataSource = null;
            dgvBitacora.DataSource = filtrados;

            lblResultados.Text = $"Registros encontrados: {filtrados.Count}";
        }

        private async void btnFiltrar_Click(object sender, EventArgs e) => await CargarDatos();
        private void AlCambiarBusqueda(object sender, EventArgs e) => AplicarFiltrosLocales();
        private void btnCerrar_Click(object sender, EventArgs e) => this.Close();
        private void panelCabecera_MouseDown(object sender, MouseEventArgs e) => DragForm();

        private async void btnLimpiar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "¿Desea eliminar los registros de auditoría con más de 90 días de antigüedad?\nEsta acción es irreversible.",
                "Limpiar Historial",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                await _servicioAuditoria.LimpiarHistorialAsync(90);
                await CargarDatos();
                MessageBox.Show("Historial antiguo eliminado correctamente.", "Mantenimiento");
            }
        }
    }
}
