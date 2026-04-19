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
using CasaDeLosNinos.Aplicacion.Servicios;

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
        public bool DeseaCerrarSesion { get; private set; } = false;

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
            ConfigurarEventosBotones();
            ApplyTheme();
            this.FormClosing += FormPrincipal_FormClosing;
        }

        private void ConfigurarEventosBotones()
        {
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is IconButton btn)
                {
                    btn.MouseEnter += (s, e) => 
                    {
                        if (btn != currentBtn) // Solo si no es el botón activo
                        {
                            btn.ForeColor = _currentTheme.TextPrimary;
                            btn.IconColor = _currentTheme.TextPrimary;
                        }
                    };

                    btn.MouseLeave += (s, e) => 
                    {
                        if (btn != currentBtn) // Solo si no es el botón activo
                        {
                            btn.ForeColor = _currentTheme.AccentColor;
                            btn.IconColor = _currentTheme.AccentColor;
                        }
                    };
                }
            }
        }

        private void FormPrincipal_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Si el cierre fue provocado por el sistema o por el login inicial, no preguntamos
            if (e.CloseReason == CloseReason.ApplicationExitCall) return;

            var result = MessageBox.Show("¿Está seguro que desea salir del sistema o cerrar sesión?", "Confirmar Salida",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
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
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = color; // Accent Color
                currentBtn.ForeColor = Color.Black; // Máximo contraste
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.Black;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                
                // Borde lateral decorativo
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                // Icono de cabecera
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = _currentTheme.AccentColor;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = _currentTheme.AccentColor;
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
            if (childForm is FormBase formBase) formBase.EsRedimensionable = false;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
            
            // Aplicar tema al formulario hijo respetando encapsulamiento
            if (childForm is FormBase childBase)
                childBase.RefreshTheme(_currentTheme);
            else
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
            {
                if (currentChildForm is FormBase childBase)
                    childBase.RefreshTheme(_currentTheme);
                else
                    ThemeEngine.ApplyTheme(currentChildForm, _currentTheme);
            }
            
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
                var servicioFoto = _proveedor.GetRequiredService<IServicioFoto>();
                OpenChildForm(new FrmGestionNinos(servicioNino, servicioObservacion, servicioFoto, _usuarioActual.Id, _currentTheme));
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
                OpenChildForm(new FrmTomaAsistencia(servicioAsistencia, _usuarioActual.Id, _currentTheme));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de asistencia:\n{ex.Message}", "Error");
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, _currentTheme.AccentColor);
            try
            {
                var servicioUsuario = _proveedor.GetRequiredService<IServicioUsuario>();
                OpenChildForm(new FrmGestionUsuarios(servicioUsuario, _usuarioActual.Id, _currentTheme));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de usuarios:\n{ex.Message}", "Error");
            }
        }

        private void btnVoluntarios_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, _currentTheme.AccentColor);
            try
            {
                var servicioVoluntario = _proveedor.GetRequiredService<IServicioVoluntario>();
                var servicioRegistroHoras = _proveedor.GetRequiredService<IServicioRegistroHoras>();
                OpenChildForm(new FrmGestionVoluntarios(servicioVoluntario, servicioRegistroHoras, _proveedor, _usuarioActual.Id, _currentTheme));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de voluntarios:\n{ex.Message}", "Error");
            }
        }

        private void btnCajaChica_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, _currentTheme.AccentColor);
            try
            {
                var servicioCajaChica = _proveedor.GetRequiredService<IServicioCajaChica>();
                var servicioFoto = _proveedor.GetRequiredService<IServicioFoto>();
                OpenChildForm(new FrmGestionCajaChica(servicioCajaChica, servicioFoto, _usuarioActual.Id, _currentTheme));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de caja chica:\n{ex.Message}", "Error");
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

            // Lógica de Permisos
            if (_usuarioActual.IdRol != 1) // Si NO es Administrador
            {
                btnUsuarios.Visible = false;
            }

            ConfigurarFondo();
        }

        private void ConfigurarLogotipo()
        {
            try
            {
                string pathMin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "logomin.png");
                if (File.Exists(pathMin))
                {
                    var img = Image.FromFile(pathMin);
                    btnHome.Image = img;
                    //picLogoCentral.Image = img;
                }
            }
            catch { }
        }

        private void ConfigurarFondo()
        {
            try
            {
                string wallPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "walls", "CasaNinosWallHalfSat.png");
                if (File.Exists(wallPath))
                {
                    panelDesktop.BackgroundImage = Image.FromFile(wallPath);
                    panelDesktop.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch { }
        }

        // Botones de Control Box
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DeseaCerrarSesion = false;
            this.Close();
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.DeseaCerrarSesion = true;
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
