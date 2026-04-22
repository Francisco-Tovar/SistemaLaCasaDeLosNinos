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
            
            var filtrados = new List<Voluntario>();
            foreach (var v in _voluntariosAll)
            {
                if (string.IsNullOrEmpty(filtro) || 
                    v.NombreCompleto.ToLower().Contains(filtro) ||
                    v.Especialidad.ToLower().Contains(filtro))
                {
                    filtrados.Add(v);
                }
            }

            // Ordenar por nombre (Sin lambdas)
            filtrados.Sort(CompararPorNombre);

            dgvVoluntarios.DataSource = filtrados;

            if (dgvVoluntarios.Columns["Id"] != null) dgvVoluntarios.Columns["Id"].Visible = false;
            
            lblStatus.Text = $"Registros: {filtrados.Count}";
            ConfigurarColumnas();
        }
        private int CompararPorNombre(Voluntario v1, Voluntario v2)
        {
            return string.Compare(v1.NombreCompleto, v2.NombreCompleto, StringComparison.OrdinalIgnoreCase);
        }

        private void ConfigurarColumnas()
        {
            // Ocultar Id y Cedula por solicitud
            if (dgvVoluntarios.Columns["Id"] != null) dgvVoluntarios.Columns["Id"].Visible = false;
            if (dgvVoluntarios.Columns["Cedula"] != null) dgvVoluntarios.Columns["Cedula"].Visible = false;
            if (dgvVoluntarios.Columns["FechaIngreso"] != null) dgvVoluntarios.Columns["FechaIngreso"].Visible = false;

            if (dgvVoluntarios.Columns["NombreCompleto"] != null)
            {
                dgvVoluntarios.Columns["NombreCompleto"].HeaderText = "Nombre";
                dgvVoluntarios.Columns["NombreCompleto"].FillWeight = 200; // Más grande
            }

            if (dgvVoluntarios.Columns["Correo"] != null)
                dgvVoluntarios.Columns["Correo"].FillWeight = 180; // Un poco menos

            if (dgvVoluntarios.Columns["Especialidad"] != null)
                dgvVoluntarios.Columns["Especialidad"].FillWeight = 150; // Más espacio para especialidad

            if (dgvVoluntarios.Columns["Telefono"] != null)
                dgvVoluntarios.Columns["Telefono"].FillWeight = 100; // Ajustado

            if (dgvVoluntarios.Columns["ContactoSupervisor"] != null)
                dgvVoluntarios.Columns["ContactoSupervisor"].HeaderText = "Supervisor";

            if (dgvVoluntarios.Columns["Activo"] != null)
            {
                dgvVoluntarios.Columns["Activo"].Width = 60; // Estrecho (ajustado al texto)
                dgvVoluntarios.Columns["Activo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoluntarios.Columns["Activo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
                
            // Asegurar que la columna de Horas existe y está al final
            if (dgvVoluntarios.Columns["TotalHoras"] == null)
            {
                var col = new DataGridViewTextBoxColumn();
                col.Name = "TotalHoras";
                col.HeaderText = "Horas";
                col.ReadOnly = true;
                dgvVoluntarios.Columns.Add(col);
            }

            if (dgvVoluntarios.Columns["TotalHoras"] != null)
            {
                dgvVoluntarios.Columns["TotalHoras"].Width = 60; // Estrecho
                dgvVoluntarios.Columns["TotalHoras"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVoluntarios.Columns["TotalHoras"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Desactivar redimensionamiento
            dgvVoluntarios.AllowUserToResizeColumns = false;
            dgvVoluntarios.AllowUserToResizeRows = false;
            dgvVoluntarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvVoluntarios.ColumnHeadersHeight = 45;
            dgvVoluntarios.RowTemplate.Height = 35;
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

        private async void btnRefrescar_Click(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            FiltrarGrilla();
        }

        private async void chkInactivos_CheckedChanged(object sender, EventArgs e)
        {
            await CargarDatos();
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var frm = new FrmEdicionVoluntario(null, _servicioVoluntario, _idUsuarioSesion, _theme);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                await CargarDatos();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvVoluntarios.CurrentRow?.DataBoundItem is Voluntario vol)
            {
                using var frm = new FrmEdicionVoluntario(vol, _servicioVoluntario, _idUsuarioSesion, _theme);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await CargarDatos();
                }
            }
            else MessageBox.Show("Seleccione un voluntario para editar.", "Aviso");
        }

        private async void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (dgvVoluntarios.CurrentRow?.DataBoundItem is Voluntario vol)
            {
                string accion = vol.Activo ? "desactivar" : "activar";
                var res = MessageBox.Show($"¿Desea {accion} al voluntario {vol.NombreCompleto}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (res == DialogResult.Yes)
                {
                    await _servicioVoluntario.CambiarEstadoAsync(vol.Id, !vol.Activo, _idUsuarioSesion);
                    await CargarDatos();
                }
            }
        }

        private async void btnHoras_Click(object sender, EventArgs e)
        {
            if (dgvVoluntarios.CurrentRow?.DataBoundItem is Voluntario vol)
            {
                using var frm = new FrmBitacoraVoluntario(vol, _idUsuarioSesion, _servicioHoras, _theme);
                frm.ShowDialog();
                await CargarDatos(); // Refrescar para ver el total actualizado en la grilla principal
            }
            else
            {
                MessageBox.Show("Seleccione un voluntario para ver su bitácora.", "Aviso");
            }
        }

        private void dgvVoluntarios_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotonesEstado();
        }

        private void ActualizarBotonesEstado()
        {
            if (dgvVoluntarios.CurrentRow?.DataBoundItem is Voluntario vol)
            {
                btnDesactivar.Text = vol.Activo ? "Desactivar" : "Activar";
                btnDesactivar.IconChar = vol.Activo ? FontAwesome.Sharp.IconChar.UserSlash : FontAwesome.Sharp.IconChar.UserCheck;
                btnDesactivar.BackColor = vol.Activo ? _theme.StatusError : _theme.StatusSuccess;
                btnDesactivar.ForeColor = Color.White;
                btnDesactivar.IconColor = Color.White;
            }
        }

        private void dgvVoluntarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar_Click(sender, e);
            }
        }
    }
}
