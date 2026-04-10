using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionNino : FormBase
    {
        private readonly Nino? _ninoExistente;
        private readonly IServicioNino _servicioNino;

        public FrmEdicionNino(Nino? nino, IServicioNino servicioNino)
        {
            InitializeComponent();
            _ninoExistente = nino;
            _servicioNino = servicioNino;

            if (_ninoExistente != null)
            {
                lblTitulo.Text = "✏  Editar Beneficiario";
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            if (_ninoExistente == null) return;
            txtNombre.Text = _ninoExistente.NombreCompleto;
            txtEncargado.Text = _ninoExistente.NombreEncargado;
            txtTelefono.Text = _ninoExistente.TelefonoEncargado;
            txtDireccion.Text = _ninoExistente.Direccion;

            if (_ninoExistente.FechaNacimiento.HasValue)
            {
                chkTieneFechaNacimiento.Checked = true;
                dtpNacimiento.Value = _ninoExistente.FechaNacimiento.Value;
            }

            cboGenero.SelectedItem = _ninoExistente.Genero switch
            {
                "M" => "Masculino",
                "F" => "Femenino",
                _ => "No especificado"
            };
        }

        private void AlCambiarCheckFecha(object sender, EventArgs e) => dtpNacimiento.Enabled = chkTieneFechaNacimiento.Checked;

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblMensaje.Text = "El nombre es obligatorio.";
                return;
            }

            try
            {
                var nino = _ninoExistente ?? new Nino { Activo = true };
                nino.NombreCompleto = txtNombre.Text.Trim();
                nino.NombreEncargado = txtEncargado.Text.Trim();
                nino.TelefonoEncargado = txtTelefono.Text.Trim();
                nino.Direccion = txtDireccion.Text.Trim();
                nino.FechaNacimiento = chkTieneFechaNacimiento.Checked ? dtpNacimiento.Value.Date : null;
                nino.Genero = cboGenero.Text switch { "Masculino" => "M", "Femenino" => "F", _ => "X" };

                var (exito, mensaje) = await _servicioNino.GuardarAsync(nino);
                
                if (exito)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMensaje.Text = mensaje;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private void AlHacerClickEnCancelar(object sender, EventArgs e) => this.Close();

        private void panelCabecera_MouseDown(object sender, MouseEventArgs e) => DragForm();
    }
}
