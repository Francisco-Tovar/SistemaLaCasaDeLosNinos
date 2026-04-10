using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmLogin : Form
    {
        private readonly IServicioAutenticacion _servicioAutenticacion;

        public Usuario? UsuarioAutenticado { get; private set; }

        public FrmLogin(IServicioAutenticacion servicioAutenticacion)
        {
            InitializeComponent();
            _servicioAutenticacion = servicioAutenticacion;
        }

        private async void AlHacerClickEnIngresar(object sender, EventArgs e)
        {
            btnIngresar.Enabled = false;
            txtUsuario.Enabled = false;
            txtContrasenera.Enabled = false;
            lblError.Visible = false;

            try
            {
                string usuario = txtUsuario.Text.Trim();
                string clave = txtContrasenera.Text;

                var resultado = await _servicioAutenticacion.ValidarCredencialesAsync(usuario, clave);

                if (resultado != null)
                {
                    UsuarioAutenticado = resultado;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblError.Text = "Credenciales incorrectas. Intente de nuevo.";
                    lblError.Visible = true;
                    txtContrasenera.Clear();
                    txtContrasenera.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error durante la autenticación: {ex.Message}", "ErrorCrítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnIngresar.Enabled = true;
                txtUsuario.Enabled = true;
                txtContrasenera.Enabled = true;
            }
        }
    }
}
