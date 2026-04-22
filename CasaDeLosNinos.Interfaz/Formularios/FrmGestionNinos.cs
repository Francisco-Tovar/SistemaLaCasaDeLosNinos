using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using FontAwesome.Sharp;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmGestionNinos : FormBase
    {
        private readonly IServicioNino _servicioNino;
        private readonly IServicioObservacion _servicioObservacion;
        private readonly IServicioFoto _servicioFoto;
        private readonly int _idUsuarioSesion;


        private List<Nino> _todosLosNinos = new();

        public FrmGestionNinos(
            IServicioNino servicioNino,
            IServicioObservacion servicioObservacion,
            IServicioFoto servicioFoto,
            int idUsuarioSesion,
            ThemeColors theme)
        {
            InitializeComponent();
            _servicioNino = servicioNino;
            _servicioObservacion = servicioObservacion;
            _servicioFoto = servicioFoto;
            _idUsuarioSesion = idUsuarioSesion;
            _theme = theme;
            
            CasaDeLosNinos.Interfaz.Estilos.ThemeEngine.ApplyTheme(this, _theme);

            ConfigurarEstiloGrilla();
            ConfigurarColumnasGrilla();

            btnActualizar.Click += async (_, _) => await CargarNinosAsync();
            chkMostrarInactivos.CheckedChanged += (_, _) => AplicarFiltro();
            grdNinos.SelectionChanged += (_, _) => ActualizarBotonesEstado();
        }

        private void ConfigurarEstiloGrilla()
        {
            // El estilo base es manejado por ThemeEngine.
            // Aquí solo definimos comportamiento funcional.
            grdNinos.ColumnHeadersHeight = 45;
            
            // Bloquear redimensionamiento por el usuario
            grdNinos.AllowUserToResizeColumns = false;
            grdNinos.AllowUserToResizeRows = false;
            grdNinos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
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
            grdNinos.RowTemplate.Height = 35; // Altura estándar premium
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
                             || n.NombreCompleto.ToLowerInvariant().Contains(filtro)
                             || n.NombreEncargado.ToLowerInvariant().Contains(filtro)
                             || n.TelefonoEncargado.Contains(filtro)
                             || n.Direccion.ToLowerInvariant().Contains(filtro)))
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

            btnDesactivar.Text = nino.Activo ? "Desactivar" : "Activar";
            btnDesactivar.IconChar = nino.Activo ? IconChar.UserSlash : IconChar.Check;
            
            btnDesactivar.BackColor = nino.Activo ? _theme.StatusError : _theme.StatusSuccess;
            btnDesactivar.ForeColor = Color.White;
            btnDesactivar.IconColor = Color.White;
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
            using var frm = new FrmEdicionNino(null, _servicioNino, _servicioFoto, _idUsuarioSesion, _theme);
            if (frm.ShowDialog(this) == DialogResult.OK) await CargarNinosAsync();
        }

        private async void AlHacerClickEnEditar(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null) return;

            using var frm = new FrmEdicionNino(nino, _servicioNino, _servicioFoto, _idUsuarioSesion, _theme);
            if (frm.ShowDialog(this) == DialogResult.OK) await CargarNinosAsync();
        }

        private async void AlHacerClickEnCambiarEstado(object sender, EventArgs e)
        {
            var nino = ObtenerNinoSeleccionado();
            if (nino == null) return;

            string accion = nino.Activo ? "desactivar" : "activar";
            if (MessageBox.Show($"¿Desea {accion} a {nino.NombreCompleto}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                await _servicioNino.CambiarEstadoAsync(nino.Id, !nino.Activo, _idUsuarioSesion);
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
