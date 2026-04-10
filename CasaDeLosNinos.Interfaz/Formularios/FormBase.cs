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

        public FormBase()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(2); // Un pequeño borde para mostrar el color de acento si se desea
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
            base.WndProc(ref m);
        }
    }
}
