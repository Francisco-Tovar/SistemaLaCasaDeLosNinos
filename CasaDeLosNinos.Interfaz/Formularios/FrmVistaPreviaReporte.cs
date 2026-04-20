using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmVistaPreviaReporte : FormBase
    {
        public FrmVistaPreviaReporte(string titulo, IEnumerable<object> datos, ThemeColors theme, Dictionary<string, string>? metadata = null)
        {
            InitializeComponent();
            _theme = theme;
            lblTitulo.Text = $"Vista Previa: {titulo}";
            
            // Estándares de diseño Premium
            this.TieneBordeAcento = true;
            this.MinimumSize = new System.Drawing.Size(700, 450);
            
            // Mostrar metadatos si vienen presentes
            if (metadata != null && metadata.Any())
            {
                lblMetadata.Text = string.Join(" | ", metadata.Select(m => $"{m.Key}: {m.Value}"));
            }
            else
            {
                lblMetadata.Text = "Sin filtros adicionales aplicados.";
            }

            // Movilidad del formulario (arrastre desde el encabezado)
            pnlHeader.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) DragForm(); };
            lblTitulo.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) DragForm(); };
            lblMetadata.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) DragForm(); };

            ThemeEngine.ApplyTheme(this, _theme);
            ConfigurarGrid();
            
            CargarDatos(datos);
            
            btnCerrar.Click += (s, e) => this.Close();
        }

        private void ConfigurarGrid()
        {
            dgvPreview.RowHeadersVisible = false;
            dgvPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPreview.MultiSelect = false;
            dgvPreview.ReadOnly = true;
            dgvPreview.AllowUserToAddRows = false;
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreview.AllowUserToResizeColumns = false;
            dgvPreview.AllowUserToResizeRows = false;
            dgvPreview.AllowUserToOrderColumns = false;
            dgvPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            
            // Estilo específico para preview
            dgvPreview.BackgroundColor = _theme.ContentBackground;
            dgvPreview.ForeColor = _theme.TextPrimary;
            dgvPreview.DefaultCellStyle.BackColor = _theme.ContentBackground;
            dgvPreview.DefaultCellStyle.ForeColor = _theme.TextPrimary;
            dgvPreview.DefaultCellStyle.SelectionBackColor = _theme.AccentColor;
            dgvPreview.DefaultCellStyle.SelectionForeColor = _theme.TextPrimary;
            
            dgvPreview.ColumnHeadersDefaultCellStyle.BackColor = _theme.SurfaceColor;
            dgvPreview.ColumnHeadersDefaultCellStyle.ForeColor = _theme.TextPrimary;
            dgvPreview.EnableHeadersVisualStyles = false;
        }

        private void CargarDatos(IEnumerable<object> datos)
        {
            var dataList = datos.ToList();
            dgvPreview.DataSource = dataList;
            lblRegistros.Text = $"Registros encontrados: {dataList.Count}";
            
            if (dataList.Count == 0)
            {
                MessageBox.Show("No se encontraron datos para los filtros seleccionados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        public override void RefreshTheme(ThemeColors theme)
        {
            base.RefreshTheme(theme);
            btnCerrar.IconColor = theme.AccentColor;
            lblTitulo.ForeColor = theme.AccentColor;
        }
    }
}
