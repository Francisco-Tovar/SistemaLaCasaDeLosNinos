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
        { "Oscuro Slate", new ThemeColors(
            NavBackground: Color.FromArgb(15, 23, 42),
            HeaderBackground: Color.FromArgb(21, 31, 51),
            ContentBackground: Color.FromArgb(2, 6, 23),
            AccentColor: Color.FromArgb(56, 189, 248),
            TextPrimary: Color.FromArgb(248, 250, 252),
            TextSecondary: Color.FromArgb(148, 163, 184),
            SurfaceColor: Color.FromArgb(30, 41, 59)
        )},

        { "Claro Slate", new ThemeColors(
            NavBackground: Color.FromArgb(241, 245, 249),
            HeaderBackground: Color.FromArgb(255, 255, 255),
            ContentBackground: Color.FromArgb(248, 250, 252),
            AccentColor: Color.FromArgb(37, 99, 235),
            TextPrimary: Color.FromArgb(2, 6, 23),
            TextSecondary: Color.FromArgb(71, 85, 105),
            SurfaceColor: Color.FromArgb(255, 255, 255)
        )},

        { "Púrpura Profundo", new ThemeColors(
            NavBackground: Color.FromArgb(31, 30, 68),
            HeaderBackground: Color.FromArgb(26, 25, 62),
            ContentBackground: Color.FromArgb(34, 33, 74),
            AccentColor: Color.FromArgb(172, 126, 241),
            TextPrimary: Color.FromArgb(220, 220, 220),
            TextSecondary: Color.FromArgb(150, 150, 150),
            SurfaceColor: Color.FromArgb(37, 36, 81)
        )},

        { "VibrantChildTheme", new ThemeColors(
            NavBackground: Color.FromArgb(126, 87, 194),      // Indigo/Violeta (Sidebar)
            HeaderBackground: Color.FromArgb(255, 255, 255),   // Blanco (Barra de Título)
            ContentBackground: Color.FromArgb(240, 242, 245),  // Gris muy claro (Fondo de escritorio)
            AccentColor: Color.FromArgb(0, 188, 212),         // Cian (Acciones y Gráficos)
            TextPrimary: Color.FromArgb(33, 33, 33),           // Slate oscuro (Legibilidad)
            TextSecondary: Color.FromArgb(117, 117, 117),     // Gris medio (Labels)
            SurfaceColor: Color.FromArgb(255, 255, 255)        // Blanco (Tarjetas e Inputs)
        )}

    };

    public static IEnumerable<string> GetThemeNames() => _themes.Keys;

    public static ThemeColors GetTheme(string name)
    {
        if (_themes.TryGetValue(name, out var theme)) return theme;
        return _themes["Oscuro Slate"]; // Default
    }

    // Para compatibilidad rápida si se requiere
    public static ThemeColors DarkTheme => _themes["Oscuro Slate"];
    public static ThemeColors LightTheme => _themes["Claro Slate"];    
}
