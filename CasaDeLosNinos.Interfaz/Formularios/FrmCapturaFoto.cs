using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlashCap;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmCapturaFoto : FormBase
    {
        private CaptureDevice? _dispositivo;
        private bool _usandoCamara;
        private RotateFlipType _cameraRotation = RotateFlipType.RotateNoneFlipNone;

        public byte[]? ResultadoFoto { get; private set; }

        public FrmCapturaFoto()
        {
            InitializeComponent();
            this.TieneBordeAcento = true;
            this.EsRedimensionable = false;
            
            // Iniciar cámara al abrir
            this.Load += async (s, e) => await IniciarCamara();
        }

        private async Task IniciarCamara()
        {
            try
            {
                var devices = new CaptureDevices();
                var descriptor = devices.EnumerateDescriptors()
                    .FirstOrDefault(d => d.Characteristics != null &&
                                         d.Characteristics.Length > 0 &&
                                         d.Characteristics.Any(c => c.PixelFormat != PixelFormats.Unknown));

                if (descriptor == null)
                {
                    lblMensaje.Text = "⚠️ No se detectó ninguna cámara compatible.";
                    return;
                }

                var caracteristicas = descriptor.Characteristics
                    .Where(c => c.PixelFormat != PixelFormats.Unknown)
                    .OrderBy(c => c.Width * c.Height)
                    .ToList();

                bool iniciada = false;
                foreach (var charact in caracteristicas)
                {
                    try
                    {
                        _dispositivo = await descriptor.OpenAsync(charact, bufferScope =>
                        {
                            if (picPreview.IsDisposed || !picPreview.IsHandleCreated) return;
                            
                            byte[] imageBytes = bufferScope.Buffer.ExtractImage();

                            Bitmap frame;
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                using var temp = Image.FromStream(ms);
                                frame = new Bitmap(temp);
                            }

                            if (_cameraRotation != RotateFlipType.RotateNoneFlipNone)
                                frame.RotateFlip(_cameraRotation);

                            picPreview.BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    var old = picPreview.Image;
                                    picPreview.Image = frame;
                                    old?.Dispose();
                                }
                                catch { frame.Dispose(); }
                            }));
                        });

                        await _dispositivo.StartAsync();
                        _usandoCamara = iniciada = true;
                        break;
                    }
                    catch { _dispositivo?.Dispose(); _dispositivo = null; }
                }

                if (!iniciada) lblMensaje.Text = "⚠️ El hardware rechazó todos los formatos.";
                else lblMensaje.Text = "Cámara lista. Presione el botón verde.";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error: " + ex.Message;
            }
        }

        private void AlHacerClickEnCapturar(object sender, EventArgs e)
        {
            if (picPreview.Image == null) return;

            try
            {
                using var ms = new MemoryStream();
                // Clonar para evitar problemas de stream
                using var clone = new Bitmap(picPreview.Image);
                clone.Save(ms, ImageFormat.Jpeg);
                ResultadoFoto = ms.ToArray();
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al capturar: " + ex.Message);
            }
        }

        private void AlHacerClickEnRotar(object sender, EventArgs e)
        {
            _cameraRotation = _cameraRotation switch
            {
                RotateFlipType.RotateNoneFlipNone => RotateFlipType.Rotate90FlipNone,
                RotateFlipType.Rotate90FlipNone => RotateFlipType.Rotate180FlipNone,
                RotateFlipType.Rotate180FlipNone => RotateFlipType.Rotate270FlipNone,
                _ => RotateFlipType.RotateNoneFlipNone
            };
        }

        private void FrmCapturaFoto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dispositivo != null)
            {
                _usandoCamara = false;
                var d = _dispositivo;
                _dispositivo = null;
                Task.Run(async () => {
                    try { await d.StopAsync(); d.Dispose(); } catch { }
                });
            }
        }
    }
}
