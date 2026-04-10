using System.Windows.Forms;
using System.Drawing;
using FontAwesome.Sharp;
using System.IO;
using System;

namespace CasaDeLosNinos.Interfaz.Estilos;

/// <summary>
/// Motor recursivo optimizado con detección de patrones de nombres y corrección de persistencia visual.
/// </summary>
public static class ThemeEngine
{
    public static void ApplyTheme(Control container, ThemeColors theme)
    {
        if (container == null) return;

        StyleControl(container, theme);

        foreach (Control child in container.Controls)
        {
            ApplyTheme(child, theme);
        }
    }

    private static void StyleControl(Control control, ThemeColors theme)
    {
        // 1. Paneles Estructurales (Detección por nombre)
        if (control is Panel p)
        {
            string name = p.Name.ToLower();
            if (name.Contains("menu") || name.Contains("navegacion") || name.Contains("side") || name.Contains("logo"))
                p.BackColor = theme.NavBackground;
            else if (name.Contains("titlebar") || name.Contains("cabecera") || name.Contains("superior") || name.Contains("header"))
            {
                p.BackColor = theme.HeaderBackground;
                p.Tag = theme;
                p.Paint -= PanelTitleBar_Paint;
                p.Paint += PanelTitleBar_Paint;
            }
            else if (name.Contains("desktop") || name.Contains("contenido") || name.Contains("main"))
                p.BackColor = theme.ContentBackground;
            else if (name.Contains("herramientas") || name.Contains("inferior") || name.Contains("botones") || name.Contains("surface") || name.Contains("fecha") || name.Contains("filtro"))
                p.BackColor = theme.SurfaceColor;
            else if (name.Contains("shadow"))
                p.BackColor = GetShadowColor(theme);
            else if (p is TableLayoutPanel or FlowLayoutPanel)
                p.BackColor = Color.Transparent; // Dejar que el contenedor padre defina
            else
                p.BackColor = theme.ContentBackground;
        }

        // 2. Textos y Etiquetas
        if (control is Label lbl)
        {
            string name = lbl.Name.ToLower();
            string parentName = lbl.Parent?.Name.ToLower() ?? "";

            // REGLA ESTRICTA: Detección de Títulos y Headers
            bool isMainTitle = name.Contains("titulo") || name.Contains("title") || name.Contains("bienvenida");
            bool isHeaderArea = parentName.Contains("titlebar") || parentName.Contains("header") || 
                                parentName.Contains("superior") || parentName.Contains("cabecera") ||
                                parentName.Contains("herramientas") || parentName.Contains("botones");
            
            bool isTechnicalHeader = name.Contains("org") || name.Contains("historial") || name.Contains("nueva");

            if (isMainTitle || isHeaderArea || isTechnicalHeader)
            {
                lbl.ForeColor = theme.TextPrimary;
                // Determinamos el tamaño estableciendo valores fijos para evitar que crezca en cada cambio de tema
                float fontSize = 12f;
                if (isMainTitle) fontSize = 24f;
                else if (isTechnicalHeader) fontSize = 14f;

                lbl.Font = FontManager.GetFont("Grandstander", fontSize, FontStyle.Bold);
                
                if (name.Contains("historial") || name.Contains("nueva"))
                    lbl.ForeColor = theme.AccentColor;
            }
            else
            {
                lbl.ForeColor = theme.TextSecondary;
                lbl.Font = FontManager.GetFont("Nunito Sans", lbl.Font.Size, FontStyle.Regular);
            }

            if (lbl.BackColor != Color.Transparent)
            {
                if (lbl.Parent != null) lbl.BackColor = lbl.Parent.BackColor;
                else lbl.BackColor = theme.ContentBackground;
            }
        }

        // 3. Botones (IconButton y otros)
        if (control is IconButton btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            
            // Hover Dinámico
            btn.FlatAppearance.MouseOverBackColor = theme.NavBackground == Color.FromArgb(241, 245, 249)
                ? Color.FromArgb(226, 232, 240) 
                : theme.AccentColor ;
            
            btn.ForeColor = theme.TextPrimary;

            string name = btn.Name.ToLower();
            string parentName = btn.Parent?.Name.ToLower() ?? "";
            
            // Detección de botones en cabecera o barras de herramientas superiores
            bool isHeaderBtn = name.Contains("header") || name.Contains("titulo") || 
                              parentName.Contains("titlebar") || parentName.Contains("header") || 
                              parentName.Contains("superior") || parentName.Contains("cabecera") ||
                              parentName.Contains("herramientas") || parentName.Contains("botones");

            if (isHeaderBtn)
                btn.Font = FontManager.GetFont("Grandstander", btn.Font.Size, FontStyle.Bold);
            else
                btn.Font = FontManager.GetFont("Nunito Sans", btn.Font.Size, FontStyle.Regular);

            // Botones de Estado y Acción
            if (name.Contains("guardar") || name.Contains("aceptar") || name.Contains("nuevo"))
            {
                btn.BackColor = theme.StatusSuccess;
                btn.ForeColor = Color.White;
                btn.IconColor = btn.ForeColor;
            }
            else if (name.Contains("cancelar") || name.Contains("eliminar") || name.Contains("desactivar"))
            {
                btn.BackColor = theme.StatusError;
                btn.ForeColor = Color.White;
                btn.IconColor = btn.ForeColor;
            }
            else if (name.Contains("accent") || name.Contains("ingresar") || name.Contains("enviar"))
            {
                btn.BackColor = theme.AccentColor;
                btn.ForeColor = (theme.AccentColor.GetBrightness() > 0.6f) ? theme.TextPrimary : Color.White;
                btn.IconColor = btn.ForeColor;
            }
            else if (name.Contains("close") || name.Contains("maximize") || name.Contains("minimize") || name.Contains("theme"))
            {
                // Botones de la caja de control superior
                btn.BackColor = Color.Transparent;
                btn.ForeColor = theme.AccentColor;
                btn.IconColor = theme.AccentColor;
                
                // Efecto hover destructivo solo para cerrar, el resto Accent oscuro/claro
                if (name.Contains("close"))
                    btn.FlatAppearance.MouseOverBackColor = theme.StatusError;
                else
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, theme.AccentColor);
            }
            else
            {
                // Sidebar Typography
                bool isSidebarButton = name.Contains("menu") || name.Contains("side") || 
                                     (btn.Parent != null && (btn.Parent.Name.ToLower().Contains("menu") || btn.Parent.Name.ToLower().Contains("side")));

                if (isSidebarButton)
                {
                    btn.Font = FontManager.GetFont("Grandstander", 12, FontStyle.Bold);
                }

                if (btn.Parent != null)
                {
                    string pName = btn.Parent.Name.ToLower();
                    if (pName.Contains("menu") || pName.Contains("side") || pName.Contains("herramientas") || pName.Contains("botones"))
                        btn.BackColor = Color.Transparent;
                }
            }
        }

