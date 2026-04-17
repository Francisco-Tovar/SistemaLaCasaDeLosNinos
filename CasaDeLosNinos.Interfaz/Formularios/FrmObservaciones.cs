using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmObservaciones : FormBase
    {
        private readonly Nino _nino;
        private readonly int _idUsuarioSesion;
        private readonly IServicioObservacion _servicioObservacion;

        public FrmObservaciones(Nino nino, int idUsuarioSesion, IServicioObservacion servicio)
        {
            InitializeComponent();
            _nino = nino;
            _idUsuarioSesion = idUsuarioSesion;
            _servicioObservacion = servicio;
            this.EsRedimensionable = false;
            this.TieneBordeAcento = true;

            lblTitulo.Text = $"📋  Bitácora — {_nino.NombreCompleto}";
            lblAutorInfo.Text = $"Escribiendo como usuario ID: {_idUsuarioSesion}";

            // Aplicar tema
            ThemeEngine.ApplyTheme(this, ThemeEngine.LoadThemePreference());
        }

        private int? _idObservacionEdicion = null;

        private async void FrmObservaciones_Load(object sender, EventArgs e) => await CargarHistorialAsync();

        private async Task CargarHistorialAsync()
        {
            try
            {
                panelHistorial.Controls.Clear();
                var observaciones = await _servicioObservacion.ObtenerHistorialAsync(_nino.Id);

                foreach (var obs in observaciones)
                {
                    AgregarControlObservacion(obs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial: {ex.Message}", "Error");
            }
        }

        private void AgregarControlObservacion(ObservacionDetalleDto obs)
        {
            var theme = ThemeEngine.LoadThemePreference();
            
            // Panel contenedor principal
            var pnl = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                BackColor = theme.SurfaceColor,
                Padding = new Padding(12),
                Margin = new Padding(0, 0, 0, 15)
            };

            // Panel de acciones (Derecha)
            var pnlAcciones = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Top,
                Height = 30
            };

            var btnEliminar = new FontAwesome.Sharp.IconButton
            {
                IconChar = FontAwesome.Sharp.IconChar.TrashAlt,
                IconColor = Color.FromArgb(231, 76, 60),
                IconFont = FontAwesome.Sharp.IconFont.Auto,
                IconSize = 18,
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.Click += async (s, e) => await AlEliminarObservacion(obs.Id);

            var btnEditar = new FontAwesome.Sharp.IconButton
            {
                IconChar = FontAwesome.Sharp.IconChar.Pen,
                IconColor = theme.AccentColor,
                IconFont = FontAwesome.Sharp.IconFont.Auto,
                IconSize = 18,
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.Click += (s, e) => ActivarModoEdicion(obs);

            pnlAcciones.Controls.Add(btnEliminar);
            pnlAcciones.Controls.Add(btnEditar);

            // Metadata
            var lblMeta = new Label
            {
                Text = $"{obs.FechaHora:dd/MM/yyyy HH:mm} — Por: {obs.NombreAutor}",
                Dock = DockStyle.Top,
                ForeColor = theme.AccentColor,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Height = 22
            };

            // Contenido
            var lblTexto = new Label
            {
                Text = obs.Contenido,
                Dock = DockStyle.Top,
                AutoSize = true,
                ForeColor = theme.TextPrimary,
                Font = new Font("Segoe UI", 9.5f),
                Padding = new Padding(0, 5, 0, 5),
                // Crucial para WordWrap en Labels dinámicos:
                MaximumSize = new Size(panelHistorial.Width - 60, 0)
            };

            pnl.Controls.Add(lblTexto);
            pnl.Controls.Add(lblMeta);
            pnl.Controls.Add(pnlAcciones);
            
            panelHistorial.Controls.Add(pnl);
        }

        private void ActivarModoEdicion(ObservacionDetalleDto obs)
        {
            _idObservacionEdicion = obs.Id;
            txtNuevaObservacion.Text = obs.Contenido;
            lblNueva.Text = "✏️ Editando Observación:";
            btnGuardar.Text = "Actualizar";
            btnGuardar.IconColor = Color.FromArgb(52, 152, 219);
            txtNuevaObservacion.Focus();
            txtNuevaObservacion.SelectionStart = txtNuevaObservacion.Text.Length;
        }

        private void CancelarEdicion()
        {
            _idObservacionEdicion = null;
            txtNuevaObservacion.Clear();
            lblNueva.Text = "Nueva Observación:";
            btnGuardar.Text = "Guardar";
            btnGuardar.IconColor = Color.FromArgb(46, 204, 113);
        }

        private async void AlGuardarObservacion(object sender, EventArgs e)
        {
            var texto = txtNuevaObservacion.Text.Trim();
            if (string.IsNullOrEmpty(texto)) return;

            try
            {
                if (_idObservacionEdicion.HasValue)
                {
                    await _servicioObservacion.ActualizarAsync(_idObservacionEdicion.Value, texto);
                    CancelarEdicion();
                }
                else
                {
                    await _servicioObservacion.RegistrarAsync(_nino.Id, _idUsuarioSesion, texto);
                    txtNuevaObservacion.Clear();
                }

                await CargarHistorialAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar: {ex.Message}", "Error");
            }
        }

        private async Task AlEliminarObservacion(int id)
        {
            if (MessageBox.Show("¿Está seguro de eliminar esta observación?", "Confirmar", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _servicioObservacion.EliminarAsync(id);
                    await CargarHistorialAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error");
                }
            }
        }

        private void AlCambiarTexto(object sender, EventArgs e) => lblContador.Text = $"{txtNuevaObservacion.Text.Length} / 2000";

        private void AlHacerClickEnCerrar(object sender, EventArgs e)
        {
            if (_idObservacionEdicion.HasValue)
            {
                if (MessageBox.Show("Hay cambios sin guardar. ¿Desea cerrar de todas formas?", "Aviso", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
            }
            this.Close();
        }

        private void panelEncabezado_MouseDown(object sender, MouseEventArgs e) => DragForm();
    }
}
