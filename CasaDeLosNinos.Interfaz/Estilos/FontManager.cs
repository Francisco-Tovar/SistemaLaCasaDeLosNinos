using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace CasaDeLosNinos.Interfaz.Estilos;

/// <summary>
/// Gestiona la carga de fuentes personalizadas desde archivos locales (.ttf)
/// para evitar dependencia de fuentes instaladas en el sistema.
/// </summary>
public static class FontManager
{
    private static readonly PrivateFontCollection _privateFonts = new();
    private static bool _fontsLoaded = false;

    public static void LoadCustomFonts()
    {
        if (_fontsLoaded) return;

        string fontsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "fonts");
        if (!Directory.Exists(fontsPath)) return;

        // Búsqueda recursiva para encontrar fuentes en subcarpetas (static, etc)
        string[] fontFiles = Directory.GetFiles(fontsPath, "*.ttf", SearchOption.AllDirectories);
        foreach (var file in fontFiles)
        {
            try { _privateFonts.AddFontFile(file); }
            catch { /* Ignorar errores de carga de archivos individuales */ }
        }

        _fontsLoaded = true;
    }

    public static Font GetFont(string familyName, float size, FontStyle style = FontStyle.Regular)
    {
        LoadCustomFonts();

        // Buscar en la colección privada
        var family = _privateFonts.Families.FirstOrDefault(f => f.Name.Equals(familyName, StringComparison.OrdinalIgnoreCase));
        
        if (family != null)
        {
            return new Font(family, size, style);
        }

        // Fallback al sistema si no se encontró en la colección privada
        return new Font(familyName, size, style);
    }
}
