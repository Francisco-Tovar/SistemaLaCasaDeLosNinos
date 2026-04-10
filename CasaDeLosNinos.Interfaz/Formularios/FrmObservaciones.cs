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
                MessageBox.Show($"Error al cargar: {ex.Message}", "Error");
            }
        }

        private void AgregarControlObservacion(ObservacionDetalleDto obs)
        {
            var theme = ThemeEngine.LoadThemePreference();
            var pnl = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                BackColor = theme.SurfaceColor,
                Padding = new Padding(10),
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblMeta = new Label
            {
                Text = $"{obs.FechaHora:dd/MM/yyyy HH:mm} — Por: {obs.NombreAutor}",
                Dock = DockStyle.Top,
                ForeColor = theme.AccentColor,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Height = 20
            };

            var lblTexto = new Label
            {
                Text = obs.Contenido,
                Dock = DockStyle.Top,
                AutoSize = true,
                ForeColor = theme.TextPrimary,
                Font = new Font("Segoe UI", 9.5f),
                Padding = new Padding(0, 5, 0, 5)
            };

            pnl.Controls.Add(lblTexto);
            pnl.Controls.Add(lblMeta);
            panelHistorial.Controls.Add(pnl);
        }

        private async void AlGuardarObservacion(object sender, EventArgs e)
        {
            var texto = txtNuevaObservacion.Text.Trim();
            if (string.IsNullOrEmpty(texto)) return;

            try
            {
                await _servicioObservacion.RegistrarAsync(_nino.Id, _idUsuarioSesion, texto);

                txtNuevaObservacion.Clear();
                await CargarHistorialAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error");
            }
        }

        private void AlCambiarTexto(object sender, EventArgs e) => lblContador.Text = $"{txtNuevaObservacion.Text.Length} / 2000";

        private void AlHacerClickEnCerrar(object sender, EventArgs e) => this.Close();

        private void panelEncabezado_MouseDown(object sender, MouseEventArgs e) => DragForm();
    }
}
