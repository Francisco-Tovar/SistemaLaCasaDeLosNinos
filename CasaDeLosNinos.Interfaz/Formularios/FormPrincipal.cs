using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDeLosNinos.Interfaz.Formularios;

/// <summary>
/// Formulario principal de la aplicación.
/// Contiene el menú de navegación principal y delega la apertura
/// de módulos al contenedor de inyección de dependencias.
/// </summary>
public class FormPrincipal : Form
{
    private readonly IConfiguration  _configuracion;
    private readonly IServiceProvider _proveedor;
    private readonly Usuario          _usuarioActual;

    public FormPrincipal(
        IConfiguration  configuracion,
        IServiceProvider proveedor,
        Usuario          usuarioActual)
    {
        _configuracion = configuracion;
        _proveedor     = proveedor;
        _usuarioActual = usuarioActual;
        InicializarComponentes();
    }

    private void InicializarComponentes()
    {
        var nombreOrg   = _configuracion["Configuracion:NombreOrganizacion"] ?? "La Casa de los Niños";
        var version     = _configuracion["Configuracion:VersionSistema"] ?? "0.0.0";

        Text          = $"{nombreOrg} — v{version}";
        Size          = new Size(1000, 660);
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize   = new Size(800, 500);
        BackColor     = Color.FromArgb(245, 247, 250);
        Font          = new Font("Segoe UI", 9.5f);

        // ── MenuStrip ────────────────────────────────────────────
        var menuPrincipal = new MenuStrip
        {
            BackColor = Color.FromArgb(30, 80, 160),
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 9.5f),
            Renderer  = new ToolStripProfessionalRenderer(new ColorMenuProfesional())
        };

        // Menú Beneficiarios
        var mnuBeneficiarios = new ToolStripMenuItem("👶  Beneficiarios")
        {
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 9.5f)
        };
        var mnuGestionNinos = new ToolStripMenuItem("Gestión de Niños...");
        mnuGestionNinos.Click += AlAbrirGestionNinos;
        mnuBeneficiarios.DropDownItems.Add(mnuGestionNinos);

        // Menú Actividades
        var mnuActividades = new ToolStripMenuItem("📋  Actividades")
        {
            ForeColor = Color.White,
            Font      = new Font("Segoe UI", 9.5f)
        };
        var mnuAsistencia = new ToolStripMenuItem("Toma de Asistencia...");
        mnuAsistencia.Click += AlAbrirTomaAsistencia;
        mnuActividades.DropDownItems.Add(mnuAsistencia);

        menuPrincipal.Items.AddRange(new ToolStripItem[] { mnuBeneficiarios, mnuActividades });
        MainMenuStrip = menuPrincipal;
        Controls.Add(menuPrincipal);

        // ── Panel de bienvenida ──────────────────────────────────
        var panelCentral = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 30, 0, 0) };

        var lblBienvenida = new Label
        {
            Text      = $"Bienvenido, {_usuarioActual.NombreCompleto}",
            Font      = new Font("Segoe UI", 17, FontStyle.Bold),
            ForeColor = Color.FromArgb(30, 80, 160),
            AutoSize  = true,
            TextAlign = ContentAlignment.MiddleCenter
        };

        var lblOrg = new Label
        {
            Text      = nombreOrg,
            Font      = new Font("Segoe UI", 11),
            ForeColor = Color.FromArgb(100, 100, 120),
            AutoSize  = true,
            TextAlign = ContentAlignment.MiddleCenter
        };

        var lblHint = new Label
        {
            Text      = "Use el menú superior para navegar entre los módulos del sistema.",
            Font      = new Font("Segoe UI", 9, FontStyle.Italic),
            ForeColor = Color.FromArgb(140, 140, 160),
            AutoSize  = true,
            TextAlign = ContentAlignment.MiddleCenter
        };

        panelCentral.Controls.AddRange(new Control[] { lblBienvenida, lblOrg, lblHint });
        Controls.Add(panelCentral);

        // Centrar los controles al cargar y al redimensionar
        Load   += (_, _) => CentrarControlesBienvenida(panelCentral, lblBienvenida, lblOrg, lblHint);
        Resize += (_, _) => CentrarControlesBienvenida(panelCentral, lblBienvenida, lblOrg, lblHint);
    }

    private static void CentrarControlesBienvenida(
        Panel panel, Label lbl1, Label lbl2, Label lbl3)
    {
        int anchoPanel  = panel.ClientSize.Width;
        int altoPanel   = panel.ClientSize.Height;
        int altoTotal   = lbl1.PreferredHeight + 10 + lbl2.PreferredHeight + 20 + lbl3.PreferredHeight;
        int inicioY     = (altoPanel - altoTotal) / 2;

        lbl1.Location = new Point((anchoPanel - lbl1.PreferredWidth) / 2, inicioY);
        lbl2.Location = new Point((anchoPanel - lbl2.PreferredWidth) / 2, inicioY + lbl1.PreferredHeight + 10);
        lbl3.Location = new Point((anchoPanel - lbl3.PreferredWidth) / 2, inicioY + lbl1.PreferredHeight + 10 + lbl2.PreferredHeight + 20);
    }

    // ══════════════════════════════════════════════════════════════
    // MANEJADORES DE MENÚ
    // ══════════════════════════════════════════════════════════════

    private void AlAbrirGestionNinos(object? sender, EventArgs e)
    {
        try
        {
            var servicioNino = _proveedor.GetRequiredService<IServicioNino>();
            using var frm = new FrmGestionNinos(servicioNino);
            frm.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al abrir el módulo de niños:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AlAbrirTomaAsistencia(object? sender, EventArgs e)
    {
        try
        {
            var servicioAsistencia = _proveedor.GetRequiredService<IServicioAsistencia>();
            using var frm = new FrmTomaAsistencia(servicioAsistencia, _usuarioActual.Id);
            frm.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al abrir el módulo de asistencia:\n{ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

/// <summary>
/// Personaliza los colores del ToolStrip para que coincidan con la paleta azul del sistema.
/// </summary>
internal sealed class ColorMenuProfesional : ProfessionalColorTable
{
    public override Color MenuItemSelected          => Color.FromArgb(50, 110, 200);
    public override Color MenuItemBorder            => Color.FromArgb(50, 110, 200);
    public override Color MenuBorder                => Color.FromArgb(30, 80, 160);
    public override Color ToolStripDropDownBackground => Color.FromArgb(40, 90, 175);
    public override Color ImageMarginGradientBegin  => Color.FromArgb(30, 80, 160);
    public override Color ImageMarginGradientMiddle => Color.FromArgb(30, 80, 160);
    public override Color ImageMarginGradientEnd    => Color.FromArgb(30, 80, 160);
    public override Color MenuStripGradientBegin    => Color.FromArgb(30, 80, 160);
    public override Color MenuStripGradientEnd      => Color.FromArgb(30, 80, 160);
}
