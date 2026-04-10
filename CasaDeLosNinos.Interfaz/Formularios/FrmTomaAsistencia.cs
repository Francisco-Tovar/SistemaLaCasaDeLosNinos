using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Interfaz.Formularios;

/// <summary>
/// Formulario de toma de asistencia masiva diaria.
/// Carga todos los niños activos, permite marcar/desmarcar presencia
/// y guarda la asistencia completa en una sola transacción.
/// Si ya existe asistencia para la fecha, permite corregirla.
/// </summary>
public class FrmTomaAsistencia : Form
{
    private readonly IServicioAsistencia _servicioAsistencia;
    private readonly int                  _idUsuarioActual;

    // Controles
    private DateTimePicker dtpFecha      = null!;
    private Button         btnCargar     = null!;
    private Button         btnGuardar    = null!;
    private Button         btnMarcarTodos = null!;
    private Button         btnDesmarcarTodos = null!;
    private DataGridView   grdAsistencia = null!;
    private Label          lblResumen    = null!;
    private Label          lblEstado     = null!;

    // Datos en memoria
    private List<NinoAsistenciaDto> _lista = new();

    public FrmTomaAsistencia(IServicioAsistencia servicioAsistencia, int idUsuarioActual)
    {
        _servicioAsistencia = servicioAsistencia;
        _idUsuarioActual    = idUsuarioActual;
        ConfigurarUI();
    }

    // ══════════════════════════════════════════════════════════════
    // CONSTRUCCIÓN DE LA INTERFAZ
    // ══════════════════════════════════════════════════════════════

    private void ConfigurarUI()
    {
        Text            = "Toma de Asistencia Diaria — La Casa de los Niños";
        Size            = new Size(620, 620);
        StartPosition   = FormStartPosition.CenterParent;
        MinimumSize     = new Size(500, 480);
        BackColor       = Color.FromArgb(245, 247, 250);
        Font            = new Font("Segoe UI", 9.5f);

        // ── Cabecera ─────────────────────────────────────────────
        var panelCabecera = new Panel
        {
            Dock      = DockStyle.Top,
            Height    = 50,
            BackColor = Color.FromArgb(22, 120, 100)
        };
        var lblTitulo = new Label
        {
            Text      = "📋  Toma de Asistencia",
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 13, FontStyle.Bold),
            AutoSize  = true,
            Location  = new Point(12, 12)
        };
        panelCabecera.Controls.Add(lblTitulo);

        // ── Barra de fecha y acciones ────────────────────────────
        var panelFecha = new Panel
        {
            Dock      = DockStyle.Top,
            Height    = 48,
            BackColor = Color.FromArgb(220, 240, 235),
            Padding   = new Padding(12, 9, 12, 5)
        };

