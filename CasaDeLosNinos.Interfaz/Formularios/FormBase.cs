using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CasaDeLosNinos.Interfaz.Formularios
{
    public class FormBase : Form
    {
        // Importaciones para arrastrar el formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        protected extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        protected extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public bool EsRedimensionable { get; set; } = true;
        public bool TieneBordeAcento { get; set; } = false;
        protected Estilos.ThemeColors _theme;
        private Label? _lblGrip;
        
        public FormBase()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(1);
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };
            _theme = Estilos.ThemeEngine.LoadThemePreference();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigurarGrip(); // Ahora se configura cuando todas las propiedades están seteadas
        }

        private void ConfigurarGrip()
        {
            if (EsRedimensionable)
            {
                if (_lblGrip != null) return; // Ya existe

                _lblGrip = new Label
                {
                    Width = 15,
                    Height = 15,
                    Cursor = Cursors.SizeNWSE,
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.BottomRight,
                    Text = "◢", // Un carácter de esquina sutil
                    ForeColor = Color.FromArgb(100, Color.Gray),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                    Enabled = true // Ahora es una manija activa
                };
                
                // Mapear el clic del Grip al redimensionamiento nativo de Windows
                _lblGrip.MouseDown += (s, e) => {
                    if (e.Button == MouseButtons.Left) {
                        ReleaseCapture();
                        SendMessage(this.Handle, 0x112, 0xF008, 0); // 0xF008 = SC_SIZE + HTBOTTOMRIGHT
                    }
                };
                
                // Mantenemos el Grip siempre al frente
                this.Controls.Add(_lblGrip);
                _lblGrip.BringToFront();
                ActualizarPosicionGrip();
            }
        }

        private void ActualizarPosicionGrip()
        {
            if (_lblGrip != null)
            {
                _lblGrip.Location = new Point(this.Width - _lblGrip.Width, this.Height - _lblGrip.Height);
                _lblGrip.BringToFront();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ActualizarPosicionGrip();
        }

        public virtual void RefreshTheme(Estilos.ThemeColors theme)
        {
            _theme = theme;
            if (_lblGrip != null) _lblGrip.ForeColor = Color.FromArgb(150, _theme.AccentColor);
            Estilos.ThemeEngine.ApplyTheme(this, _theme);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (TieneBordeAcento && _theme != null)
            {
                using (var pen = new Pen(_theme.AccentColor, 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }

        protected void DragForm()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_NCHITTEST = 0x0084;
            const int resizeAreaSize = 16; // Aumentado para facilitar agarre bordes (era 10)

            // Quitar barra de título pero mantener funciones de redimensionado/sombra
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }

            // Redimensionamiento personalizado
            if (m.Msg == WM_NCHITTEST)
            {
                if (!EsRedimensionable)
                {
                    base.WndProc(ref m);
                    return;
                }

                base.WndProc(ref m);
                if ((int)m.Result == 1) // HTCLIENT
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);

                    if (clientPoint.Y <= resizeAreaSize)
                    {
                        if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)13; // HTTOPLEFT
                        else if (clientPoint.X >= (this.Size.Width - resizeAreaSize)) m.Result = (IntPtr)14; // HTTOPRIGHT
                        else m.Result = (IntPtr)12; // HTTOP
                    }
                    else if (clientPoint.Y >= (this.Size.Height - resizeAreaSize))
                    {
                        if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)16; // HTBOTTOMLEFT
                        else if (clientPoint.X >= (this.Size.Width - resizeAreaSize)) m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                        else m.Result = (IntPtr)15; // HTBOTTOM
                    }
                    else if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)10; // HTLEFT
                    else if (clientPoint.X >= (this.Size.Width - resizeAreaSize)) m.Result = (IntPtr)11; // HTRIGHT
                }
                return;
            }

            // Evitar que Windows dibuje la barra de título/borde gris al perder el foco
            const int WM_NCACTIVATE = 0x0086;
            if (m.Msg == WM_NCACTIVATE)
            {
                m.Result = (IntPtr)1; // Indicar que hemos manejado la activación
                return;
            }

            base.WndProc(ref m);
        }
    }
}
