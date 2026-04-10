using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using FontAwesome.Sharp;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmGestionNinos : Form
    {
        private readonly IServicioNino _servicioNino;
        private readonly IServicioObservacion _servicioObservacion;
        private readonly int _idUsuarioSesion;

        private List<Nino> _todosLosNinos = new();

        public FrmGestionNinos(
            IServicioNino servicioNino,
            IServicioObservacion servicioObservacion,
            int idUsuarioSesion)
        {
            InitializeComponent();
            _servicioNino = servicioNino;
            _servicioObservacion = servicioObservacion;
            _idUsuarioSesion = idUsuarioSesion;

            ConfigurarEstiloGrilla();
            ConfigurarColumnasGrilla();

            btnActualizar.Click += async (_, _) => await CargarNinosAsync();
            chkMostrarInactivos.CheckedChanged += (_, _) => AplicarFiltro();
        }

        private void ConfigurarEstiloGrilla()
        {
            grdNinos.BackgroundColor = Color.FromArgb(34, 33, 74);
            grdNinos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(26, 25, 62);
            grdNinos.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gainsboro;
            grdNinos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            grdNinos.ColumnHeadersHeight = 35;
            grdNinos.DefaultCellStyle.BackColor = Color.FromArgb(34, 33, 74);
            grdNinos.DefaultCellStyle.ForeColor = Color.Gainsboro;
            grdNinos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(172, 126, 241);
            grdNinos.DefaultCellStyle.SelectionForeColor = Color.White;
            grdNinos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(37, 36, 81);
            grdNinos.GridColor = Color.FromArgb(45, 45, 81);
        }

        private void ConfigurarColumnasGrilla()
        {
            grdNinos.Columns.Clear();
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "ID", DataPropertyName = "Id", Width = 50, FillWeight = 5 });
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre", HeaderText = "Nombre", DataPropertyName = "NombreCompleto", FillWeight = 35 });
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNacimiento", HeaderText = "Nacimiento", DataPropertyName = "FechaNacimiento", FillWeight = 15 });
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEncargado", HeaderText = "Encargado", DataPropertyName = "NombreEncargado", FillWeight = 25 });
            grdNinos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "colActivo", HeaderText = "Activo", DataPropertyName = "Activo", FillWeight = 10 });
        }

        private async void FrmGestionNinos_Load(object sender, EventArgs e)
        {
            await CargarNinosAsync();
        }

        private async Task CargarNinosAsync()
        {
            try
            {
                _todosLosNinos = (await _servicioNino.ObtenerTodosAsync()).ToList();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos:\n{ex.Message}", "Error");
            }
        }

        private void AplicarFiltro()
        {
            var filtro = txtBuscar.Text.Trim().ToLowerInvariant();
            var mostrarInactivos = chkMostrarInactivos.Checked;

            var lista = _todosLosNinos
                .Where(n => (mostrarInactivos || n.Activo)
                         && (string.IsNullOrEmpty(filtro)
                             || n.NombreCompleto.ToLowerInvariant().Contains(filtro)))
                .ToList();

            grdNinos.DataSource = null;
            grdNinos.DataSource = lista;

            lblConteo.Text = $"Registros: {lista.Count}";
            ActualizarBotonesEstado();
        }

        private void ActualizarBotonesEstado()
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null) return;

            btnEstado.Text = nino.Activo ? "Desactivar" : "Activar";
            btnEstado.IconChar = nino.Activo ? IconChar.Ban : IconChar.Check;
            btnEstado.IconColor = nino.Activo ? Color.FromArgb(231, 76, 60) : Color.FromArgb(46, 204, 113);
        }

        private Nino? ObtenerNinoSeleccionado()
        {
            if (grdNinos.CurrentRow?.DataBoundItem is Nino nino)
                return nino;
            return null;
        }

        private void AlCambiarBusqueda(object sender, EventArgs e) => AplicarFiltro();

        private void AlDobleClickEnFila(object sender, DataGridViewCellEventArgs e) => AlHacerClickEnEditar(sender, e);

        private async void AlHacerClickEnNuevo(object sender, EventArgs e)
        {
            using var frm = new FrmEdicionNino(null, _servicioNino);
            if (frm.ShowDialog(this) == DialogResult.OK) await CargarNinosAsync();
        }

        private async void AlHacerClickEnEditar(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null) return;

            using var frm = new FrmEdicionNino(nino, _servicioNino);
            if (frm.ShowDialog(this) == DialogResult.OK) await CargarNinosAsync();
        }

        private async void AlHacerClickEnCambiarEstado(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null) return;

            string accion = nino.Activo ? "desactivar" : "activar";
            if (MessageBox.Show($"¿Desea {accion} a {nino.NombreCompleto}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await _servicioNino.CambiarEstadoAsync(nino.Id, !nino.Activo);
                await CargarNinosAsync();
            }
        }

        private void AlHacerClickEnBitacora(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null) return;

            using var frm = new FrmObservaciones(nino, _idUsuarioSesion, _servicioObservacion);
            frm.ShowDialog(this);
        }
    }
}
