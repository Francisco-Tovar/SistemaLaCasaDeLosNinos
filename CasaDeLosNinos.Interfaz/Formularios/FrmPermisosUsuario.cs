using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Interfaz.Estilos;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    /// <summary>
    /// Formulario modal para asignar y revocar permisos de acceso a módulos
    /// para un usuario específico. Solo accesible por administradores.
    /// El usuario con Id=1 (admin maestro) no puede gestionarse desde aquí.
    /// </summary>
    public partial class FrmPermisosUsuario : FormBase
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly Usuario _usuario;

        // Mapa: nombre de módulo en BD → checkbox correspondiente
        private Dictionary<string, CheckBox> _mapaModulos = null!;

        private readonly int _idEditorActual;

        public FrmPermisosUsuario(IServicioUsuario servicioUsuario, Usuario usuario, int idEditorActual, ThemeColors theme)
        {
            InitializeComponent();
            _servicioUsuario = servicioUsuario;
            _usuario = usuario;
            _idEditorActual = idEditorActual;
            _theme = theme;

            this.TieneBordeAcento = true;
            this.EsRedimensionable = false;

            ThemeEngine.ApplyTheme(this, _theme);
        }

        private async void FrmPermisosUsuario_Load(object sender, EventArgs e)
        {
            // Información del usuario en el header del body
            lblSubtitulo.Text = _usuario.NombreCompleto;
            lblRolUsuario.Text = $"Rol: {(_usuario.IdRol == 1 ? "Administrador" : "Funcionario")}  |  Usuario: {_usuario.NombreUsuario}";

            // Mapa de módulos → checkboxes
            _mapaModulos = new Dictionary<string, CheckBox>
            {
                ["Ninos"] = chkNinos,
                ["Asistencia"] = chkAsistencia,
                ["Voluntarios"] = chkVoluntarios,
                ["CajaChica"] = chkCajaChica,
                ["Reportes"] = chkReportes,
                ["BitacoraEventos"] = chkBitacoraEventos
            };

            // Cargar permisos actuales y marcar checkboxes
            try
            {
                var permisosActuales = (await _servicioUsuario.ObtenerPermisosAsync(_usuario.Id)).ToHashSet();
                foreach (var entrada in _mapaModulos)
                {
                    entrada.Value.Checked = permisosActuales.Contains(entrada.Key);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los permisos del usuario:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            try
            {
                foreach (var entrada in _mapaModulos)
                {
                    if (entrada.Value.Checked)
                        await _servicioUsuario.OtorgarPermisoAsync(_usuario.Id, entrada.Key, _idEditorActual);
                    else
                        await _servicioUsuario.RevocarPermisoAsync(_usuario.Id, entrada.Key, _idEditorActual);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los permisos:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGuardar.Enabled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void panelCabecera_MouseDown(object sender, MouseEventArgs e) => DragForm();

        private void lblSubtitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
