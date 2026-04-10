using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmLogin : FormBase
    {
        private readonly IServicioAutenticacion _servicioAutenticacion;
        public Usuario? UsuarioAutenticado => this.Tag as Usuario;

        public FrmLogin(IServicioAutenticacion servicioAutenticacion)
        {
            InitializeComponent();
            _servicioAutenticacion = servicioAutenticacion;
            ConfigurarLogotipo();
            
            // Permitir arrastrar desde el fondo o el panel lateral
            this.MouseDown += (s, e) => DragForm();
            panelSide.MouseDown += (s, e) => DragForm();

            // Aplicar tema persistente
            ThemeEngine.ApplyTheme(this, ThemeEngine.LoadThemePreference());
        }

        private async void AlHacerClickEnIngresar(object sender, EventArgs e)
        {
            lblError.Visible = false;
            btnIngresar.Enabled = false;

            try
            {
                var usuario = await _servicioAutenticacion.ValidarCredencialesAsync(txtUsuario.Text, txtContrasenera.Text);
                if (usuario != null)
                {
                    this.Tag = usuario;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblError.Visible = true;
                    txtContrasenera.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión:\n{ex.Message}", "Error");
            }
            finally
            {
                btnIngresar.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ConfigurarLogotipo()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "logo.png");
                if (System.IO.File.Exists(path))
                {
                    iconLogo.IconChar = FontAwesome.Sharp.IconChar.None;
                    iconLogo.Image = Image.FromFile(path);
                    iconLogo.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch { }
        }
    }
}
