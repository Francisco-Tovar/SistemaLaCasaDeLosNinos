using System.Drawing;

namespace CasaDeLosNinos.Interfaz.Estilos;

/// <summary>
/// Define la paleta de colores centralizada para un tema.
/// </summary>
public record ThemeColors(
    Color NavBackground,
    Color HeaderBackground,
    Color ContentBackground,
    Color AccentColor,
    Color TextPrimary,
    Color TextSecondary,
    Color SurfaceColor,
    Color StatusSuccess,
    Color StatusError,
    Color DividerColor
);
