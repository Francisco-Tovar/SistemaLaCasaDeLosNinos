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
        private readonly int _usuarioActualId;


        public FrmEdicionUsuario(IServicioUsuario servicioUsuario, ThemeColors theme, Usuario? usuario = null, int usuarioActualId = 0)
        {
            InitializeComponent();
            _servicioUsuario = servicioUsuario;
            _usuarioEdicion = usuario;
            _usuarioActualId = usuarioActualId;
            _theme = theme;

            this.TieneBordeAcento = true;
            this.EsRedimensionable = false;

            CasaDeLosNinos.Interfaz.Estilos.ThemeEngine.ApplyTheme(this, _theme);

            ConfigurarComboRoles();
            
            txtUsername.TextChanged += (s, e) => {
                int cursor = txtUsername.SelectionStart;
                txtUsername.Text = txtUsername.Text.ToLower();
                txtUsername.SelectionStart = cursor;
            };
            
            if (_usuarioEdicion != null)
            {
                lblTitulo.Text = "👤  Editar Usuario";
                CargarDatos();
                BloquearRolSiCorresponde();
            }
            else
            {
                lblTitulo.Text = "👤  Nuevo Usuario";
            }
        }

        /// <summary>
        /// Deshabilita el combo de Rol cuando el cambio de rol no está permitido:
        /// - Editando al admin maestro (Id=1): nadie puede cambiarle el rol.
        /// - Un admin editándose a sí mismo: no puede degradarse.
        /// </summary>
        private void BloquearRolSiCorresponde()
        {
            if (_usuarioEdicion == null) return;

            bool esMaestro = (_usuarioEdicion.Id == 1);
            bool esAdminEditandoseSiMismo = (_usuarioActualId != 0
                && _usuarioActualId == _usuarioEdicion.Id
                && _usuarioEdicion.IdRol == 1);

            if (esMaestro || esAdminEditandoseSiMismo)
            {
                cmbRol.Enabled = false;
                string razon = esMaestro
                    ? "El rol del administrador maestro no puede modificarse."
                    : "Un administrador no puede cambiar su propio rol.";
                cmbRol.Tag = razon; // Guardamos para mostrar si intentan hacer click
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
                    await _servicioUsuario.ActualizarAsync(usuario, password, _usuarioActualId);
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
