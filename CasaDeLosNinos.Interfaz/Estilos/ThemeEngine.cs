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
                p.BackColor = theme.HeaderBackground;
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
            // Títulos y etiquetas principales
            if (name.Contains("title") || name.Contains("titulo") || name.Contains("bienvenida") || name.Contains("header") || name.Contains("org") || name.Contains("historial") || name.Contains("nueva"))
            {
                lbl.ForeColor = theme.TextPrimary;
                // Aumento de prominencia solicitado (250%) para títulos principales
                if (name.Contains("child") || name.Contains("titulo")) 
                    lbl.Font = new Font(lbl.Font.FontFamily, 22, FontStyle.Bold);
                
                // Si es un header de sección interno, darle un toque de acento o prominencia
                if (name.Contains("historial") || name.Contains("nueva"))
                    lbl.ForeColor = theme.AccentColor;
            }
            else
            {
                lbl.ForeColor = theme.TextSecondary;
            }

            // Asegurar que el fondo del label coincida con su contenedor si no es transparente
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

            // Botones de Acción Principal (Accent / Ingresar / Guardar / Nuevo)
            if (name.Contains("accent") || name.Contains("ingresar") || name.Contains("guardar") || name.Contains("nuevo") || name.Contains("enviar"))
            {
                btn.BackColor = theme.TextSecondary;
                // Calculamos contraste para el texto sobre el AccentColor
                btn.ForeColor = (theme.AccentColor.GetBrightness() < 0.5f) ? Color.White : Color.Black;
                btn.IconColor = btn.ForeColor;
            }
            else
            {
                // Sidebar Highlight Persistence: Si el botón tiene el fondo activo (usualmente Accent o transparente con borde)
                // En el sidebar mantenemos el color de acento si ya estaba activo
                if (name.Contains("menu") || name.Contains("side"))
                {
                    // Si el botón está "seleccionado" (lo detectamos por el color de su icono si ya fue activado)
                    // o simplemente dejamos que FormPrincipal lo maneje tras el ApplyTheme
                }

                // Forzar IconColor al texto primario en cabeceras
                if (btn.Parent != null && (btn.Parent.Name.ToLower().Contains("titlebar") || btn.Parent.Name.ToLower().Contains("header") || btn.Parent.Name.ToLower().Contains("menu") || btn.Parent.Name.ToLower().Contains("side")))
                {
                    btn.IconColor = theme.TextPrimary;
                }
                else if (IsNeutralColor(btn.IconColor))
                {
                    btn.IconColor = theme.TextPrimary;
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
            txt.BorderStyle = BorderStyle.FixedSingle;
            if (theme == ThemeConfiguration.DarkTheme) txt.BorderStyle = BorderStyle.None;
        }

        if (control is ComboBox cbo)
        {
            cbo.BackColor = theme.SurfaceColor;
            cbo.ForeColor = theme.TextPrimary;
            cbo.FlatStyle = FlatStyle.Flat;
        }

        if (control is CheckBox chk)
        {
            chk.ForeColor = theme.TextSecondary;
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
            grd.GridColor = theme.SurfaceColor;
            grd.BorderStyle = BorderStyle.None;
            
            grd.ColumnHeadersDefaultCellStyle.BackColor = theme.HeaderBackground;
            grd.ColumnHeadersDefaultCellStyle.ForeColor = theme.TextPrimary;
            grd.ColumnHeadersDefaultCellStyle.SelectionBackColor = theme.HeaderBackground;
            grd.EnableHeadersVisualStyles = false;
            
            grd.DefaultCellStyle.BackColor = theme.ContentBackground;
            grd.DefaultCellStyle.ForeColor = theme.TextPrimary;
            grd.DefaultCellStyle.SelectionBackColor = theme.AccentColor;
            grd.DefaultCellStyle.SelectionForeColor = (theme == ThemeConfiguration.DarkTheme) ? Color.Black : Color.White;
            
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
        string themeName = "Oscuro Slate";
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
        return "Oscuro Slate";
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
}
