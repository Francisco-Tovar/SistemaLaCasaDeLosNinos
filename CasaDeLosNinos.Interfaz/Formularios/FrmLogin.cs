using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Interfaz.Formularios;

public partial class FrmLogin : Form
{
    private readonly IServicioAutenticacion _servicioAutenticacion;
    private TextBox txtUsuario = null!;
    private TextBox txtContrasenera = null!;
    private Button btnIngresar = null!;
    private Label lblError = null!;

    public Usuario? UsuarioAutenticado { get; private set; }

    public FrmLogin(IServicioAutenticacion servicioAutenticacion)
    {
        _servicioAutenticacion = servicioAutenticacion;
        ConfigurarUI();
    }

    private void ConfigurarUI()
    {
        Text = "Acceso al Sistema — La Casa de los Niños";
        Size = new Size(400, 320);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;
        MinimizeBox = false;
        BackColor = Color.White;

        var panelPrincipal = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(30),
            RowCount = 6,
            ColumnCount = 1
        };

        var lblTitulo = new Label
        {
            Text = "INICIAR SESIÓN",
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            ForeColor = Color.FromArgb(45, 45, 45),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };

        var lblUsuario = new Label { Text = "Nombre de Usuario:", Dock = DockStyle.Bottom };
        txtUsuario = new TextBox { Dock = DockStyle.Top, Font = new Font("Segoe UI", 10) };

        var lblClave = new Label { Text = "Contraseña:", Dock = DockStyle.Bottom, Margin = new Padding(0, 10, 0, 0) };
        txtContrasenera = new TextBox 
        { 
            Dock = DockStyle.Top, 
            UseSystemPasswordChar = true, 
            Font = new Font("Segoe UI", 10) 
        };

        lblError = new Label
        {
            Text = "",
            ForeColor = Color.Firebrick,
            Font = new Font("Segoe UI", 8, FontStyle.Italic),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.TopCenter,
            Visible = false
        };

        btnIngresar = new Button
        {
            Text = "Ingresar",
            Dock = DockStyle.Bottom,
            Height = 40,
            BackColor = Color.FromArgb(0, 122, 204),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnIngresar.FlatAppearance.BorderSize = 0;
        btnIngresar.Click += AlHacerClickEnIngresar;

        panelPrincipal.Controls.Add(lblTitulo);
        panelPrincipal.Controls.Add(lblUsuario);
        panelPrincipal.Controls.Add(txtUsuario);
        panelPrincipal.Controls.Add(lblClave);
        panelPrincipal.Controls.Add(txtContrasenera);
        panelPrincipal.Controls.Add(lblError);
        panelPrincipal.Controls.Add(btnIngresar);

        Controls.Add(panelPrincipal);

        AcceptButton = btnIngresar;
    }

    private async void AlHacerClickEnIngresar(object? sender, EventArgs e)
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
