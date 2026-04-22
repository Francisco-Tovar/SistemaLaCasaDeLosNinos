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
        )},

        // ── Temas de Colorffy Featured Palettes ────────────────────────────────

        // Palette 1 – Wanderlust (#244A6B, #5D8ECC, #A0E7E5, #F4EAD5, #E8AE1C)
        { "Wanderlust", new ThemeColors(
            NavBackground:     Color.FromArgb(36,  74, 107),   // #244A6B – Azul marino profundo
            HeaderBackground:  Color.FromArgb(93, 142, 204),   // #5D8ECC – Azul cielo
            ContentBackground: Color.FromArgb(244, 234, 213),  // #F4EAD5 – Crema arena
            AccentColor:       Color.FromArgb(232, 174,  28),  // #E8AE1C – Dorado viajero
            TextPrimary:       Color.FromArgb(36,  74, 107),   // #244A6B
            TextSecondary:     Color.FromArgb(93, 142, 204),   // #5D8ECC
            SurfaceColor:      Color.FromArgb(255, 255, 255),
            StatusSuccess:     Color.FromArgb(160, 231, 229),  // #A0E7E5 – Agua menta
            StatusError:       Color.FromArgb(205,  60,  60),
            DividerColor:      Color.FromArgb(214, 204, 183)
        )},

        // Palette 2 – Orion (#0B0D17, #2D3A54, #A3C9E2, #F4F6F8, #D4AF37)
        { "Orion", new ThemeColors(
            NavBackground:     Color.FromArgb(11,  13,  23),   // #0B0D17 – Negro cosmos
            HeaderBackground:  Color.FromArgb(45,  58,  84),   // #2D3A54 – Azul medianoche
            ContentBackground: Color.FromArgb(11,  13,  23),   // #0B0D17
            AccentColor:       Color.FromArgb(212, 175,  55),  // #D4AF37 – Dorado estelar
            TextPrimary:       Color.FromArgb(244, 246, 248),  // #F4F6F8
            TextSecondary:     Color.FromArgb(163, 201, 226),  // #A3C9E2
            SurfaceColor:      Color.FromArgb(45,  58,  84),
            StatusSuccess:     Color.FromArgb(163, 201, 226),  // Azul nebulosa
            StatusError:       Color.FromArgb(220,  60,  60),
            DividerColor:      Color.FromArgb(45,  58,  84)
        )},

        // Palette 3 – World Cup (#7000FF, #D11010, #7B1313, #CFF023, #15C651)
        { "World Cup", new ThemeColors(
            NavBackground:     Color.FromArgb(112,   0, 255),  // #7000FF – Violeta intenso
            HeaderBackground:  Color.FromArgb(123,  19,  19),  // #7B1313 – Rojo oscuro
            ContentBackground: Color.FromArgb(20,  20,  30),
            AccentColor:       Color.FromArgb(207, 240,  35),  // #CFF023 – Lima eléctrico
            TextPrimary:       Color.FromArgb(255, 255, 255),
            TextSecondary:     Color.FromArgb(207, 240,  35),
            SurfaceColor:      Color.FromArgb(123,  19,  19),
            StatusSuccess:     Color.FromArgb(21, 198,  81),   // #15C651 – Verde gol
            StatusError:       Color.FromArgb(209,  16,  16),  // #D11010
            DividerColor:      Color.FromArgb(70,   0, 150)
        )},

        // Palette 4 – Weather (#4682B4, #87CEEB, #ADD8E6, #F8F8FF, #F5F5DC)
        { "Weather", new ThemeColors(
            NavBackground:     Color.FromArgb(70, 130, 180),   // #4682B4 – Azul cielo despejado
            HeaderBackground:  Color.FromArgb(135, 206, 235),  // #87CEEB
            ContentBackground: Color.FromArgb(248, 248, 255),  // #F8F8FF – Blanco fantasma
            AccentColor:       Color.FromArgb(255, 120,   0),  // Ámbar naranja – visible sobre azul NAV y sobre contenido blanco
            TextPrimary:       Color.FromArgb(40,  60,  90),   // Azul oscuro – legible sobre contenido claro
            TextSecondary:     Color.FromArgb(100, 140, 180),
            SurfaceColor:      Color.FromArgb(255, 255, 255),
            StatusSuccess:     Color.FromArgb(100, 200, 130),
            StatusError:       Color.FromArgb(220,  80,  80),
            DividerColor:      Color.FromArgb(173, 216, 230)   // #ADD8E6
        )},

        // Palette 5 – Knightfall (#380202, #8B0000, #EF0000, #FF6666)
        { "Knightfall", new ThemeColors(
            NavBackground:     Color.FromArgb(56,   2,   2),   // #380202 – Negro sangre
            HeaderBackground:  Color.FromArgb(139,   0,   0),  // #8B0000 – Rojo oscuro
            ContentBackground: Color.FromArgb(25,   0,   0),
            AccentColor:       Color.FromArgb(239,   0,   0),  // #EF0000 – Rojo brillante
            TextPrimary:       Color.FromArgb(255, 220, 220),
            TextSecondary:     Color.FromArgb(255, 102, 102),  // #FF6666
            SurfaceColor:      Color.FromArgb(139,   0,   0),
            StatusSuccess:     Color.FromArgb(100, 200, 100),
            StatusError:       Color.FromArgb(239,   0,   0),
            DividerColor:      Color.FromArgb(90,   10,  10)
        )},

        // Palette 6 – Coastal Calm (#4682B4, #87CEEB, #F5DEB3, #D2B48C)
        { "Coastal Calm", new ThemeColors(
            NavBackground:     Color.FromArgb(70, 130, 180),   // #4682B4 – Azul océano
            HeaderBackground:  Color.FromArgb(255, 255, 255),
            ContentBackground: Color.FromArgb(245, 222, 179),  // #F5DEB3 – Arena dorada
            AccentColor:       Color.FromArgb(180,  90,  20),  // Terracota quemada – visible sobre azul NAV y sobre arena
            TextPrimary:       Color.FromArgb(50,  40,  30),   // Marrón oscuro – legible sobre arena
            TextSecondary:     Color.FromArgb(100, 80,  60),
            SurfaceColor:      Color.FromArgb(255, 255, 255),
            StatusSuccess:     Color.FromArgb(80, 160, 110),
            StatusError:       Color.FromArgb(200,  70,  70),
            DividerColor:      Color.FromArgb(210, 180, 140)   // #D2B48C – Tan
        )},

        // Palette 7 – Desert Oasis (#E76F51, #F4A261, #E9C46A, #2A9D8F, #264653)
        { "Desert Oasis", new ThemeColors(
            NavBackground:     Color.FromArgb(38,  70,  83),   // #264653 – Verde oscuro
            HeaderBackground:  Color.FromArgb(42, 157, 143),   // #2A9D8F – Turquesa
            ContentBackground: Color.FromArgb(253, 246, 235),
            AccentColor:       Color.FromArgb(231, 111,  81),  // #E76F51 – Terracota
            TextPrimary:       Color.FromArgb(38,  70,  83),
            TextSecondary:     Color.FromArgb(100, 120, 130),
            SurfaceColor:      Color.FromArgb(255, 255, 255),
            StatusSuccess:     Color.FromArgb(42, 157, 143),   // Turquesa
            StatusError:       Color.FromArgb(231, 111,  81),  // Terracota
            DividerColor:      Color.FromArgb(233, 196, 106)   // #E9C46A – Amarillo arena
        )},

        // Palette 8 – Equality (#E71D73, #F49AC2, #F8BBD0, #6AAFE6, #2C3E50)
        { "Equality", new ThemeColors(
            NavBackground:     Color.FromArgb(44,  62,  80),   // #2C3E50 – Azul noche
            HeaderBackground:  Color.FromArgb(231,  29, 115),  // #E71D73 – Rosa intenso
            ContentBackground: Color.FromArgb(248, 187, 208),  // #F8BBD0 – Rosa pálido
            AccentColor:       Color.FromArgb(106, 175, 230),  // #6AAFE6 – Azul esperanza
            TextPrimary:       Color.FromArgb(44,  62,  80),
            TextSecondary:     Color.FromArgb(100, 100, 120),
            SurfaceColor:      Color.FromArgb(255, 255, 255),
            StatusSuccess:     Color.FromArgb(106, 175, 230),
            StatusError:       Color.FromArgb(231,  29, 115),
            DividerColor:      Color.FromArgb(244, 154, 194)   // #F49AC2
        )},

        // Palette 9 – Tulip (#E7005A, #FD7A81, #FFDE59, #ADC973, #6B8E23)
        { "Tulip", new ThemeColors(
            NavBackground:     Color.FromArgb(107, 142,  35),  // #6B8E23 – Verde oliva
            HeaderBackground:  Color.FromArgb(255, 255, 255),
            ContentBackground: Color.FromArgb(255, 250, 235),
            AccentColor:       Color.FromArgb(231,   0,  90),  // #E7005A – Rosa tulipán
            TextPrimary:       Color.FromArgb(50,  60,  20),
            TextSecondary:     Color.FromArgb(107, 142,  35),
            SurfaceColor:      Color.FromArgb(255, 255, 255),
            StatusSuccess:     Color.FromArgb(173, 201, 115),  // #ADC973 – Verde jardín
            StatusError:       Color.FromArgb(253, 122, 129),  // #FD7A81 – Rosa coral
            DividerColor:      Color.FromArgb(255, 222,  89)   // #FFDE59 – Amarillo sol
        )},

        // Palette 10 – Cupid's Charm (#E6AACE, #D98CAF, #C7709D, #A84C7D, #87305C)
        { "Cupid's Charm", new ThemeColors(
            NavBackground:     Color.FromArgb(135,  48,  92),  // #87305C – Magenta oscuro
            HeaderBackground:  Color.FromArgb(168,  76, 125),  // #A84C7D
            ContentBackground: Color.FromArgb(255, 245, 250),
            AccentColor:       Color.FromArgb(199, 112, 157),  // #C7709D – Rosa intenso
            TextPrimary:       Color.FromArgb(80,  20,  50),
            TextSecondary:     Color.FromArgb(168,  76, 125),
            SurfaceColor:      Color.FromArgb(255, 255, 255),
            StatusSuccess:     Color.FromArgb(140, 200, 140),
            StatusError:       Color.FromArgb(199, 112, 157),
            DividerColor:      Color.FromArgb(230, 170, 206)   // #E6AACE – Rosa suave
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
