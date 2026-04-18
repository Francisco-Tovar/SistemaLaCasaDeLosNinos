using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionUsuario : FormBase
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly Usuario? _usuarioEdicion;
        private readonly ThemeColors _theme;

        public FrmEdicionUsuario(IServicioUsuario servicioUsuario, ThemeColors theme, Usuario? usuario = null)
        {
            InitializeComponent();
            _servicioUsuario = servicioUsuario;
            _usuarioEdicion = usuario;
            _theme = theme;

            this.TieneBordeAcento = true;
            this.EsRedimensionable = false;

            CasaDeLosNinos.Interfaz.Estilos.ThemeEngine.ApplyTheme(this, _theme);

            ConfigurarComboRoles();
            
            if (_usuarioEdicion != null)
            {
                lblTitulo.Text = "👤  Editar Usuario";
                CargarDatos();
            }
            else
            {
                lblTitulo.Text = "👤  Nuevo Usuario";
            }
        }

        private void ConfigurarComboRoles()
        {
            var roles = new List<dynamic>
            {
                new { Id = 1, Nombre = "Administrador" },
                new { Id = 2, Nombre = "Funcionario" }
            };
            cmbRol.DataSource = roles;
            cmbRol.DisplayMember = "Nombre";
            cmbRol.ValueMember = "Id";
        }

        private void CargarDatos()
        {
            txtNombreCompleto.Text = _usuarioEdicion!.NombreCompleto;
            txtUsername.Text = _usuarioEdicion.NombreUsuario;
            cmbRol.SelectedValue = _usuarioEdicion.IdRol;
            
            lblPasswordHint.Visible = true;
            lblPasswordHint.Text = "(Dejar vacío para mantener la actual)";
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                var usuario = _usuarioEdicion ?? new Usuario { Activo = true };
                usuario.NombreCompleto = txtNombreCompleto.Text.Trim();
                usuario.NombreUsuario = txtUsername.Text.Trim();
                usuario.IdRol = (int)cmbRol.SelectedValue!;

                string? password = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text;

                if (_usuarioEdicion == null)
                {
                    await _servicioUsuario.CrearAsync(usuario, password!);
                }
                else
                {
                    await _servicioUsuario.ActualizarAsync(usuario, password);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
            {
                MessageBox.Show("El nombre completo es requerido.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("El nombre de usuario es requerido.");
                return false;
            }
            
            if (_usuarioEdicion == null && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("La contraseña es requerida para nuevos usuarios.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text != txtConfirmar.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            bool isPassword = txtPassword.PasswordChar == '●';
            char newChar = isPassword ? '\0' : '●';
            var newIcon = isPassword ? FontAwesome.Sharp.IconChar.EyeSlash : FontAwesome.Sharp.IconChar.Eye;
            
            txtPassword.PasswordChar = newChar;
            txtConfirmar.PasswordChar = newChar;
            btnShowPass.IconChar = newIcon;
        }

        private void panelCabecera_MouseDown(object sender, MouseEventArgs e) => DragForm();
    }
}
