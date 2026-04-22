using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Aplicacion.Servicios;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionCajaChica : FormBase
    {
        private readonly IServicioCajaChica _servicioCajaChica;
        private readonly IServicioFoto _servicioFoto;
        private readonly int _idUsuarioSesion;

        private CajaChica _movimiento;
        private bool _esEdicion;

        // Propiedad expuesta (si se ocupa recarga inmediata)
        public CajaChica MovimientoGuardado => _movimiento;

        public FrmEdicionCajaChica(
            IServicioCajaChica servicioCajaChica,
            IServicioFoto servicioFoto,
            int idUsuarioSesion,
            CajaChica? movimientoExistente,
            string tipoPorDefecto,
            ThemeColors theme)
        {
            InitializeComponent();
            _servicioCajaChica = servicioCajaChica;
            _servicioFoto = servicioFoto;
            _idUsuarioSesion = idUsuarioSesion;
            _theme = theme;

            this.TieneBordeAcento = true;
            this.EsRedimensionable = false;

            ThemeEngine.ApplyTheme(this, _theme);

            // Re-aplicar colores específicos que sobreescriben ApplyTheme general
            grpRecibo.ForeColor = _theme.AccentColor;
            btnTomarFoto.ForeColor = _theme.TextPrimary;
            btnTomarFoto.IconColor = _theme.AccentColor;
            btnSubirFoto.ForeColor = _theme.TextPrimary;
            btnSubirFoto.IconColor = _theme.AccentColor;
            btnQuitarFoto.IconColor = _theme.StatusError;

            if (movimientoExistente != null)
            {
                _movimiento = movimientoExistente;
                _esEdicion = true;
                lblTitulo.Text = $"Edición de {_movimiento.TipoMovimiento}";
            }
            else
            {
                _movimiento = new CajaChica { TipoMovimiento = tipoPorDefecto };
                _esEdicion = false;
                lblTitulo.Text = $"Nuevo {tipoPorDefecto}";
            }

            VincularEventos();
        }

        private void VincularEventos()
        {
            this.Load += AlCargarFormulario;
            btnGuardar.Click += AlHacerClickEnGuardar;
            btnCancelar.Click += AlHacerClickEnCancelar;
            btnTomarFoto.Click += AlHacerClickEnTomarFoto;
            btnSubirFoto.Click += AlHacerClickEnSubirFoto;
            btnQuitarFoto.Click += AlHacerClickEnQuitarFoto;
            pnlCabecera.MouseDown += (s, e) => DragForm();
        }

        private async void AlCargarFormulario(object? sender, EventArgs e)
        {
            if (_esEdicion)
            {
                txtConcepto.Text = _movimiento.Concepto;
                numMonto.Value = _movimiento.Monto;
                dtpFecha.Value = _movimiento.Fecha;

                if (_movimiento.IdFotoRecibo.HasValue)
                {
                    await CargarFotoAsync(_movimiento.IdFotoRecibo.Value);
                }
            }
        }

        private async void AlHacerClickEnGuardar(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConcepto.Text))
            {
                MessageBox.Show("El concepto o detalle del recibo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numMonto.Value <= 0)
            {
                MessageBox.Show("El monto de la transacción debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _movimiento.Concepto = txtConcepto.Text.Trim();
            _movimiento.Monto = numMonto.Value;
            _movimiento.Fecha = dtpFecha.Value.Date;

            try
            {
                if (_esEdicion)
                {
                    var (exito, msj) = await _servicioCajaChica.ModificarMovimientoAsync(_movimiento, _idUsuarioSesion);
                    if (!exito)
                    {
                        MessageBox.Show(msj, "Error de Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    _movimiento.IdUsuario = _idUsuarioSesion;
                    await _servicioCajaChica.RegistrarMovimientoAsync(_movimiento, _idUsuarioSesion);
                }

                MessageBox.Show("El movimiento se registró en la contabilidad satisfactoriamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al persistir el registro: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlHacerClickEnCancelar(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void AlHacerClickEnTomarFoto(object? sender, EventArgs e)
        {
            using var frmFoto = new FrmCapturaFoto(_theme);
            if (frmFoto.ShowDialog(this) == DialogResult.OK && frmFoto.ResultadoFoto != null)
            {
                try
                {
                    var bytesFoto = frmFoto.ResultadoFoto;

                    // Generar un ID pseudo-aleatorio o usar el que ya tiene
                    int idFoto = _movimiento.IdFotoRecibo ?? new Random().Next(100000, 999999);

                    await _servicioFoto.GuardarFotoAsync(idFoto, bytesFoto);

                    _movimiento.IdFotoRecibo = idFoto;

                    if (picRecibo.Image != null) picRecibo.Image.Dispose();
                    using var ms = new MemoryStream(bytesFoto);
                    picRecibo.Image = new Bitmap(Image.FromStream(ms));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al archivar la fotografía del recibo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void AlHacerClickEnSubirFoto(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Seleccionar comprobante o recibo";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var bytesFoto = File.ReadAllBytes(ofd.FileName);

                    int idFoto = _movimiento.IdFotoRecibo ?? new Random().Next(100000, 999999);
                    await _servicioFoto.GuardarFotoAsync(idFoto, bytesFoto);
                    _movimiento.IdFotoRecibo = idFoto;

                    if (picRecibo.Image != null) picRecibo.Image.Dispose();
                    using var ms = new MemoryStream(bytesFoto);
                    picRecibo.Image = new Bitmap(Image.FromStream(ms));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AlHacerClickEnQuitarFoto(object? sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de desligar este comprobante/recibo?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _movimiento.IdFotoRecibo = null;
                if (picRecibo.Image != null)
                {
                    picRecibo.Image.Dispose();
                    picRecibo.Image = null;
                }
            }
        }

        private async Task CargarFotoAsync(int idFoto)
        {
            try
            {
                var bytesFoto = await _servicioFoto.ObtenerFotoAsync(idFoto);
                if (bytesFoto != null && bytesFoto.Length > 0)
                {
                    using var ms = new MemoryStream(bytesFoto);
                    picRecibo.Image = new Bitmap(Image.FromStream(ms));
                }
            }
            catch (Exception)
            {
                // Falla silenciosa de UI si la foto está corrompida.
            }
        }
    }
}
