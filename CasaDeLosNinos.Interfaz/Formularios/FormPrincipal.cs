using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Interfaz.Formularios;

/// <summary>
/// Formulario principal de la aplicación.
/// Muestra información básica de configuración al iniciar.
/// </summary>
public class FormPrincipal : Form
{
    private readonly IConfiguration _configuracion;

    public FormPrincipal(IConfiguration configuracion)
    {
        _configuracion = configuracion;
        InicializarComponentes();
    }

    private void InicializarComponentes()
    {
        var nombreOrganizacion = _configuracion["Configuracion:NombreOrganizacion"]
            ?? "Sistema de Gestión";
        var versionSistema = _configuracion["Configuracion:VersionSistema"]
            ?? "0.0.0";

        Text = $"{nombreOrganizacion} — v{versionSistema}";
        Size = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(800, 500);

        var etiquetaBienvenida = new Label
        {
            Text = $"Bienvenido al sistema de gestión de\n{nombreOrganizacion}",
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleCenter
        };

        // Centrar la etiqueta en el formulario
        etiquetaBienvenida.Location = new Point(
            (ClientSize.Width - etiquetaBienvenida.PreferredWidth) / 2,
            (ClientSize.Height - etiquetaBienvenida.PreferredHeight) / 2
        );

        Controls.Add(etiquetaBienvenida);
        Resize += (s, e) =>
        {
            etiquetaBienvenida.Location = new Point(
                (ClientSize.Width - etiquetaBienvenida.PreferredWidth) / 2,
                (ClientSize.Height - etiquetaBienvenida.PreferredHeight) / 2
            );
        };
    }
}
