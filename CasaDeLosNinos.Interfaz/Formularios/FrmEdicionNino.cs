using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Interfaz.Estilos;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionNino : FormBase
    {
        private readonly Nino? _ninoExistente;
        private readonly IServicioNino _servicioNino;
        private readonly IServicioFoto _servicioFoto;

        private PictureBox picFoto = null!;
        private byte[]? _imagenNueva;

        public FrmEdicionNino(Nino? nino, IServicioNino servicioNino, IServicioFoto servicioFoto)
        {
            InitializeComponent();
            _ninoExistente = nino;
            _servicioNino = servicioNino;
            _servicioFoto = servicioFoto;

            this.EsRedimensionable = false;
            this.TieneBordeAcento = true;

            ConfigurarAreaID();

            if (_ninoExistente != null)
            {
                lblTitulo.Text = "✏  Editar Beneficiario";
                CargarDatos();
            }

            ThemeEngine.ApplyTheme(this, ThemeEngine.LoadThemePreference());
        }

        private void ConfigurarAreaID()
        {
            // Panel de Tarjeta de ID (Se apila entre la cabecera y la tabla)
            var pnlID = new Panel
            {
                Dock = DockStyle.Top,
                Height = 110,
                BackColor = Color.FromArgb(40, 40, 80),
                Padding = new Padding(15, 12, 15, 12)
            };
            this.Controls.Add(pnlID);

            // EL ORDEN ES CLAVE para el Docking en WinForms:
            pnlID.SendToBack();          // Evalúa segundo (Debajo de cabecera)
            panelCabecera.SendToBack();  // Evalúa primero (Absoluto Top)
            tabla.BringToFront();        // Evalúa último (Llena el resto)

            // Foto Thumbnail
            picFoto = new PictureBox
            {
                Size = new Size(86, 86),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(30, 30, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, 12)
            };
            pnlID.Controls.Add(picFoto);

            // Botón Capturar
            var btnCaptura = new FontAwesome.Sharp.IconButton
            {
                Size = new Size(32, 32),
                IconChar = FontAwesome.Sharp.IconChar.Camera,
                IconSize = 18,
                IconColor = Color.FromArgb(46, 204, 113),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(115, 12),
                Cursor = Cursors.Hand
            };
            btnCaptura.FlatAppearance.BorderSize = 0;
            btnCaptura.Click += (s, e) => AbrirCapturaCamara();
            pnlID.Controls.Add(btnCaptura);

            // Botón Subir
            var btnUpload = new FontAwesome.Sharp.IconButton
            {
                Size = new Size(32, 32),
                IconChar = FontAwesome.Sharp.IconChar.Upload,
                IconSize = 18,
                IconColor = Color.FromArgb(52, 152, 219),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(115, 48),
                Cursor = Cursors.Hand
            };
            btnUpload.FlatAppearance.BorderSize = 0;
            btnUpload.Click += AlHacerClickEnSubir;
            pnlID.Controls.Add(btnUpload);

            // Etiqueta decorativa
            var lblInfoID = new Label
            {
                Text = "IDENTIFICACIÓN VISUAL",
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 7F, FontStyle.Bold),
                Location = new Point(115, 84),
                AutoSize = true
            };
            pnlID.Controls.Add(lblInfoID);
        }

        private async void CargarDatos()
        {
            if (_ninoExistente == null) return;

            txtNombre.Text = _ninoExistente.NombreCompleto;
            txtEncargado.Text = _ninoExistente.NombreEncargado;
            txtTelefono.Text = _ninoExistente.TelefonoEncargado;
            txtDireccion.Text = _ninoExistente.Direccion;

            var bytes = await _servicioFoto.ObtenerFotoAsync(_ninoExistente.Id);
            if (bytes != null)
            {
                using var ms = new MemoryStream(bytes);
                picFoto.Image = Image.FromStream(ms);
            }

            if (_ninoExistente.FechaNacimiento.HasValue)
            {
                chkTieneFechaNacimiento.Checked = true;
                dtpNacimiento.Value = _ninoExistente.FechaNacimiento.Value;
            }

            cboGenero.SelectedItem = _ninoExistente.Genero switch
            {
                "M" => "Masculino",
                "F" => "Femenino",
                _ => "No especificado"
            };
        }

        private void AbrirCapturaCamara()
        {
            using var frm = new FrmCapturaFoto();
            if (frm.ShowDialog() == DialogResult.OK && frm.ResultadoFoto != null)
            {
                _imagenNueva = frm.ResultadoFoto;
                using var ms = new MemoryStream(_imagenNueva);
                var old = picFoto.Image;
                picFoto.Image = Image.FromStream(ms);
                old?.Dispose();

                lblMensaje.Text = "✅ Foto capturada.";
                lblMensaje.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        private void AlHacerClickEnSubir(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Images|*.jpg;*.jpeg;*.png" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imagenNueva = File.ReadAllBytes(ofd.FileName);
                using var ms = new MemoryStream(_imagenNueva);
                var old = picFoto.Image;
                picFoto.Image = Image.FromStream(ms);
                old?.Dispose();

                lblMensaje.Text = "✅ Foto cargada.";
                lblMensaje.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var nino = _ninoExistente ?? new Nino { Activo = true };
                nino.NombreCompleto = txtNombre.Text.Trim();
                nino.NombreEncargado = txtEncargado.Text.Trim();
                nino.TelefonoEncargado = txtTelefono.Text.Trim();
                nino.Direccion = txtDireccion.Text.Trim();
                nino.FechaNacimiento = chkTieneFechaNacimiento.Checked ? dtpNacimiento.Value.Date : null;
                nino.Genero = cboGenero.Text switch { "Masculino" => "M", "Femenino" => "F", _ => "X" };

                var (exito, mensaje) = await _servicioNino.GuardarAsync(nino);
                if (exito)
                {
                    if (_imagenNueva != null)
                        await _servicioFoto.GuardarFotoAsync(nino.Id, _imagenNueva);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else lblMensaje.Text = mensaje;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private bool ValidarCampos()
        {
            errorProvider.Clear();
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "Nombre obligatorio.");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtEncargado.Text))
            {
                errorProvider.SetError(txtEncargado, "Encargado obligatorio.");
                esValido = false;
            }

            return esValido;
        }

        private void AlCambiarCheckFecha(object sender, EventArgs e) => dtpNacimiento.Enabled = chkTieneFechaNacimiento.Checked;
        private void AlHacerClickEnCancelar(object sender, EventArgs e) => this.Close();
        private void panelCabecera_MouseDown(object sender, MouseEventArgs e) => DragForm();

        private void dtpNacimiento_ValueChanged(object sender, EventArgs e)
        {
            if (dtpNacimiento.Value > DateTime.Today)
            {
                dtpNacimiento.Value = DateTime.Today;
            }
        }
    }
}
