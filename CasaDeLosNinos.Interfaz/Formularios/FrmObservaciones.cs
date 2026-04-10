using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    /// <summary>
    /// Muestra el historial de observaciones de un niño y permite añadir nuevas.
    /// El IdUsuario del autor y la FechaHora son capturados automáticamente
    /// — el usuario nunca los edita.
    /// </summary>
    public partial class FrmObservaciones : Form
    {
        // ── Dependencias ─────────────────────────────────────────────
        private readonly IServicioObservacion _servicio;
        private readonly Nino                 _nino;
        private readonly int                  _idUsuarioSesion;

        private const int MaxCaracteres = 2000;

        public FrmObservaciones(Nino nino, int idUsuarioSesion, IServicioObservacion servicio)
        {
            InitializeComponent();
            _nino            = nino;
            _idUsuarioSesion = idUsuarioSesion;
            _servicio        = servicio;
        }

        private async void FrmObservaciones_Load(object sender, EventArgs e)
        {
            // Configurar textos dinámicos
            this.Text = $"Bitácora — {_nino.NombreCompleto}";
            this.lblTitulo.Text = $"📋  Bitácora de Observaciones — {_nino.NombreCompleto}";

            await CargarHistorialAsync();
        }

        // ══════════════════════════════════════════════════════════════
        // LÓGICA DE DATOS
        // ══════════════════════════════════════════════════════════════

        private async Task CargarHistorialAsync()
        {
            try
            {
                var lista = (await _servicio.ObtenerHistorialAsync(_nino.Id)).ToList();

                panelHistorial.Controls.Clear();
                int posY = 6;

                if (!lista.Any())
                {
                    var lblVacio = new Label
                    {
                        Text      = "No hay observaciones registradas para este niño.",
                        Font      = new Font("Segoe UI", 9.5f, FontStyle.Italic),
                        ForeColor = Color.FromArgb(150, 150, 170),
                        AutoSize  = true,
                        Location  = new Point(12, posY)
                    };
                    panelHistorial.Controls.Add(lblVacio);
                    return;
                }

                foreach (var obs in lista)
                {
                    var tarjeta = CrearTarjetaObservacion(obs, posY);
                    panelHistorial.Controls.Add(tarjeta);
                    posY += tarjeta.Height + 8;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crea una tarjeta visual con los datos de una observación.
        /// El encabezado muestra autor y fecha/hora — ambos inmutables.
        /// </summary>
        private Panel CrearTarjetaObservacion(ObservacionDetalleDto obs, int posicionY)
        {
            int anchoDisponible = panelHistorial.ClientSize.Width - 20;

            var tarjeta = new Panel
            {
                Location    = new Point(6, posicionY),
                Width       = anchoDisponible,
                BackColor   = Color.FromArgb(240, 245, 255),
                Padding     = new Padding(10, 8, 10, 8),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblEncabezado = new Label
            {
                Text      = $"✍  {obs.NombreAutor}   •   {obs.FechaHora:dd/MM/yyyy HH:mm}",
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 80, 160),
                AutoSize  = true,
                Location  = new Point(8, 6)
            };

            var lblContenido = new Label
            {
                Text      = obs.Contenido,
                Font      = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(40, 40, 60),
                Location  = new Point(8, 26),
                Width     = anchoDisponible - 20,
                AutoSize  = false
            };

            // Calcular la altura dinámica según el contenido del texto
            using var g     = lblContenido.CreateGraphics();
            var       tamano = g.MeasureString(obs.Contenido, lblContenido.Font,
                                               lblContenido.Width);
            lblContenido.Height = (int)Math.Ceiling(tamano.Height) + 6;

            tarjeta.Height = lblContenido.Location.Y + lblContenido.Height + 12;
            tarjeta.Controls.AddRange(new Control[] { lblEncabezado, lblContenido });

            return tarjeta;
        }

        // ══════════════════════════════════════════════════════════════
        // MANEJADORES DE EVENTOS
        // ══════════════════════════════════════════════════════════════

        private void AlCambiarTexto(object sender, EventArgs e)
        {
            int usados = txtNuevaObservacion.TextLength;
            lblContador.Text      = $"{usados} / {MaxCaracteres}";
            lblContador.ForeColor = usados > MaxCaracteres - 100
                ? Color.Firebrick
                : Color.FromArgb(120, 120, 150);
        }

        private async void AlGuardarObservacion(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            try
            {
                // El idUsuarioSesion y DateTime.Now son asignados en el Servicio,
                // no se le pregunta al usuario ni se toman de ningún control.
                await _servicio.RegistrarAsync(_nino.Id, _idUsuarioSesion, txtNuevaObservacion.Text);

                txtNuevaObservacion.Clear();
                await CargarHistorialAsync();

                // Feedback visual de éxito
                btnGuardar.Text      = "✔ Guardado";
                btnGuardar.BackColor = Color.FromArgb(30, 130, 70);
                await Task.Delay(1500);
                btnGuardar.Text      = "💾  Guardar Observación";
                btnGuardar.BackColor = Color.FromArgb(39, 174, 96);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }
    }
}
