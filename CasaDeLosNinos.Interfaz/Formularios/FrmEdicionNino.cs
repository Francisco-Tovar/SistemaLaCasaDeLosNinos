using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios;

/// <summary>
/// Formulario modal para crear o editar un niño beneficiario.
/// Si recibe un Nino nulo, está en modo "Crear". Si recibe uno existente, en modo "Editar".
/// </summary>
public class FrmEdicionNino : Form
{
    private readonly IServicioNino _servicioNino;
    private readonly Nino?         _ninoExistente;

    // Controles del formulario
    private TextBox         txtNombre         = null!;
    private DateTimePicker  dtpNacimiento     = null!;
    private CheckBox        chkTieneFechaNacimiento = null!;
    private ComboBox        cboGenero         = null!;
    private TextBox         txtEncargado      = null!;
    private TextBox         txtTelefono       = null!;
    private TextBox         txtDireccion      = null!;
    private Button          btnGuardar        = null!;
    private Button          btnCancelar       = null!;
    private Label           lblMensaje        = null!;

    public FrmEdicionNino(Nino? ninoExistente, IServicioNino servicioNino)
    {
        _ninoExistente = ninoExistente;
        _servicioNino  = servicioNino;
        ConfigurarUI();
        if (_ninoExistente != null)
            HidratarCampos(_ninoExistente);
    }

    // ══════════════════════════════════════════════════════════════
    // CONSTRUCCIÓN DE LA INTERFAZ
    // ══════════════════════════════════════════════════════════════

    private void ConfigurarUI()
    {
        bool esEdicion = _ninoExistente != null;

        Text            = esEdicion ? "Editar Niño — La Casa de los Niños" : "Nuevo Niño — La Casa de los Niños";
        Size            = new Size(440, 460);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition   = FormStartPosition.CenterParent;
        MaximizeBox     = false;
        MinimizeBox     = false;
        BackColor       = Color.White;
        Font            = new Font("Segoe UI", 9.5f);

        // Panel de cabecera
        var panelCabecera = new Panel
        {
            Dock      = DockStyle.Top,
            Height    = 48,
            BackColor = Color.FromArgb(30, 80, 160)
        };
        var lblTitulo = new Label
        {
            Text      = esEdicion ? "✏  Editar Beneficiario" : "＋  Nuevo Beneficiario",
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 12, FontStyle.Bold),
            AutoSize  = true,
            Location  = new Point(14, 12)
        };
        panelCabecera.Controls.Add(lblTitulo);

        // Panel de contenido con TableLayout
        var tabla = new TableLayoutPanel
        {
            Dock        = DockStyle.Fill,
            ColumnCount = 2,
            RowCount    = 9,
            Padding     = new Padding(16, 10, 16, 6)
        };
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
        tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));

        // Nombre
        tabla.Controls.Add(CrearEtiqueta("Nombre completo *:"), 0, 0);
        txtNombre = new TextBox { Dock = DockStyle.Fill, MaxLength = 150 };
        tabla.Controls.Add(txtNombre, 1, 0);

        // Fecha nacimiento
        tabla.Controls.Add(CrearEtiqueta("Fecha de nacimiento:"), 0, 1);
        var panelFecha = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        dtpNacimiento = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 120, MaxDate = DateTime.Today };
        chkTieneFechaNacimiento = new CheckBox { Text = "Conocida", AutoSize = true, Margin = new Padding(6, 4, 0, 0) };
        chkTieneFechaNacimiento.CheckedChanged += AlCambiarCheckFecha;
        panelFecha.Controls.AddRange(new Control[] { dtpNacimiento, chkTieneFechaNacimiento });
        tabla.Controls.Add(panelFecha, 1, 1);
        dtpNacimiento.Enabled = false; // Deshabilitada hasta marcar checkbox

        // Género
        tabla.Controls.Add(CrearEtiqueta("Género:"), 0, 2);
        cboGenero = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
        cboGenero.Items.AddRange(new object[] { "Masculino", "Femenino", "No especificado" });
        cboGenero.SelectedIndex = 2;
        tabla.Controls.Add(cboGenero, 1, 2);

        // Encargado
        tabla.Controls.Add(CrearEtiqueta("Nombre encargado:"), 0, 3);
        txtEncargado = new TextBox { Dock = DockStyle.Fill, MaxLength = 150 };
        tabla.Controls.Add(txtEncargado, 1, 3);

        // Teléfono
        tabla.Controls.Add(CrearEtiqueta("Teléfono encargado:"), 0, 4);
        txtTelefono = new TextBox { Dock = DockStyle.Fill, MaxLength = 20 };
        tabla.Controls.Add(txtTelefono, 1, 4);

        // Dirección
        tabla.Controls.Add(CrearEtiqueta("Dirección:"), 0, 5);
        txtDireccion = new TextBox { Dock = DockStyle.Fill, MaxLength = 250 };
        tabla.Controls.Add(txtDireccion, 1, 5);

        // Mensaje de error/validación
        lblMensaje = new Label
        {
            Dock      = DockStyle.Fill,
            ForeColor = Color.Firebrick,
            Font      = new Font("Segoe UI", 8.5f, FontStyle.Italic),
            Padding   = new Padding(0, 4, 0, 0)
        };
        tabla.SetColumnSpan(lblMensaje, 2);
        tabla.Controls.Add(lblMensaje, 0, 6);

        // Separador
        tabla.Controls.Add(new Label { Height = 6 }, 0, 7);

        // Botones
        var panelBotones = new FlowLayoutPanel
        {
            FlowDirection   = FlowDirection.RightToLeft,
            Dock            = DockStyle.Fill,
            Padding         = new Padding(0, 4, 0, 0)
        };
        tabla.SetColumnSpan(panelBotones, 2);

        btnCancelar = new Button
        {
            Text      = "Cancelar",
            Size      = new Size(90, 32),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(200, 200, 200),
            Cursor    = Cursors.Hand
        };
        btnCancelar.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };

        btnGuardar = new Button
        {
            Text      = "💾  Guardar",
            Size      = new Size(110, 32),
            FlatStyle = FlatStyle.Flat,
            BackColor = Color.FromArgb(30, 80, 160),
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Cursor    = Cursors.Hand,
            Margin    = new Padding(0, 0, 8, 0)
        };
        btnGuardar.Click += AlHacerClickEnGuardar;

        panelBotones.Controls.AddRange(new Control[] { btnCancelar, btnGuardar });
        tabla.Controls.Add(panelBotones, 0, 8);

        Controls.Add(tabla);
        Controls.Add(panelCabecera);

        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
    }

    private static Label CrearEtiqueta(string texto)
        => new Label
        {
            Text      = texto,
            Dock      = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleRight,
            Padding   = new Padding(0, 0, 8, 0),
            Font      = new Font("Segoe UI", 9f)
        };

    private void AlCambiarCheckFecha(object? sender, EventArgs e)
        => dtpNacimiento.Enabled = chkTieneFechaNacimiento.Checked;

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

    private async void AlHacerClickEnGuardar(object? sender, EventArgs e)
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
