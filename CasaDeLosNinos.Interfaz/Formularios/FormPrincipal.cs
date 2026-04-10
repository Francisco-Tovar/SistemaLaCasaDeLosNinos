using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using FontAwesome.Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public partial class FormPrincipal : FormBase
    {
        // Campos
        private readonly IConfiguration _configuracion;
        private readonly IServiceProvider _proveedor;
        private readonly Usuario _usuarioActual;
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        public FormPrincipal(
            IConfiguration configuracion,
            IServiceProvider proveedor,
            Usuario usuarioActual)
        {
            InitializeComponent();
            _configuracion = configuracion;
            _proveedor = proveedor;
            _usuarioActual = usuarioActual;

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            // Formulario sin bordes
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        // Estructuras de Colores
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        // Métodos
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                // Botón
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // Borde izquierdo del botón
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Icono del formulario hijo actual
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.House;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "Inicio";
        }

        // Eventos
        private void btnNinos_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            try
            {
                var servicioNino = _proveedor.GetRequiredService<IServicioNino>();
                var servicioObservacion = _proveedor.GetRequiredService<IServicioObservacion>();
                OpenChildForm(new FrmGestionNinos(servicioNino, servicioObservacion, _usuarioActual.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de niños:\n{ex.Message}", "Error");
            }
        }

        private void btnAsistencia_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            try
            {
                var servicioAsistencia = _proveedor.GetRequiredService<IServicioAsistencia>();
                OpenChildForm(new FrmTomaAsistencia(servicioAsistencia, _usuarioActual.Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de asistencia:\n{ex.Message}", "Error");
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        // Arrastrar Formulario
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            DragForm();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            var nombreOrg = _configuracion["Configuracion:NombreOrganizacion"] ?? "La Casa de los Niños";
            this.lblBienvenida.Text = $"Bienvenido, {_usuarioActual.NombreCompleto}";
            this.lblOrg.Text = nombreOrg;
        }

        // Botones de Control Box
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
