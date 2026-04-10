using System.Drawing;
using System.Collections.Generic;

namespace CasaDeLosNinos.Interfaz.Estilos;

/// <summary>
/// Sistema de temas extensible. Permite agregar múltiples paletas de colores.
/// </summary>
public static class ThemeConfiguration
{
    private static readonly Dictionary<string, ThemeColors> _themes = new()
    {
        { "Oscuro", new ThemeColors(
            NavBackground: Color.FromArgb(15, 23, 42),
            HeaderBackground: Color.FromArgb(21, 31, 51),
            ContentBackground: Color.FromArgb(2, 6, 23),
            AccentColor: Color.FromArgb(56, 189, 248),
            TextPrimary: Color.FromArgb(248, 250, 252),
            TextSecondary: Color.FromArgb(148, 163, 184),
            SurfaceColor: Color.FromArgb(30, 41, 59),
            StatusSuccess: Color.FromArgb(34, 197, 94),
            StatusError: Color.FromArgb(239, 68, 68),
            DividerColor: Color.FromArgb(51, 65, 85)
        )},

        { "Claro", new ThemeColors(
            NavBackground: Color.FromArgb(241, 245, 249),
            HeaderBackground: Color.FromArgb(255, 255, 255),
            ContentBackground: Color.FromArgb(248, 250, 252),
            AccentColor: Color.FromArgb(37, 99, 235),
            TextPrimary: Color.FromArgb(2, 6, 23),
            TextSecondary: Color.FromArgb(71, 85, 105),
            SurfaceColor: Color.FromArgb(255, 255, 255),
            StatusSuccess: Color.FromArgb(22, 163, 74),
            StatusError: Color.FromArgb(220, 38, 38),
            DividerColor: Color.FromArgb(226, 232, 240)
        )},

        { "Púrpura", new ThemeColors(
            NavBackground: Color.FromArgb(31, 30, 68),
            HeaderBackground: Color.FromArgb(26, 25, 62),
            ContentBackground: Color.FromArgb(34, 33, 74),
            AccentColor: Color.FromArgb(172, 126, 241),
            TextPrimary: Color.FromArgb(220, 220, 220),
            TextSecondary: Color.FromArgb(150, 150, 150),
            SurfaceColor: Color.FromArgb(37, 36, 81),
            StatusSuccess: Color.FromArgb(24, 203, 150),
            StatusError: Color.FromArgb(255, 87, 87),
            DividerColor: Color.FromArgb(45, 43, 90)
        )},

        { "Jugueton", new ThemeColors(
            NavBackground: Color.FromArgb(126, 87, 194),
            HeaderBackground: Color.FromArgb(255, 255, 255),
            ContentBackground: Color.FromArgb(240, 242, 245),
            AccentColor: Color.FromArgb(0, 188, 212),
            TextPrimary: Color.FromArgb(33, 33, 33),
            TextSecondary: Color.FromArgb(117, 117, 117),
            SurfaceColor: Color.FromArgb(255, 255, 255),
            StatusSuccess: Color.FromArgb(0, 200, 83),
            StatusError: Color.FromArgb(255, 61, 0),
            DividerColor: Color.FromArgb(224, 224, 224)
        )},
        { "Institucional", new ThemeColors(
            NavBackground: Color.FromArgb(0, 143, 143),      // VERDE TEAL (image_624c98.png)
            HeaderBackground: Color.FromArgb(255, 255, 255), // Blanco Puro
            ContentBackground: Color.FromArgb(245, 247, 250),// Gris Hueso
            AccentColor: Color.FromArgb(224, 168, 0),       // AMARILLO (image_625040.png)
            TextPrimary: Color.FromArgb(30, 40, 50),
            TextSecondary: Color.FromArgb(100, 115, 130),
            SurfaceColor: Color.FromArgb(255, 255, 255),
            StatusSuccess: Color.FromArgb(141, 198, 63),     // Verde Manzana Logo
            StatusError: Color.FromArgb(120, 28, 81),       // VIOLETA-ROJIZO "DONAR" (image_624d53.png)
            DividerColor: Color.FromArgb(224, 224, 224)
        )}
    };

    public static IEnumerable<string> GetThemeNames() => _themes.Keys;

    public static ThemeColors GetTheme(string name)
    {
        if (_themes.TryGetValue(name, out var theme)) return theme;
        return _themes["Oscuro"]; // Default
    }

    // Para compatibilidad rápida si se requiere
    public static ThemeColors DarkTheme => _themes["Oscuro"];
    public static ThemeColors LightTheme => _themes["Claro"];    
}
