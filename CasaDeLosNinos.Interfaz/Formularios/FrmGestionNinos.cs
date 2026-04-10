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
            // El estilo base es manejado por ThemeEngine.
            // Aquí solo definimos comportamiento funcional.
            grdNinos.ColumnHeadersHeight = 35;
        }

        private void ConfigurarColumnasGrilla()
        {
            grdNinos.Columns.Clear();
            grdNinos.AutoGenerateColumns = false;

            grdNinos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colNombre", 
                HeaderText = "Nombre", 
                DataPropertyName = "NombreCompleto", 
                FillWeight = 40 
            });

            grdNinos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colEdad", 
                HeaderText = "Edad", 
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle { WrapMode = DataGridViewTriState.True }
            });

            grdNinos.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "colEncargado", 
                HeaderText = "Encargado", 
                DataPropertyName = "NombreEncargado", 
                FillWeight = 30 
            });

            grdNinos.Columns.Add(new DataGridViewCheckBoxColumn 
            { 
                Name = "colActivo", 
                HeaderText = "Activo", 
                DataPropertyName = "Activo", 
                FillWeight = 10 
            });

            grdNinos.CellFormatting += AlFormatearCelda;
            grdNinos.RowTemplate.Height = 45; // Aumentar altura para wordwrap
        }

        private void AlFormatearCelda(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (grdNinos.Columns[e.ColumnIndex].Name == "colEdad")
            {
                var nino = grdNinos.Rows[e.RowIndex].DataBoundItem as Nino;
                if (nino?.FechaNacimiento != null)
                {
                    var hoy = DateTime.Today;
                    var fechaNac = nino.FechaNacimiento.Value.Date;
                    
                    int edad = hoy.Year - fechaNac.Year;
                    if (fechaNac > hoy.AddYears(-edad)) edad--;

                    var proximoCumple = fechaNac.AddYears(edad + 1);
                    var diasParaCumple = (proximoCumple - hoy).Days;

                    string infoExtra = "";
                    if (hoy.Month == fechaNac.Month && hoy.Day == fechaNac.Day)
                    {
                        infoExtra = "\n(¡Hoy cumple!) 🎉";
                    }
                    else if (diasParaCumple <= 7)
                    {
                        infoExtra = $"\n(en {diasParaCumple} días) 🎂";
                    }

                    e.Value = $"{edad} años{infoExtra}";
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = "-";
                    e.FormattingApplied = true;
                }
            }
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
                .OrderBy(n => n.NombreCompleto)
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