        var lblFecha = new Label
        {
            Text      = "Fecha:",
            AutoSize  = true,
            Location  = new Point(12, 14),
            Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold)
        };

        dtpFecha = new DateTimePicker
        {
            Format   = DateTimePickerFormat.Short,
            MaxDate  = DateTime.Today,
            Value    = DateTime.Today,
            Location = new Point(60, 10),
            Width    = 115
        };

        btnCargar = new Button
        {
            Text      = "⟳  Cargar",
            Location  = new Point(188, 9),
            Size      = new Size(85, 30),
            BackColor = Color.FromArgb(22, 120, 100),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
            Cursor    = Cursors.Hand
        };
        btnCargar.Click += AlHacerClickEnCargar;

        btnMarcarTodos = new Button
        {
            Text      = "✔ Todos",
            Location  = new Point(283, 9),
            Size      = new Size(78, 30),
            BackColor = Color.FromArgb(39, 174, 96),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand
        };
        btnMarcarTodos.Click += (_, _) => CambiarTodos(true);

        btnDesmarcarTodos = new Button
        {
            Text      = "✘ Ninguno",
            Location  = new Point(369, 9),
            Size      = new Size(85, 30),
            BackColor = Color.FromArgb(192, 57, 43),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor    = Cursors.Hand
        };
        btnDesmarcarTodos.Click += (_, _) => CambiarTodos(false);

        panelFecha.Controls.AddRange(new Control[]
            { lblFecha, dtpFecha, btnCargar, btnMarcarTodos, btnDesmarcarTodos });

        // ── DataGridView ─────────────────────────────────────────
        grdAsistencia = new DataGridView
        {
            Dock                  = DockStyle.Fill,
            SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect           = false,
            AllowUserToAddRows    = false,
            AllowUserToDeleteRows = false,
            RowHeadersVisible     = false,
            BackgroundColor       = Color.White,
            BorderStyle           = BorderStyle.None,
            Font                  = new Font("Segoe UI", 9.5f)
        };
        grdAsistencia.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 120, 100);
        grdAsistencia.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grdAsistencia.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        grdAsistencia.EnableHeadersVisualStyles = false;
        grdAsistencia.RowTemplate.Height        = 28;
        grdAsistencia.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 248, 244);
        grdAsistencia.CellValueChanged  += AlCambiarCelda;
        grdAsistencia.CurrentCellDirtyStateChanged += (_, _) =>
        {
            if (grdAsistencia.IsCurrentCellDirty)
                grdAsistencia.CommitEdit(DataGridViewDataErrorContexts.Commit);
        };

        ConfigurarColumnasGrilla();

        // ── Panel inferior ───────────────────────────────────────
        var panelInferior = new Panel
        {
            Dock      = DockStyle.Bottom,
            Height    = 54,
            BackColor = Color.FromArgb(220, 240, 235),
            Padding   = new Padding(12, 8, 12, 6)
        };

        lblResumen = new Label
        {
            AutoSize  = true,
            Location  = new Point(12, 10),
            Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 100, 60)
        };

        lblEstado = new Label
        {
            AutoSize  = true,
            Location  = new Point(12, 30),
            Font      = new Font("Segoe UI", 8.5f, FontStyle.Italic),
            ForeColor = Color.FromArgb(60, 100, 80)
        };

        btnGuardar = new Button
        {
            Text      = "💾  Guardar Asistencia",
            Size      = new Size(160, 36),
            BackColor = Color.FromArgb(30, 80, 160),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            Cursor    = Cursors.Hand,
            Enabled   = false
        };
        // Anclar botón a la derecha del panel inferior
        btnGuardar.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        panelInferior.Controls.Add(btnGuardar);
        panelInferior.SizeChanged += (_, _) =>
            btnGuardar.Location = new Point(panelInferior.ClientSize.Width - btnGuardar.Width - 12, 8);

        btnGuardar.Click += AlHacerClickEnGuardar;

        panelInferior.Controls.AddRange(new Control[] { lblResumen, lblEstado });

        Controls.Add(grdAsistencia);
        Controls.Add(panelFecha);
        Controls.Add(panelCabecera);
        Controls.Add(panelInferior);

        // Cargar automáticamente con la fecha de hoy al abrir
        Load += async (_, _) => await CargarAsistenciaAsync();
    }

    private void ConfigurarColumnasGrilla()
    {
        var colPresente = new DataGridViewCheckBoxColumn
        {
            Name             = "colPresente",
            HeaderText       = "✔ Presente",
            DataPropertyName = "Presente",
            Width            = 80,
            FillWeight       = 15
        };

        var colNombre = new DataGridViewTextBoxColumn
        {
            Name             = "colNombre",
            HeaderText       = "Nombre del Niño / Niña",
            DataPropertyName = "NombreCompleto",
            ReadOnly         = true,
            FillWeight       = 85
        };

        grdAsistencia.Columns.AddRange(colPresente, colNombre);
    }

    // ══════════════════════════════════════════════════════════════
    // LÓGICA DE DATOS
    // ══════════════════════════════════════════════════════════════

    private async Task CargarAsistenciaAsync()
    {
        btnGuardar.Enabled = false;
        lblEstado.Text     = "Cargando...";
        lblResumen.Text    = string.Empty;

        try
        {
            var fecha = dtpFecha.Value.Date;
            _lista = (await _servicioAsistencia.ObtenerNinosParaAsistenciaAsync(fecha)).ToList();

            grdAsistencia.DataSource = null;
            grdAsistencia.DataSource = _lista;

            btnGuardar.Enabled = _lista.Count > 0;
            ActualizarResumen();
            lblEstado.Text = _lista.Count > 0
                ? $"Cargados {_lista.Count} niños activos para el {fecha:dd/MM/yyyy}"
                : "No hay niños activos registrados en el sistema.";
        }
        catch (Exception ex)
        {
            lblEstado.Text = $"Error al cargar: {ex.Message}";
            MessageBox.Show($"Error al cargar la asistencia:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ActualizarResumen()
    {
        int total    = _lista.Count;
        int presentes = _lista.Count(d => d.Presente);
        lblResumen.Text = $"Presentes: {presentes} / {total}   |   Ausentes: {total - presentes}";
    }

    private void CambiarTodos(bool estado)
    {
        foreach (var dto in _lista)
            dto.Presente = estado;

        // Refrescar grilla
        grdAsistencia.DataSource = null;
        grdAsistencia.DataSource = _lista;
        ActualizarResumen();
    }

    // ══════════════════════════════════════════════════════════════
    // MANEJADORES DE EVENTOS
    // ══════════════════════════════════════════════════════════════

    private async void AlHacerClickEnCargar(object? sender, EventArgs e)
        => await CargarAsistenciaAsync();

    private void AlCambiarCelda(object? sender, DataGridViewCellEventArgs e)
    {
        // Sincronizar el cambio de checkbox con el DTO subyacente
        if (e.ColumnIndex == grdAsistencia.Columns["colPresente"]?.Index && e.RowIndex >= 0)
        {
            var celda = grdAsistencia.Rows[e.RowIndex].Cells["colPresente"];
            if (e.RowIndex < _lista.Count)
                _lista[e.RowIndex].Presente = (bool)(celda.Value ?? false);

            ActualizarResumen();
        }
    }

    private async void AlHacerClickEnGuardar(object? sender, EventArgs e)
    {
        btnGuardar.Enabled = false;
        lblEstado.Text     = "Guardando asistencia...";

        try
        {
            var fecha = dtpFecha.Value.Date;
            var (exito, mensaje) = await _servicioAsistencia.GuardarAsistenciaAsync(
                fecha, _lista, _idUsuarioActual);

            if (exito)
            {
                lblEstado.ForeColor = Color.FromArgb(22, 120, 60);
                lblEstado.Text      = $"✔  {mensaje}";
                MessageBox.Show(mensaje, "Asistencia Guardada",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblEstado.ForeColor = Color.Firebrick;
                lblEstado.Text      = $"✘  {mensaje}";
            }
        }
        catch (Exception ex)
        {
            lblEstado.ForeColor = Color.Firebrick;
            lblEstado.Text      = $"Error: {ex.Message}";
            MessageBox.Show($"Error al guardar la asistencia:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnGuardar.Enabled = _lista.Count > 0;
        }
    }
}
