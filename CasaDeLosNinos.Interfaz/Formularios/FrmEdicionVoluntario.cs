using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionVoluntario : FormBase
    {
        private readonly Voluntario? _voluntarioExistente;
        private readonly IServicioVoluntario _servicioVoluntario;

        public FrmEdicionVoluntario(Voluntario? voluntario, IServicioVoluntario servicioVoluntario, ThemeColors theme)
        {
            InitializeComponent();
            _voluntarioExistente = voluntario;
            _servicioVoluntario = servicioVoluntario;
            _theme = theme;
            
            this.EsRedimensionable = false;
            this.TieneBordeAcento = true;

            ConfigurarAreaCabecera();

            if (_voluntarioExistente != null)
            {
                lblTitulo.Text = "✏  Editar Voluntario";
                CargarDatos();
            }
            else
            {
                cboTipoCedula.SelectedIndex = 0; // Default: Nacional
            }

            ThemeEngine.ApplyTheme(this, _theme);
        }

        private void ConfigurarAreaCabecera()
        {
            // Panel Decorativo debajo de la cabecera (Estilo FrmEdicionNino)
            var pnlInfo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = _theme.HeaderBackground,
                Padding = new Padding(15, 10, 15, 10),
                Name = "pnlHeaderInfo"
            };
            this.Controls.Add(pnlInfo);

            pnlInfo.SendToBack();
            panelCabecera.SendToBack();
            tabla.BringToFront();

            var icnUser = new FontAwesome.Sharp.IconPictureBox
            {
                IconChar = FontAwesome.Sharp.IconChar.UserTie,
                IconColor = _theme.AccentColor,
                IconSize = 40,
                Size = new Size(40, 40),
                Location = new Point(20, 10),
                BackColor = Color.Transparent
            };
            pnlInfo.Controls.Add(icnUser);

            var lblSubtitle = new Label
            {
                Text = "DATOS DEL COLABORADOR",
                ForeColor = _theme.TextSecondary,
                Font = new Font("Segoe UI", 7F, FontStyle.Bold),
                Location = new Point(70, 5),
                AutoSize = true
            };
            pnlInfo.Controls.Add(lblSubtitle);

            if (_voluntarioExistente != null)
            {
                var lblFechaIngreso = new Label
                {
                    Text = $"FECHA DE INGRESO: {_voluntarioExistente.FechaIngreso:dd/MM/yyyy}",
                    ForeColor = _theme.TextSecondary,
                    Font = new Font("Segoe UI", 4.2F),
                    Location = new Point(70, 30),
                    AutoSize = true,
                    Name = "lblFechaIngreso"
                };
                pnlInfo.Controls.Add(lblFechaIngreso);
            }
        }

        private void CargarDatos()
        {
            if (_voluntarioExistente == null) return;

            txtNombre.Text = _voluntarioExistente.NombreCompleto;
            txtEspecialidad.Text = _voluntarioExistente.Especialidad;
            txtTelefono.Text = _voluntarioExistente.Telefono;
            txtCorreo.Text = _voluntarioExistente.Correo;
            txtInstitucion.Text = _voluntarioExistente.Institucion;
            txtSupervisor.Text = _voluntarioExistente.ContactoSupervisor;

            // Lógica para detectar tipo de cédula
            if (!string.IsNullOrEmpty(_voluntarioExistente.Cedula))
            {
                if (_voluntarioExistente.Cedula.Length > 9)
                {
                    cboTipoCedula.SelectedItem = "DIM";
                }
                else
                {
                    cboTipoCedula.SelectedItem = "CR";
                }
                txtCedula.Text = _voluntarioExistente.Cedula;
            }
            else
            {
                cboTipoCedula.SelectedIndex = 0;
            }
        }

        private void AlCambiarTipoCedula(object sender, EventArgs e)
        {
            txtCedula.Clear();
            if (cboTipoCedula.Text == "CR")
            {
                txtCedula.Mask = "0-0000-0000"; // Formato Nacional
            }
            else
            {
                txtCedula.Mask = "000000000000"; // Formato DIMEX (12 dígitos)
            }
        }

        private bool EsCorreoValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true; // Validar solo si tiene contenido
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var voluntario = _voluntarioExistente ?? new Voluntario { Activo = true, FechaIngreso = DateTime.Today };
                voluntario.NombreCompleto = txtNombre.Text.Trim();
                voluntario.Cedula = txtCedula.Text.Trim();
                voluntario.Especialidad = txtEspecialidad.Text.Trim();
                voluntario.Telefono = txtTelefono.Text.Trim();
                voluntario.Correo = txtCorreo.Text.Trim();
                voluntario.Institucion = txtInstitucion.Text.Trim();
                voluntario.ContactoSupervisor = txtSupervisor.Text.Trim();

                if (voluntario.Id == 0)
                {
                    await _servicioVoluntario.CrearAsync(voluntario);
                }
                else
                {
                    await _servicioVoluntario.ActualizarAsync(voluntario);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"❌ {ex.Message}";
                lblMensaje.ForeColor = _theme.StatusError;
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

            if (string.IsNullOrWhiteSpace(txtCorreo.Text) && string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                errorProvider.SetError(txtCorreo, "Debe indicar correo o teléfono.");
                errorProvider.SetError(txtTelefono, "Debe indicar correo o teléfono.");
                esValido = false;
            }

            if (!string.IsNullOrWhiteSpace(txtCorreo.Text) && !EsCorreoValido(txtCorreo.Text))
            {
                errorProvider.SetError(txtCorreo, "Formato de correo inválido (ejemplo@mail.com).");
                esValido = false;
            }

            if (txtCedula.Visible)
            {
                if (string.IsNullOrWhiteSpace(txtCedula.Text))
                {
                    errorProvider.SetError(txtCedula, "La identificación es obligatoria.");
                    esValido = false;
                }
                else if (!txtCedula.MaskCompleted)
                {
                    string tipo = cboTipoCedula.Text == "CR" ? "nacional (9 dígitos)" : "DIMEX (12 dígitos)";
                    errorProvider.SetError(txtCedula, $"La identificación {tipo} está incompleta.");
                    esValido = false;
                }
            }

            return esValido;
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
