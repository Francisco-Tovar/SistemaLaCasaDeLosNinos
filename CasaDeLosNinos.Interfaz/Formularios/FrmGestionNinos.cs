using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios;

/// <summary>
/// Formulario de gestión del catálogo de niños beneficiarios.
/// Permite listar, buscar, crear, editar y activar/desactivar (borrado lógico).
/// Nunca elimina registros físicamente.
/// </summary>
public class FrmGestionNinos : Form
{
    private readonly IServicioNino         _servicioNino;
    private readonly IServicioObservacion  _servicioObservacion;
    private readonly int                   _idUsuarioSesion;

    // Controles principales
    private DataGridView    grdNinos      = null!;
    private TextBox         txtBuscar     = null!;
    private Button          btnNuevo      = null!;
    private Button          btnEditar     = null!;
    private Button          btnEstado     = null!;
    private Button          btnBitacora   = null!;
    private Button          btnActualizar = null!;
    private Label           lblConteo     = null!;
    private CheckBox        chkMostrarInactivos = null!;

    // Cache en memoria para filtrado local
    private List<Nino> _todosLosNinos = new();

    public FrmGestionNinos(
        IServicioNino        servicioNino,
        IServicioObservacion servicioObservacion,
        int                  idUsuarioSesion)
    {
        _servicioNino        = servicioNino;
        _servicioObservacion = servicioObservacion;
        _idUsuarioSesion     = idUsuarioSesion;
        ConfigurarUI();
    }

    // ══════════════════════════════════════════════════════════════
    // CONSTRUCCIÓN DE LA INTERFAZ
    // ══════════════════════════════════════════════════════════════

    private void ConfigurarUI()
    {
        Text            = "Gestión de Niños Beneficiarios — La Casa de los Niños";
        Size            = new Size(900, 580);
        StartPosition   = FormStartPosition.CenterParent;
        MinimumSize     = new Size(750, 480);
        BackColor       = Color.FromArgb(245, 247, 250);
        Font            = new Font("Segoe UI", 9.5f);

        // ── Barra superior ───────────────────────────────────────
        var panelSuperior = new Panel
        {
            Dock        = DockStyle.Top,
            Height      = 54,
            BackColor   = Color.FromArgb(30, 80, 160),
            Padding     = new Padding(10, 8, 10, 8)
        };

        var lblTitulo = new Label
        {
            Text      = "👶  Niños Beneficiarios",
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 13, FontStyle.Bold),
            AutoSize  = true,
            Location  = new Point(10, 12)
        };
        panelSuperior.Controls.Add(lblTitulo);

        // ── Barra de herramientas ────────────────────────────────
        var panelHerramientas = new Panel
        {
            Dock      = DockStyle.Top,
            Height    = 46,
            Padding   = new Padding(10, 8, 10, 4),
            BackColor = Color.FromArgb(235, 240, 248)
        };

        txtBuscar = new TextBox
        {
            Width        = 220,
            Location     = new Point(10, 12),
            Font         = new Font("Segoe UI", 9.5f),
            PlaceholderText = "🔍  Buscar por nombre..."
        };
        txtBuscar.TextChanged += AlCambiarBusqueda;

        chkMostrarInactivos = new CheckBox
        {
            Text     = "Mostrar inactivos",
            Location = new Point(240, 14),
            AutoSize = true
        };
        chkMostrarInactivos.CheckedChanged += (_, _) => AplicarFiltro();

        btnNuevo      = CrearBoton("＋ Nuevo",       Color.FromArgb(39, 174, 96),   440);
        btnEditar     = CrearBoton("✏  Editar",      Color.FromArgb(41, 128, 185),  535);
        btnBitacora   = CrearBoton("📝  Bitácora",   Color.FromArgb(142, 68, 173),  630);
        btnEstado     = CrearBoton("⊘  Desactivar",  Color.FromArgb(192, 57, 43),   730);
        btnActualizar = CrearBoton("↺  Actualizar",  Color.FromArgb(100, 100, 100), 830);

        btnNuevo.Click      += AlHacerClickEnNuevo;
        btnEditar.Click     += AlHacerClickEnEditar;
        btnBitacora.Click   += AlHacerClickEnBitacora;
        btnEstado.Click     += AlHacerClickEnCambiarEstado;
        btnActualizar.Click += async (_, _) => await CargarNinosAsync();

        panelHerramientas.Controls.AddRange(new Control[]
            { txtBuscar, chkMostrarInactivos, btnNuevo, btnEditar, btnBitacora, btnEstado, btnActualizar });


