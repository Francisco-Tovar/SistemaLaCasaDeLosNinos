using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using FontAwesome.Sharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CasaDeLosNinos.Interfaz.Estilos;
using System.IO;

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
        private ThemeColors _currentTheme;
        private ContextMenuStrip _themeMenu;

        public FormPrincipal(
            IConfiguration configuracion,
            IServiceProvider proveedor,
            Usuario usuarioActual)
        {
            InitializeComponent();
            _configuracion = configuracion;
            _proveedor = proveedor;
            _usuarioActual = usuarioActual;
            ConfigurarLogotipo();

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            // Formulario sin bordes
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            // Cargar y Aplicar Tema Inicial
            _currentTheme = ThemeEngine.LoadThemePreference();
            ConfigurarMenuTemas();
            ApplyTheme();
        }

        private void ConfigurarMenuTemas()
        {
            _themeMenu = new ContextMenuStrip();
            ActualizarMenuTemas();
        }

        private void ActualizarMenuTemas()
        {
            _themeMenu.Items.Clear();
            foreach (var themeName in ThemeConfiguration.GetThemeNames())
            {
                var item = new ToolStripMenuItem(themeName);
                item.Click += (s, e) => SeleccionarTema(themeName);
                _themeMenu.Items.Add(item);
            }
        }

        private void SeleccionarTema(string themeName)
        {
            _currentTheme = ThemeConfiguration.GetTheme(themeName);
            ThemeEngine.SaveThemePreference(themeName);
            ApplyTheme();
        }


        // Métodos
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                // Botón
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = _currentTheme.SurfaceColor;
                currentBtn.ForeColor = _currentTheme.AccentColor;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = _currentTheme.AccentColor;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // Borde izquierdo del botón
                leftBorderBtn.BackColor = _currentTheme.AccentColor;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Icono del formulario hijo actual
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = _currentTheme.AccentColor;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = _currentTheme.TextPrimary;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = _currentTheme.TextPrimary;
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
            
            // Aplicar tema al formulario hijo
            ThemeEngine.ApplyTheme(childForm, _currentTheme);
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.House;
            iconCurrentChildForm.IconColor = _currentTheme.AccentColor;
            lblTitleChildForm.Text = "Inicio";
        }

        private void ApplyTheme()
        {
            this.SuspendLayout();
            ThemeEngine.ApplyTheme(this, _currentTheme);
            if (currentChildForm != null)
                ThemeEngine.ApplyTheme(currentChildForm, _currentTheme);
            
            // Re-activar botón destacado si existe uno para mantener el resaltado tras el cambio de tema
            if (currentBtn != null)
            {
                ActivateButton(currentBtn, _currentTheme.AccentColor);
            }

            // Actualizar icono de toggle a icono de paleta ya que es selección
            btnTheme.IconChar = IconChar.Palette;
            
            this.ResumeLayout();
        }

        // Métodos de Persistencia ahora en ThemeEngine

        // Eventos
        private void btnNinos_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, _currentTheme.AccentColor);
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
            ActivateButton(sender, _currentTheme.AccentColor);
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

            // Abrir Gestión de Niños al iniciar
            btnNinos_Click(btnNinos, EventArgs.Empty);
        }

        private void ConfigurarLogotipo()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "logomin.png");
                if (File.Exists(path))
                {
                    btnHome.Image = Image.FromFile(path);
                }
            }
            catch { }
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

        private void btnTheme_Click(object sender, EventArgs e)
        {
            _themeMenu.Show(btnTheme, new Point(0, btnTheme.Height));
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