        // 4. Controles de Entrada (Elevación)
        if (control is TextBox txt)
        {
            txt.BackColor = theme.SurfaceColor;
            txt.ForeColor = theme.TextPrimary;
            txt.Font = FontManager.GetFont("Nunito Sans", txt.Font.Size, FontStyle.Regular);
            txt.BorderStyle = BorderStyle.FixedSingle;
            if (theme == ThemeConfiguration.DarkTheme) txt.BorderStyle = BorderStyle.None;
        }

        if (control is ComboBox cbo)
        {
            cbo.BackColor = theme.SurfaceColor;
            cbo.ForeColor = theme.TextPrimary;
            cbo.Font = FontManager.GetFont("Nunito Sans", cbo.Font.Size, FontStyle.Regular);
            cbo.FlatStyle = FlatStyle.Flat;
        }

        if (control is CheckBox chk)
        {
            chk.ForeColor = theme.TextSecondary;
            chk.Font = FontManager.GetFont("Nunito Sans", chk.Font.Size, FontStyle.Regular);
        }

        if (control is DateTimePicker dtp)
        {
            dtp.CalendarMonthBackground = theme.SurfaceColor;
            dtp.CalendarTitleBackColor = theme.HeaderBackground;
            dtp.CalendarTitleForeColor = theme.TextPrimary;
        }

