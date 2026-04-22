using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Interfaz.Estilos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Aplicacion.Servicios;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmGestionUsuarios : FormBase
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly int _usuarioActualId;


        public FrmGestionUsuarios(IServicioUsuario servicioUsuario, int usuarioActualId, ThemeColors theme)
        {
            InitializeComponent();
            _servicioUsuario = servicioUsuario;
            _usuarioActualId = usuarioActualId;
            _theme = theme;
            ThemeEngine.ApplyTheme(this, _theme);
            
            this.Text = "Gestión de Usuarios";
            ConfigurarColumnas();
            dgvUsuarios.CellFormatting += DgvUsuarios_CellFormatting;
        }

        private void DgvUsuarios_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvUsuarios.Rows.Count) return;

            if (dgvUsuarios.Rows[e.RowIndex].DataBoundItem is Usuario user)
            {
                // Mapeo de Rol
                if (dgvUsuarios.Columns[e.ColumnIndex].Name == "ColRol")
                {
                    e.Value = user.IdRol == 1 ? "Administrador" : "Funcionario";
                    e.FormattingApplied = true;
                }

                // Estilo para Inactivos
                if (!user.Activo)
                {
                    e.CellStyle.ForeColor = _theme.TextSecondary;
                    e.CellStyle.Font = new Font(dgvUsuarios.Font, FontStyle.Italic);
                }
            }
        }

        private async void FrmGestionUsuarios_Load(object sender, EventArgs e)
        {
            await CargarUsuarios();
        }

        private void ConfigurarColumnas()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCompleto", HeaderText = "Nombre Completo", FillWeight = 150 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreUsuario", HeaderText = "Usuario", FillWeight = 100 });
            
            var colRol = new DataGridViewTextBoxColumn { HeaderText = "Rol", FillWeight = 100 };
            colRol.Name = "ColRol";
            dgvUsuarios.Columns.Add(colRol);

            var colEstado = new DataGridViewCheckBoxColumn { DataPropertyName = "Activo", HeaderText = "Activo", Width = 70 };
            dgvUsuarios.Columns.Add(colEstado);

            // Desactivar redimensionamiento
            dgvUsuarios.AllowUserToResizeColumns = false;
            dgvUsuarios.AllowUserToResizeRows = false;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsuarios.ColumnHeadersHeight = 45;
            dgvUsuarios.RowTemplate.Height = 35;
        }

        private async Task CargarUsuarios()
        {
            try
            {
                var usuarios = await _servicioUsuario.ObtenerTodosAsync();
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error");
            }
        }

        private async void btnNuevo_Click(object sender, EventArgs e)
        {
            using var frm = new FrmEdicionUsuario(_servicioUsuario, _theme, null, _usuarioActualId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                await CargarUsuarios();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow?.DataBoundItem is Usuario usuario)
            {
                using var frm = new FrmEdicionUsuario(_servicioUsuario, _theme, usuario, _usuarioActualId);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await CargarUsuarios();
                }
            }
        }

        private async void btnDesactivar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow?.DataBoundItem is Usuario usuario)
            {
                if (usuario.Id == _usuarioActualId)
                {
                    MessageBox.Show("No puedes desactivarte a ti mismo.", "Aviso");
                    return;
                }

                string accion = usuario.Activo ? "desactivar" : "activar";
                var result = MessageBox.Show($"¿Desea {accion} al usuario {usuario.NombreUsuario}?", "Confirmar", MessageBoxButtons.YesNo);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _servicioUsuario.CambiarEstadoAsync(usuario.Id, !usuario.Activo, _usuarioActualId);
                        await CargarUsuarios();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            // Filtrado rápido local
            string busqueda = txtBusqueda.Text.ToLower();
            CurrencyManager currencyManager = (CurrencyManager)BindingContext[dgvUsuarios.DataSource];
            currencyManager.SuspendBinding();

            foreach (DataGridViewRow row in dgvUsuarios.Rows)
            {
                var user = (Usuario)row.DataBoundItem;
                bool visible = user.NombreCompleto.ToLower().Contains(busqueda) || 
                               user.NombreUsuario.ToLower().Contains(busqueda);
                row.Visible = visible;
            }

            currencyManager.ResumeBinding();
        }

        private async void btnPermisos_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow?.DataBoundItem is Usuario usuario)
            {
                if (usuario.Id == 1)
                {
                    MessageBox.Show(
                        "Los permisos del administrador maestro no pueden modificarse.",
                        "Operación no permitida",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                using var frm = new FrmPermisosUsuario(_servicioUsuario, usuario, _usuarioActualId, _theme);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Permisos actualizados — no es necesario recargar la lista completa
                    MessageBox.Show(
                        $"Permisos de '{usuario.NombreCompleto}' actualizados correctamente.",
                        "Permisos Guardados",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditar_Click(this.btnEditar, EventArgs.Empty);
            }
        }

        private void dgvUsuarios_SelectionChanged(object sender, EventArgs e)
        {
            ActualizarBotonesEstado();
        }

        private void ActualizarBotonesEstado()
        {
            if (dgvUsuarios.CurrentRow?.DataBoundItem is Usuario user)
            {
                bool esMaestro = (user.Id == 1);

                // Botón Desactivar
                btnDesactivar.Enabled = !esMaestro;
                if (!esMaestro)
                {
                    btnDesactivar.Text = user.Activo ? "Desactivar" : "Activar";
                    btnDesactivar.IconChar = user.Activo ? FontAwesome.Sharp.IconChar.UserSlash : FontAwesome.Sharp.IconChar.UserCheck;
                    btnDesactivar.BackColor = user.Activo ? _theme.StatusError : _theme.StatusSuccess;
                    btnDesactivar.ForeColor = Color.White;
                    btnDesactivar.IconColor = Color.White;
                }
                else
                {
                    btnDesactivar.Text = "Desactivar";
                    btnDesactivar.IconChar = FontAwesome.Sharp.IconChar.UserSlash;
                    btnDesactivar.BackColor = Color.Gray;
                    btnDesactivar.ForeColor = Color.White;
                    btnDesactivar.IconColor = Color.White;
                }

                // Botón Permisos — deshabilitado para el admin maestro
                btnPermisos.Enabled = !esMaestro;
                btnPermisos.BackColor = esMaestro ? Color.Gray : _theme.AccentColor;
                btnPermisos.ForeColor = Color.White;
                btnPermisos.IconColor = Color.White;
            }
        }
    }
}
