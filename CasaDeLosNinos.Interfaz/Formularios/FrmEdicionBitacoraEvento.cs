using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionBitacoraEvento : FormBase
    {
        private readonly IServicioBitacoraEvento _servicioEvento;
        private readonly int _idUsuarioSesion;
        private readonly BitacoraEvento? _eventoOriginal;
        
        private List<byte[]> _fotosNuevas = new();
        private List<int> _idsFotosAEliminar = new();
        private List<FotoEvento> _fotosExistentes = new();

        public FrmEdicionBitacoraEvento(
            BitacoraEvento? evento,
            IServicioBitacoraEvento servicioEvento,
            int idUsuarioSesion,
            ThemeColors theme)
        {
            InitializeComponent();
            _eventoOriginal = evento;
            _servicioEvento = servicioEvento;
            _idUsuarioSesion = idUsuarioSesion;
            _theme = theme;

            EsRedimensionable = false;
            TieneBordeAcento = true;

            ThemeEngine.ApplyTheme(this, _theme);
        }

        private async void FrmEdicionBitacoraEvento_Load(object sender, EventArgs e)
        {
            if (_eventoOriginal != null)
            {
                lblTituloForm.Text = "Editar Evento";
                dtpFecha.Value = _eventoOriginal.Fecha;
                txtTitulo.Text = _eventoOriginal.Titulo;
                txtDescripcion.Text = _eventoOriginal.Descripcion;

                _fotosExistentes = (await _servicioEvento.ObtenerFotosEventoAsync(_eventoOriginal.Id)).ToList();
                CargarMiniaturas();
            }
            else
            {
                lblTituloForm.Text = "Nuevo Evento";
                dtpFecha.Value = DateTime.Today;
            }
        }

        private void CargarMiniaturas()
        {
            flpFotos.Controls.Clear();

            // Fotos ya en base de datos
            foreach (var foto in _fotosExistentes)
            {
                if (!_idsFotosAEliminar.Contains(foto.Id))
                {
                    AgregarMiniaturaALayout(foto.Imagen, foto.Id);
                }
            }

            // Fotos nuevas aún no guardadas
            foreach (var imgData in _fotosNuevas)
            {
                AgregarMiniaturaALayout(imgData, -1);
            }
        }

        private void AgregarMiniaturaALayout(byte[] data, int id)
        {
            var pnl = new Panel { Size = new Size(100, 120), Padding = new Padding(2) };
            
            var pic = new PictureBox
            {
                Image = ByteToImage(data),
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 80
            };

            var btnEliminar = new Button
            {
                Text = "Quitar",
                Dock = DockStyle.Bottom,
                Height = 25,
                FlatStyle = FlatStyle.Flat,
                BackColor = _theme.StatusError,
                ForeColor = Color.White
            };

            btnEliminar.Click += (s, e) =>
            {
                if (id == -1) // Es una foto nueva
                {
                    _fotosNuevas.Remove(data);
                }
                else // Es una foto existente
                {
                    _idsFotosAEliminar.Add(id);
                }
                CargarMiniaturas();
            };

            pnl.Controls.Add(pic);
            pnl.Controls.Add(btnEliminar);
            flpFotos.Controls.Add(pnl);
        }

        private void AlHacerClickEnSubirFoto(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    _fotosNuevas.Add(File.ReadAllBytes(file));
                }
                CargarMiniaturas();
            }
        }

        private void AlHacerClickEnTomarFoto(object sender, EventArgs e)
        {
            using var frm = new FrmCapturaFoto(_theme);
            if (frm.ShowDialog() == DialogResult.OK && frm.ResultadoFoto != null)
            {
                _fotosNuevas.Add(frm.ResultadoFoto);
                CargarMiniaturas();
            }
        }

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                MessageBox.Show("El título es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                if (_eventoOriginal == null)
                {
                    var nuevoEvento = new BitacoraEvento
                    {
                        Fecha = dtpFecha.Value,
                        Titulo = txtTitulo.Text.Trim(),
                        Descripcion = txtDescripcion.Text.Trim(),
                        IdUsuario = _idUsuarioSesion
                    };
                    await _servicioEvento.RegistrarEventoAsync(nuevoEvento, _fotosNuevas);
                }
                else
                {
                    _eventoOriginal.Fecha = dtpFecha.Value;
                    _eventoOriginal.Titulo = txtTitulo.Text.Trim();
                    _eventoOriginal.Descripcion = txtDescripcion.Text.Trim();
                    await _servicioEvento.ActualizarEventoAsync(_eventoOriginal, _fotosNuevas, _idsFotosAEliminar);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar:\n{ex.Message}", "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AlHacerClickEnCancelar(object sender, EventArgs e)
        {
            Close();
        }

        private void pnlCabecera_MouseDown(object sender, MouseEventArgs e)
        {
            DragForm();
        }

        private Image ByteToImage(byte[] blob)
        {
            using var mStream = new MemoryStream(blob);
            return Image.FromStream(mStream);
        }

        private byte[] ImageToByte(Image img)
        {
            using var mStream = new MemoryStream();
            img.Save(mStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return mStream.ToArray();
        }
        
        public override void RefreshTheme(ThemeColors theme)
        {
            base.RefreshTheme(theme);
            lblTituloForm.ForeColor = theme.TextPrimary;
            btnGuardar.BackColor = theme.StatusSuccess;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.IconColor = Color.White;
            
            btnCancelar.BackColor = theme.StatusError;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.IconColor = Color.White;
            
            btnClose.IconColor = theme.TextPrimary;
        }
    }
}