        // 5. DataGridView (Corrección Total)
        if (control is DataGridView grd)
        {
            grd.BackgroundColor = theme.ContentBackground;
            grd.GridColor = theme.DividerColor;
            grd.BorderStyle = BorderStyle.None;
            
            grd.ColumnHeadersDefaultCellStyle.BackColor = theme.HeaderBackground;
            grd.ColumnHeadersDefaultCellStyle.ForeColor = theme.TextPrimary;
            grd.ColumnHeadersDefaultCellStyle.SelectionBackColor = theme.HeaderBackground;
            grd.ColumnHeadersDefaultCellStyle.Font = FontManager.GetFont("Grandstander", 10, FontStyle.Bold);
            grd.EnableHeadersVisualStyles = false;
            
            grd.DefaultCellStyle.BackColor = theme.ContentBackground;
            grd.DefaultCellStyle.ForeColor = theme.TextPrimary;
            grd.DefaultCellStyle.SelectionBackColor = theme.AccentColor;
            grd.DefaultCellStyle.SelectionForeColor = (theme == ThemeConfiguration.DarkTheme) ? Color.Black : Color.White;
            grd.DefaultCellStyle.Font = FontManager.GetFont("Nunito Sans", 9, FontStyle.Regular);
            
            grd.AlternatingRowsDefaultCellStyle.BackColor = theme.SurfaceColor;
            grd.AlternatingRowsDefaultCellStyle.ForeColor = theme.TextPrimary;
        }

        // 7. Formularios
        if (control is IconPictureBox iconPic)
        {
            iconPic.IconColor = theme.AccentColor;
            if (iconPic.Parent != null && (iconPic.Parent.Name.ToLower().Contains("titlebar") || iconPic.Parent.Name.ToLower().Contains("header")))
            {
                iconPic.Size = new Size(48, 48);
                iconPic.IconSize = 48;
            }
        }

        if (control is PictureBox pic)
        {
            if (pic.Parent != null && (pic.Parent.Name.ToLower().Contains("logo") || pic.Parent.Name.ToLower().Contains("menu")))
                pic.BackColor = Color.Transparent;
        }

        if (control is Form frm)
        {
            frm.BackColor = theme.ContentBackground;
        }
    }

    private static bool IsNeutralColor(Color color)
    {
        // Detectar Blanco, Gainsboro, Silver, Gray, etc.
        if (color == Color.Gainsboro || color == Color.White || color == Color.Silver || color == Color.Gray) return true;
        // O si está muy cerca de ser gris/blanco (poco saturado y alto brillo)
        return Math.Abs(color.R - color.G) < 15 && Math.Abs(color.G - color.B) < 15 && color.R > 180;
    }

    private static readonly string _themeFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "theme.txt");

    public static ThemeColors LoadThemePreference()
    {
        string themeName = "Oscuro";
        if (File.Exists(_themeFilePath))
        {
            try { themeName = File.ReadAllText(_themeFilePath); }
            catch { }
        }
        return ThemeConfiguration.GetTheme(themeName);
    }

    public static string LoadThemeName()
    {
        if (File.Exists(_themeFilePath))
        {
            try { return File.ReadAllText(_themeFilePath); }
            catch { }
        }
        return "Oscuro";
    }

    public static void SaveThemePreference(string themeName)
    {
        try { File.WriteAllText(_themeFilePath, themeName); }
        catch { }
    }

    private static Color GetShadowColor(ThemeColors theme)
    {
        return Color.FromArgb(
            Math.Max(0, theme.HeaderBackground.R - 15),
            Math.Max(0, theme.HeaderBackground.G - 15),
            Math.Max(0, theme.HeaderBackground.B - 15)
        );
    }

    private static void PanelTitleBar_Paint(object sender, PaintEventArgs e)
    {
        if (sender is Panel p && p.Tag is ThemeColors theme)
        {
            using var pen = new Pen(theme.DividerColor, 1);
            e.Graphics.DrawLine(pen, 0, p.Height - 1, p.Width, p.Height - 1);
        }
    }
}
