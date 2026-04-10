using System;
using System.Drawing;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FormPrincipal : Form
    {
        private readonly IConfiguration   _configuracion;
        private readonly IServiceProvider _proveedor;
        private readonly Usuario          _usuarioActual;

        public FormPrincipal(
            IConfiguration   configuracion,
            IServiceProvider proveedor,
            Usuario          usuarioActual)
        {
            InitializeComponent();
            menuPrincipal.Renderer = new ToolStripProfessionalRenderer(new ColorMenuProfesional());
            _configuracion = configuracion;
            _proveedor     = proveedor;
            _usuarioActual = usuarioActual;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            var nombreOrg   = _configuracion["Configuracion:NombreOrganizacion"] ?? "La Casa de los Niños";
            var version     = _configuracion["Configuracion:VersionSistema"] ?? "0.0.0";

            this.Text          = $"{nombreOrg} — v{version}";
            this.lblBienvenida.Text = $"Bienvenido, {_usuarioActual.NombreCompleto}";
            this.lblOrg.Text = nombreOrg;

            CentrarControlesBienvenida();
        }

        private void FormPrincipal_Resize(object sender, EventArgs e)
        {
            CentrarControlesBienvenida();
        }

        private void CentrarControlesBienvenida()
        {
            if (panelCentral == null || lblBienvenida == null || lblOrg == null || lblHint == null) return;
            
            int anchoPanel  = panelCentral.ClientSize.Width;
            int altoPanel   = panelCentral.ClientSize.Height;
            int altoTotal   = lblBienvenida.PreferredHeight + 10 + lblOrg.PreferredHeight + 20 + lblHint.PreferredHeight;
            int inicioY     = (altoPanel - altoTotal) / 2;

            lblBienvenida.Location = new Point((anchoPanel - lblBienvenida.PreferredWidth) / 2, inicioY);
            lblOrg.Location = new Point((anchoPanel - lblOrg.PreferredWidth) / 2, inicioY + lblBienvenida.PreferredHeight + 10);
            lblHint.Location = new Point((anchoPanel - lblHint.PreferredWidth) / 2, inicioY + lblBienvenida.PreferredHeight + 10 + lblOrg.PreferredHeight + 20);
        }

        private void AlAbrirGestionNinos(object sender, EventArgs e)
        {
            try
            {
                var servicioNino        = _proveedor.GetRequiredService<IServicioNino>();
                var servicioObservacion = _proveedor.GetRequiredService<IServicioObservacion>();
                using var frm = new FrmGestionNinos(servicioNino, servicioObservacion, _usuarioActual.Id);
                frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de niños:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlAbrirTomaAsistencia(object sender, EventArgs e)
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
}
