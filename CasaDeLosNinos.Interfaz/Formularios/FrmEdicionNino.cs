using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlashCap;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionNino : FormBase
    {
        private readonly Nino? _ninoExistente;
        private readonly IServicioNino _servicioNino;
        private readonly IServicioFoto _servicioFoto;
        
        private PictureBox picFoto = null!;
        private byte[]? _imagenNueva;
        private bool _usandoCamara;
        private FlashCap.CaptureDevice? _dispositivo;

        public FrmEdicionNino(Nino? nino, IServicioNino servicioNino, IServicioFoto servicioFoto)
        {
            InitializeComponent();
            _ninoExistente = nino;
            _servicioNino = servicioNino;
            _servicioFoto = servicioFoto;
            
            this.EsRedimensionable = false;
            this.TieneBordeAcento = true;

            ConfigurarControlesFoto();

            if (_ninoExistente != null)
            {
                lblTitulo.Text = "✏  Editar Beneficiario";
                CargarDatos();
            }

            // Aplicar tema
            ThemeEngine.ApplyTheme(this, ThemeEngine.LoadThemePreference());
            
            this.FormClosing += (s, e) => {
                if (_usandoCamara) StopCamera();
            };
        }

        private void ConfigurarControlesFoto()
        {
            // Crear PictureBox dinámico
            picFoto = new PictureBox
            {
                Size = new Size(180, 180),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(45, 45, 81),
                Dock = DockStyle.Fill,
                Margin = new Padding(10, 5, 10, 5)
            };
            
            // Añadir a la tabla: Columna 2, Fila 0, con Span de 4 filas
            tabla.Controls.Add(picFoto, 2, 0);
            tabla.SetRowSpan(picFoto, 4);

            // Contenedor para botones
            var pnlBotonesFoto = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(10, 0, 10, 0)
            };
            tabla.Controls.Add(pnlBotonesFoto, 2, 4);

            var btnSubir = new FontAwesome.Sharp.IconButton
            {
                Size = new Size(60, 35),
                IconChar = FontAwesome.Sharp.IconChar.Upload,
                IconSize = 20,
                IconColor = Color.FromArgb(52, 152, 219),
                FlatStyle = FlatStyle.Flat,
                Text = ""
            };
            btnSubir.FlatAppearance.BorderSize = 0;
            btnSubir.Click += AlHacerClickEnSubir;
            pnlBotonesFoto.Controls.Add(btnSubir);

            var btnCamara = new FontAwesome.Sharp.IconButton
            {
                Size = new Size(60, 35),
                IconChar = FontAwesome.Sharp.IconChar.Camera,
                IconSize = 20,
                IconColor = Color.FromArgb(46, 204, 113),
                FlatStyle = FlatStyle.Flat,
                Text = ""
            };
            btnCamara.FlatAppearance.BorderSize = 0;
            btnCamara.Click += AlHacerClickEnCamara;
            pnlBotonesFoto.Controls.Add(btnCamara);
        }

        private async void CargarDatos()
        {
            if (_ninoExistente == null) return;
            
            // Cargar datos básicos
            txtNombre.Text = _ninoExistente.NombreCompleto;
            txtEncargado.Text = _ninoExistente.NombreEncargado;
            txtTelefono.Text = _ninoExistente.TelefonoEncargado;
            txtDireccion.Text = _ninoExistente.Direccion;

            // Cargar foto
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

        private void AlCambiarCheckFecha(object sender, EventArgs e) => dtpNacimiento.Enabled = chkTieneFechaNacimiento.Checked;

        private bool ValidarCampos()
        {
            errorProvider.Clear();
            bool esValido = true;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errorProvider.SetError(txtNombre, "El nombre del niño es obligatorio.");
                esValido = false;
            }

            if (string.IsNullOrWhiteSpace(txtEncargado.Text))
            {
                errorProvider.SetError(txtEncargado, "El nombre del encargado es obligatorio.");
                esValido = false;
            }

            // Validar teléfono: debe tener 8 dígitos si se ingresa
            string tel = txtTelefono.Text.Trim(); // El control ya excluye literales
            if (!string.IsNullOrWhiteSpace(tel) && tel.Length != 8)
            {
                errorProvider.SetError(txtTelefono, "El teléfono debe tener 8 dígitos.");
                esValido = false;
            }

            if (chkTieneFechaNacimiento.Checked && dtpNacimiento.Value.Date > DateTime.Today)
            {
                errorProvider.SetError(dtpNacimiento, "La fecha de nacimiento no puede ser futura.");
                esValido = false;
            }

            if (!esValido)
            {
                lblMensaje.Text = "⚠️ Revise los campos marcados con error.";
            }
            else
            {
                lblMensaje.Text = string.Empty;
            }

            return esValido;
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
                    // Guardar foto si hay una nueva cargada
                    if (_imagenNueva != null)
                    {
                        await _servicioFoto.GuardarFotoAsync(nino.Id, _imagenNueva);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMensaje.Text = mensaje;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private void AlHacerClickEnSubir(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imagenNueva = File.ReadAllBytes(ofd.FileName);
                using var ms = new MemoryStream(_imagenNueva);
                picFoto.Image = Image.FromStream(ms);
            }
        }

        private async Task IniciarCamara()
        {
            try
            {
                var devices = new CaptureDevices();
                
                // Buscar el primer dispositivo con características válidas
                var deviceDescriptor = devices.EnumerateDescriptors().FirstOrDefault(d => d.Characteristics.Any());
                if (deviceDescriptor == null)
                {
                    MessageBox.Show("No se detectó ninguna cámara compatible.", "Error de Cámara");
                    return;
                }

                var characteristics = deviceDescriptor.Characteristics.First();

                _dispositivo = await deviceDescriptor.OpenAsync(
                    characteristics,
                    OnPixelBufferArrived
                );

                await _dispositivo.StartAsync();
                _usandoCamara = true;
                lblMensaje.Text = "📸 Cámara activa. Haga clic de nuevo para capturar.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se pudo iniciar la cámara: {ex.Message}", "Hardware");
            }
        }

        private void OnPixelBufferArrived(PixelBufferScope scope)
        {
            try
            {
                // Extraer datos crudos del frame
                byte[] imageBytes = scope.Buffer.ExtractImage();
                
                using var ms = new MemoryStream(imageBytes);
                var image = Image.FromStream(ms);
                
                // Sincronizar con el hilo de la UI para actualizar el PictureBox
                picFoto.Invoke((MethodInvoker)delegate {
                    var oldImage = picFoto.Image;
                    picFoto.Image = image;
                    oldImage?.Dispose(); 
                });
            }
            catch (Exception)
            {
                // Ignorar errores durante el cierre o transiciones
            }
            finally
            {
                // Liberar memoria del buffer inmediatamente (Fundamental para estabilidad)
                scope.ReleaseNow(); 
            }
        }

        private void AlHacerClickEnCamara(object? sender, EventArgs e)
        {
            if (_usandoCamara && _dispositivo != null)
            {
                // Capturar frame actual y detener streaming
                if (picFoto.Image != null)
                {
                    using var ms = new MemoryStream();
                    picFoto.Image.Save(ms, ImageFormat.Jpeg);
                    _imagenNueva = ms.ToArray();
                }
                StopCamera();
                lblMensaje.Text = "✅ Foto capturada con éxito.";
            }
            else
            {
                // Iniciar streaming
                _ = IniciarCamara();
            }
        }

        private void StopCamera()
        {
            if (_dispositivo != null)
            {
                try
                {
                    // Intentar cerrar de forma segura
                    _dispositivo.StopAsync().GetAwaiter().GetResult();
                    _dispositivo.Dispose();
                }
                catch { /* Ignorar errores al cerrar */ }
                finally
                {
                    _dispositivo = null;
                    _usandoCamara = false;
                }
            }
        }

        private void FrmEdicionNino_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void AlHacerClickEnCancelar(object sender, EventArgs e) => this.Close();

        private void panelCabecera_MouseDown(object sender, MouseEventArgs e) => DragForm();
    }
}
