using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmGestionNinos : Form
    {
        private readonly IServicioNino         _servicioNino;
        private readonly IServicioObservacion  _servicioObservacion;
        private readonly int                   _idUsuarioSesion;

        // Cache en memoria para filtrado local
        private List<Nino> _todosLosNinos = new();

        public FrmGestionNinos(
            IServicioNino        servicioNino,
            IServicioObservacion servicioObservacion,
            int                  idUsuarioSesion)
        {
            InitializeComponent();
            _servicioNino        = servicioNino;
            _servicioObservacion = servicioObservacion;
            _idUsuarioSesion     = idUsuarioSesion;

            grdNinos.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 80, 160);
            grdNinos.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
            grdNinos.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            grdNinos.EnableHeadersVisualStyles = false;
            grdNinos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);

            btnActualizar.Click += async (_, _) => await CargarNinosAsync();
            chkMostrarInactivos.CheckedChanged += (_, _) => AplicarFiltro();

            ConfigurarColumnasGrilla();
        }

        private void ConfigurarColumnasGrilla()
        {
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colId",        HeaderText = "ID",          DataPropertyName = "Id",               Width = 50, FillWeight = 5 });
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colNombre",    HeaderText = "Nombre Completo", DataPropertyName = "NombreCompleto", FillWeight = 35 });
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colNacimiento",HeaderText = "Nacimiento",  DataPropertyName = "FechaNacimiento",  FillWeight = 15 });
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colEncargado", HeaderText = "Encargado",   DataPropertyName = "NombreEncargado",  FillWeight = 25 });
            grdNinos.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colTelefono",  HeaderText = "Teléfono",    DataPropertyName = "TelefonoEncargado",FillWeight = 15 });
            grdNinos.Columns.Add(new DataGridViewCheckBoxColumn
                { Name = "colActivo",    HeaderText = "Activo",      DataPropertyName = "Activo",           FillWeight = 8, ReadOnly = true });
        }

        private async void FrmGestionNinos_Load(object sender, EventArgs e)
        {
            await CargarNinosAsync();
        }

        // ══════════════════════════════════════════════════════════════
        // LÓGICA DE DATOS
        // ══════════════════════════════════════════════════════════════

        private async Task CargarNinosAsync()
        {
            try
            {
                _todosLosNinos = (await _servicioNino.ObtenerTodosAsync()).ToList();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los niños:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            lblConteo.Text = $"Mostrando {lista.Count} de {_todosLosNinos.Count} registros";
            ActualizarBotonesEstado();
        }

        private void ActualizarBotonesEstado()
        {
            var nino = ObtenerNinoSeleccionado();
            btnEstado.Text      = (nino?.Activo ?? true) ? "⊘  Desactivar" : "✔  Activar";
            btnEstado.BackColor = (nino?.Activo ?? true)
                ? Color.FromArgb(192, 57, 43)
                : Color.FromArgb(39, 174, 96);
        }

        private Nino? ObtenerNinoSeleccionado()
        {
            if (grdNinos.CurrentRow?.DataBoundItem is Nino nino)
                return nino;
            return null;
        }

        // ══════════════════════════════════════════════════════════════
        // MANEJADORES DE EVENTOS
        // ══════════════════════════════════════════════════════════════

        private void AlCambiarBusqueda(object sender, EventArgs e) => AplicarFiltro();

        private void AlDobleClickEnFila(object sender, DataGridViewCellEventArgs e)
            => AlHacerClickEnEditar(sender, e);

        private async void AlHacerClickEnNuevo(object sender, EventArgs e)
        {
            try
            {
                using var frm = new FrmEdicionNino(null, _servicioNino);
                if (frm.ShowDialog(this) == DialogResult.OK)
                    await CargarNinosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void AlHacerClickEnEditar(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null)
            {
                MessageBox.Show("Seleccione un niño de la lista para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var frm = new FrmEdicionNino(nino, _servicioNino);
                if (frm.ShowDialog(this) == DialogResult.OK)
                    await CargarNinosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el formulario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void AlHacerClickEnCambiarEstado(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null)
            {
                MessageBox.Show("Seleccione un niño de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string accion = nino.Activo ? "desactivar" : "activar";
            var confirmar = MessageBox.Show(
                $"¿Desea {accion} a {nino.NombreCompleto}?",
                "Confirmar cambio de estado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes) return;

            try
            {
                await _servicioNino.CambiarEstadoAsync(nino.Id, !nino.Activo);
                await CargarNinosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar el estado:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlHacerClickEnBitacora(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null)
            {
                MessageBox.Show("Seleccione un niño para ver su bitácora de observaciones.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var frm = new FrmObservaciones(nino, _idUsuarioSesion, _servicioObservacion);
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la bitácora:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
