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
        
        public FormBase()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(1); // Espacio para el borde de 1px
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (TieneBordeAcento)
            {
                var theme = Estilos.ThemeEngine.LoadThemePreference();
                using (var pen = new Pen(theme.AccentColor, 1))
                {
                    // Dibujar el borde justo en el límite interior
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
            const int resizeAreaSize = 10;

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
