using System;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FrmEdicionNino : Form
    {
        private readonly IServicioNino _servicioNino;
        private readonly Nino?         _ninoExistente;

        public FrmEdicionNino(Nino? ninoExistente, IServicioNino servicioNino)
        {
            InitializeComponent();
            _ninoExistente = ninoExistente;
            _servicioNino  = servicioNino;
            
            ConfigurarEstadoParaEdicion();

            if (_ninoExistente != null)
                HidratarCampos(_ninoExistente);
        }

        private void ConfigurarEstadoParaEdicion()
        {
            bool esEdicion = _ninoExistente != null;
            this.Text = esEdicion ? "Editar Niño — La Casa de los Niños" : "Nuevo Niño — La Casa de los Niños";
            this.lblTitulo.Text = esEdicion ? "✏  Editar Beneficiario" : "＋  Nuevo Beneficiario";
            
            if (cboGenero.Items.Count > 0)
                cboGenero.SelectedIndex = 2; // Default "No especificado"
        }

        private void AlCambiarCheckFecha(object sender, EventArgs e)
            => dtpNacimiento.Enabled = chkTieneFechaNacimiento.Checked;

        private void AlHacerClickEnCancelar(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ══════════════════════════════════════════════════════════════
        // HIDRATACIÓN Y LECTURA
        // ══════════════════════════════════════════════════════════════

        private void HidratarCampos(Nino nino)
        {
            txtNombre.Text    = nino.NombreCompleto;
            txtEncargado.Text = nino.NombreEncargado;
            txtTelefono.Text  = nino.TelefonoEncargado;
            txtDireccion.Text = nino.Direccion;

            var idxGenero = cboGenero.FindStringExact(nino.Genero);
            cboGenero.SelectedIndex = idxGenero >= 0 ? idxGenero : 2;

            if (nino.FechaNacimiento.HasValue)
            {
                chkTieneFechaNacimiento.Checked = true;
                dtpNacimiento.Value             = nino.FechaNacimiento.Value;
            }
        }

        private Nino LeerCampos()
        {
            var nino = _ninoExistente != null
                ? new Nino
                  {
                      Id            = _ninoExistente.Id,
                      Activo        = _ninoExistente.Activo,
                      FechaCreacion = _ninoExistente.FechaCreacion,
                      FechaIngreso  = _ninoExistente.FechaIngreso
                  }
                : new Nino();

            nino.NombreCompleto     = txtNombre.Text.Trim();
            nino.NombreEncargado    = txtEncargado.Text.Trim();
            nino.TelefonoEncargado  = txtTelefono.Text.Trim();
            nino.Direccion          = txtDireccion.Text.Trim();
            nino.Genero             = cboGenero.SelectedItem?.ToString() ?? "No especificado";
            nino.FechaNacimiento    = chkTieneFechaNacimiento.Checked ? dtpNacimiento.Value.Date : null;

            return nino;
        }

        // ══════════════════════════════════════════════════════════════
        // MANEJADORES DE EVENTOS
        // ══════════════════════════════════════════════════════════════

        private async void AlHacerClickEnGuardar(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            btnGuardar.Enabled = false;

            try
            {
                var nino = LeerCampos();
                var (exito, mensaje) = await _servicioNino.GuardarAsync(nino);

                if (exito)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblMensaje.Text = mensaje;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error inesperado: {ex.Message}";
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }
    }
}