        // ── DataGridView ─────────────────────────────────────────
        grdNinos = new DataGridView
        {
            Dock                  = DockStyle.Fill,
            ReadOnly              = true,
            SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect           = false,
            AllowUserToAddRows    = false,
            AllowUserToDeleteRows = false,
            RowHeadersVisible     = false,
            BackgroundColor       = Color.White,
            BorderStyle           = BorderStyle.None,
            AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
            Font                  = new Font("Segoe UI", 9.5f)
        };
        grdNinos.ColumnHeadersDefaultCellStyle.BackColor   = Color.FromArgb(30, 80, 160);
        grdNinos.ColumnHeadersDefaultCellStyle.ForeColor   = Color.White;
        grdNinos.ColumnHeadersDefaultCellStyle.Font        = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        grdNinos.EnableHeadersVisualStyles = false;
        grdNinos.RowTemplate.Height        = 28;
        grdNinos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 255);
        grdNinos.CellDoubleClick += AlDobleClickEnFila;

        ConfigurarColumnasGrilla();

        // ── Barra inferior ───────────────────────────────────────
        var panelInferior = new Panel
        {
            Dock      = DockStyle.Bottom,
            Height    = 30,
            BackColor = Color.FromArgb(220, 230, 245),
            Padding   = new Padding(10, 5, 10, 0)
        };
        lblConteo = new Label
        {
            AutoSize  = true,
            ForeColor = Color.FromArgb(60, 60, 100),
            Font      = new Font("Segoe UI", 8.5f, FontStyle.Italic)
        };
        panelInferior.Controls.Add(lblConteo);

        Controls.Add(grdNinos);
        Controls.Add(panelHerramientas);
        Controls.Add(panelSuperior);
        Controls.Add(panelInferior);

        Load += async (_, _) => await CargarNinosAsync();
    }

    private void ConfigurarColumnasGrilla()
    {
        grdNinos.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colId",        HeaderText = "ID",          DataPropertyName = "Id",               Width = 50, FillWeight = 5 });
        grdNinos.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colNombre",    HeaderText = "Nombre Completo", DataPropertyName = "NombreCompleto", FillWeight = 35 });
        grdNinos.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colNacimiento",HeaderText = "Nacimiento",  DataPropertyName = "FechaNacimiento",  FillWeight = 15 });
        grdNinos.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colEncargado", HeaderText = "Encargado",   DataPropertyName = "NombreEncargado",  FillWeight = 25 });
        grdNinos.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "colTelefono",  HeaderText = "Teléfono",    DataPropertyName = "TelefonoEncargado",FillWeight = 15 });
        grdNinos.Columns.Add(new DataGridViewCheckBoxColumn
            { Name = "colActivo",    HeaderText = "Activo",      DataPropertyName = "Activo",           FillWeight = 8, ReadOnly = true });
    }

    private static Button CrearBoton(string texto, Color color, int x)
        => new Button
        {
            Text      = texto,
            Location  = new Point(x, 9),
            Size      = new Size(88, 28),
            BackColor = color,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
            Cursor    = Cursors.Hand
        };

    // ══════════════════════════════════════════════════════════════
    // LÓGICA DE DATOS
    // ══════════════════════════════════════════════════════════════

    private async Task CargarNinosAsync()
    {
        try
        {
            _todosLosNinos = (await _servicioNino.ObtenerTodosAsync()).ToList();
            AplicarFiltro();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar los niños:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AplicarFiltro()
    {
        var filtro = txtBuscar.Text.Trim().ToLowerInvariant();
        var mostrarInactivos = chkMostrarInactivos.Checked;

        var lista = _todosLosNinos
            .Where(n => (mostrarInactivos || n.Activo)
                     && (string.IsNullOrEmpty(filtro)
                         || n.NombreCompleto.ToLowerInvariant().Contains(filtro)))
            .ToList();

        grdNinos.DataSource = null;
        grdNinos.DataSource = lista;

        lblConteo.Text = $"Mostrando {lista.Count} de {_todosLosNinos.Count} registros";
        ActualizarBotonesEstado();
    }

    private void ActualizarBotonesEstado()
    {
        var nino = ObtenerNinoSeleccionado();
        btnEstado.Text      = (nino?.Activo ?? true) ? "⊘  Desactivar" : "✔  Activar";
        btnEstado.BackColor = (nino?.Activo ?? true)
            ? Color.FromArgb(192, 57, 43)
            : Color.FromArgb(39, 174, 96);
    }

    private Nino? ObtenerNinoSeleccionado()
    {
        if (grdNinos.CurrentRow?.DataBoundItem is Nino nino)
            return nino;
        return null;
    }

    // ══════════════════════════════════════════════════════════════
    // MANEJADORES DE EVENTOS
    // ══════════════════════════════════════════════════════════════

    private void AlCambiarBusqueda(object? sender, EventArgs e) => AplicarFiltro();

    private void AlDobleClickEnFila(object? sender, DataGridViewCellEventArgs e)
        => AlHacerClickEnEditar(sender, e);

    private async void AlHacerClickEnNuevo(object? sender, EventArgs e)
    {
        try
        {
            using var frm = new FrmEdicionNino(null, _servicioNino);
            if (frm.ShowDialog(this) == DialogResult.OK)
                await CargarNinosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al abrir el formulario:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void AlHacerClickEnEditar(object? sender, EventArgs e)
    {
        var nino = ObtenerNinoSeleccionado();
        if (nino == null)
        {
            MessageBox.Show("Seleccione un niño de la lista para editar.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            using var frm = new FrmEdicionNino(nino, _servicioNino);
            if (frm.ShowDialog(this) == DialogResult.OK)
                await CargarNinosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al abrir el formulario:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void AlHacerClickEnCambiarEstado(object? sender, EventArgs e)
    {
        var nino = ObtenerNinoSeleccionado();
        if (nino == null)
        {
            MessageBox.Show("Seleccione un niño de la lista.", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        string accion = nino.Activo ? "desactivar" : "activar";
        var confirmar = MessageBox.Show(
            $"¿Desea {accion} a {nino.NombreCompleto}?",
            "Confirmar cambio de estado",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmar != DialogResult.Yes) return;

        try
        {
            await _servicioNino.CambiarEstadoAsync(nino.Id, !nino.Activo);
            await CargarNinosAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cambiar el estado:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AlHacerClickEnBitacora(object? sender, EventArgs e)
    {
        var nino = ObtenerNinoSeleccionado();
        if (nino == null)
        {
            MessageBox.Show("Seleccione un niño para ver su bitácora de observaciones.",
                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            using var frm = new FrmObservaciones(nino, _idUsuarioSesion, _servicioObservacion);
            frm.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al abrir la bitácora:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
