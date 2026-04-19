using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmRegistroHoras : FormBase
    {
        private readonly int _idVoluntario;
        private readonly int _idUsuarioSesion;
        private readonly IServicioRegistroHoras _servicioHoras;

        public FrmRegistroHoras(int idVoluntario, int idUsuarioSesion, IServicioRegistroHoras servicioHoras, ThemeColors theme)
        {
            InitializeComponent();
            _idVoluntario = idVoluntario;
            _idUsuarioSesion = idUsuarioSesion;
            _servicioHoras = servicioHoras;
            _theme = theme;

            this.EsRedimensionable = false;
            this.TieneBordeAcento = true;

            dtpFecha.MaxDate = DateTime.Today;
            
            ThemeEngine.ApplyTheme(this, _theme);
        }

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            decimal horas = numHoras.Value;

            // Validación de regla de negocio: Máximo 8 horas por registro diario
            if (horas > 8)
            {
                lblMensaje.Text = "⚠️ Máximo 8 horas permitidas por día.";
                lblMensaje.ForeColor = _theme.StatusError;
                return;
            }

            if (horas <= 0)
            {
                lblMensaje.Text = "⚠️ La cantidad de horas deben ser mayor a 0.";
                lblMensaje.ForeColor = _theme.StatusError;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtActividad.Text))
            {
                lblMensaje.Text = "⚠️ Debe indicar la actividad/descripción.";
                lblMensaje.ForeColor = _theme.StatusError;
                return;
            }

            try
            {
                var registro = new RegistroHoras
                {
                    IdVoluntario = _idVoluntario,
                    IdUsuario = _idUsuarioSesion,
                    Fecha = dtpFecha.Value.Date,
                    HorasAportadas = horas,
                    Descripcion = txtActividad.Text.Trim()
                };

                await _servicioHoras.RegistrarHorasAsync(registro);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"❌ {ex.Message}";
                lblMensaje.ForeColor = _theme.StatusError;
            }
        }

        private void AlHacerClickEnCancelar(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelCabecera_MouseDown(object sender, MouseEventArgs e)
        {
            DragForm();
        }
    }
}
