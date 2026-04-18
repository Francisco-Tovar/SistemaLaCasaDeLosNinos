using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmGestionVoluntarios : FormBase
    {
        private readonly IServicioVoluntario _servicioVoluntario;
        private readonly IServicioRegistroHoras _servicioHoras;
        private readonly IServiceProvider _proveedor;
        private readonly int _idUsuarioSesion;
        private List<Voluntario> _voluntariosAll = new List<Voluntario>();

        public FrmGestionVoluntarios(
            IServicioVoluntario servicioVoluntario,
            IServicioRegistroHoras servicioHoras,
            IServiceProvider proveedor,
            int idUsuarioSesion,
            ThemeColors theme)
        {
            InitializeComponent();
            _servicioVoluntario = servicioVoluntario;
            _servicioHoras = servicioHoras;
            _proveedor = proveedor;
            _idUsuarioSesion = idUsuarioSesion;
            _theme = theme;
            
            this.TieneBordeAcento = false; // Panel incrustado no requiere borde fuerte
        }

        private async void FrmGestionVoluntarios_Load(object sender, EventArgs e)
        {
            Estilos.ThemeEngine.ApplyTheme(this, _theme);
            await CargarDatos();
        }

        public override void RefreshTheme(ThemeColors theme)
        {
            base.RefreshTheme(theme);
            // DGV ya es procesado por ApplyTheme, pero podemos forzar refresh si hay pintado personalizado
            dgvVoluntarios.Refresh();
        }

        private async Task CargarDatos()
        {
            try
            {
                var vols = await _servicioVoluntario.ObtenerTodosAsync(chkInactivos.Checked);
                _voluntariosAll = vols.ToList();
                FiltrarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar voluntarios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarGrilla()
        {
            string filtro = txtBusqueda.Text.ToLower().Trim();
            
            var filtrados = _voluntariosAll.Where(v => 
                string.IsNullOrEmpty(filtro) || 
                v.NombreCompleto.ToLower().Contains(filtro) ||
                v.Especialidad.ToLower().Contains(filtro)
            ).ToList();

            dgvVoluntarios.DataSource = filtrados;

            if (dgvVoluntarios.Columns["Id"] != null) dgvVoluntarios.Columns["Id"].Visible = false;
            
            lblStatus.Text = $"Registros: {filtrados.Count}";
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            if (dgvVoluntarios.Columns["NombreCompleto"] != null)
                dgvVoluntarios.Columns["NombreCompleto"].HeaderText = "Nombre";
            if (dgvVoluntarios.Columns["FechaIngreso"] != null)
                dgvVoluntarios.Columns["FechaIngreso"].DefaultCellStyle.Format = "dd/MM/yyyy";
                
            // Añadir columna de Total de Horas
            if (dgvVoluntarios.Columns["TotalHoras"] == null)
            {
                var col = new DataGridViewTextBoxColumn();
                col.Name = "TotalHoras";
                col.HeaderText = "Horas";
                col.ReadOnly = true;
                dgvVoluntarios.Columns.Add(col);
            }
        }

        private async void dgvVoluntarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvVoluntarios.Rows[e.RowIndex].DataBoundItem is Voluntario vol)
            {
                // Estilo para inactivos
                if (!vol.Activo)
                {
                    e.CellStyle.ForeColor = _theme.TextSecondary;
                    e.CellStyle.Font = new Font(e.CellStyle.Font ?? dgvVoluntarios.Font, FontStyle.Italic);
                }

                // Calcular horas
                if (dgvVoluntarios.Columns[e.ColumnIndex].Name == "TotalHoras")
                {
                    decimal horas = await _servicioHoras.ObtenerTotalHorasVoluntarioAsync(vol.Id);
                    e.Value = horas.ToString("0.##");
                }
            }
        }

        private async void btnRefrescar_Click(object sender, EventArgs e) => await CargarDatos();
        private void txtBusqueda_TextChanged(object sender, EventArgs e) => FiltrarGrilla();
        private async void chkInactivos_CheckedChanged(object sender, EventArgs e) => await CargarDatos();

        private void btnNuevo_Click(object sender, EventArgs e) { /* TO DO */ }
        private void btnEditar_Click(object sender, EventArgs e) { /* TO DO */ }
        private void btnDesactivar_Click(object sender, EventArgs e) { /* TO DO */ }
        private void btnHoras_Click(object sender, EventArgs e) { /* TO DO */ }
    }
}
