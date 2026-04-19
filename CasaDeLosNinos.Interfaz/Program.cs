using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CasaDeLosNinos.Datos;
using CasaDeLosNinos.Datos.Repositorios;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using CasaDeLosNinos.Aplicacion.Servicios;
using CasaDeLosNinos.Interfaz.Formularios;

namespace CasaDeLosNinos.Interfaz;

/// <summary>
/// Punto de entrada de la aplicación.
/// Configura: lectura de appsettings.json, inyección de dependencias
/// y manejadores globales de excepciones.
/// </summary>
internal static class Program
{
    [STAThread]
    static void Main()
    {
        // ──────────────────────────────────────────────
        // 1. MANEJADORES GLOBALES DE EXCEPCIONES
        // ──────────────────────────────────────────────

        // Captura excepciones no manejadas en el hilo de la UI (WinForms)
        Application.ThreadException += AlManejarExcepcionDeHilo;
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        // Captura excepciones en hilos secundarios (Task, ThreadPool)
        AppDomain.CurrentDomain.UnhandledException += AlManejarExcepcionNoControlada;

        // ──────────────────────────────────────────────
        // 2. CONFIGURACIÓN DESDE appsettings.json
        // ──────────────────────────────────────────────

        IConfiguration configuracion;
        try
        {
            configuracion = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            // Interceptor: Redirigir a la carpeta database de la raíz si estamos en desarrollo
            if (AppContext.BaseDirectory.Contains("bin"))
            {
                var dictOverride = new Dictionary<string, string?>();
                string proyectoRaiz = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                
                if (Directory.Exists(Path.Combine(proyectoRaiz, "database")))
                {
                    foreach (var cs in configuracion.GetSection("ConnectionStrings").GetChildren())
                    {
                        if (cs.Value != null && cs.Value.Contains("database/"))
                        {
                            string nombreDb = cs.Value.Split('=')[1];
                            string rutaAbsoluta = Path.Combine(proyectoRaiz, nombreDb);
                            dictOverride[$"ConnectionStrings:{cs.Key}"] = $"Data Source={rutaAbsoluta}";
                        }
                    }
                }

                if (dictOverride.Count > 0)
                {
                    configuracion = new ConfigurationBuilder()
                        .AddConfiguration(configuracion)
                        .AddInMemoryCollection(dictOverride)
                        .Build();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"No se pudo cargar el archivo de configuración 'appsettings.json'.\n\n" +
                $"Verifique que el archivo exista junto al ejecutable.\n\nDetalle: {ex.Message}",
                "Error de Configuración",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        // ──────────────────────────────────────────────
        // 3. INYECCIÓN DE DEPENDENCIAS
        // ──────────────────────────────────────────────

        var servicios = new ServiceCollection();

        // Configuración (Singleton)
        servicios.AddSingleton<IConfiguration>(configuracion);

        // Inicializador de base de datos (Singleton — se ejecuta una vez al inicio)
        servicios.AddSingleton<IInicializadorBaseDatos, InicializadorBaseDatos>();

        // Repositorios (Transient — se crean por solicitud)
        servicios.AddTransient<IRepositorioUsuario,     RepositorioUsuario>();
        servicios.AddTransient<IRepositorioRol,         RepositorioRol>();
        servicios.AddTransient<IRepositorioNino,        RepositorioNino>();
        servicios.AddTransient<IRepositorioAsistencia,  RepositorioAsistencia>();
        servicios.AddTransient<IRepositorioObservacion, RepositorioObservacion>();
        servicios.AddTransient<IRepositorioFoto,        RepositorioFoto>();
        servicios.AddTransient<IRepositorioVoluntario,  RepositorioVoluntario>();
        servicios.AddTransient<IRepositorioRegistroHoras, RepositorioRegistroHoras>();
        servicios.AddTransient<IRepositorioCajaChica,   RepositorioCajaChica>();

        // Servicios (Transient)
        servicios.AddTransient<IServicioAutenticacion, ServicioAutenticacion>();
        servicios.AddTransient<IServicioUsuario,       ServicioUsuario>();
        servicios.AddTransient<IServicioNino,          ServicioNino>();
        servicios.AddTransient<IServicioAsistencia,    ServicioAsistencia>();
        servicios.AddTransient<IServicioObservacion,   ServicioObservacion>();
        servicios.AddTransient<IServicioFoto,          ServicioFoto>();
        servicios.AddTransient<IServicioVoluntario,    ServicioVoluntario>();
        servicios.AddTransient<IServicioRegistroHoras, ServicioRegistroHoras>();
        servicios.AddTransient<IServicioCajaChica,     ServicioCajaChica>();

        // Formularios (Transient)
        servicios.AddTransient<FrmLogin>();

        var proveedor = servicios.BuildServiceProvider();

        // ──────────────────────────────────────────────
        // 4. INICIALIZACIÓN DE BASE DE DATOS
        // ──────────────────────────────────────────────

        // Excepción válida al skill: GetAwaiter().GetResult() solo en Program.Main()
        // porque WinForms exige un punto de entrada síncrono [STAThread].
        try
        {
            var inicializador = proveedor.GetRequiredService<IInicializadorBaseDatos>();
            inicializador.InicializarAsync().GetAwaiter().GetResult();

            // ──────────────────────────────────────────────
            // 5. SEMILLA DE SEGURIDAD (ADMIN POR DEFECTO)
            // ──────────────────────────────────────────────
            var authService = proveedor.GetRequiredService<IServicioAutenticacion>();
            authService.AsegurarUsuarioAdminPorDefectoAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            RegistrarErrorEnArchivo(ex);
            MessageBox.Show(
                "No se pudo inicializar la base de datos o la seguridad.\n\n" +
                $"Detalle: {ex.Message}",
                "Error de Inicialización",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        // ──────────────────────────────────────────────
        // 6. FLUJO DE ACCESO (LOGIN)
        // ──────────────────────────────────────────────

        ApplicationConfiguration.Initialize();

        while (true)
        {
            using var frmLogin = proveedor.GetRequiredService<FrmLogin>();
            if (frmLogin.ShowDialog() != DialogResult.OK) break;

            var usuarioAutenticado = frmLogin.UsuarioAutenticado
                ?? throw new InvalidOperationException("Usuario autenticado no disponible.");

            using (var formPrincipal = new FormPrincipal(
                proveedor.GetRequiredService<IConfiguration>(),
                proveedor,
                usuarioAutenticado))
            {
                Application.Run(formPrincipal);
                
                // Si el usuario simplemente cerró la ventana sin marcar logout, salimos del bucle.
                if (!formPrincipal.DeseaCerrarSesion) break;
            }
        }
    }

    // ══════════════════════════════════════════════════
    // MANEJADORES GLOBALES
    // ══════════════════════════════════════════════════

    /// <summary>
    /// Maneja excepciones no controladas en el hilo principal de WinForms.
    /// Muestra un mensaje amigable y registra el error en archivo de log.
    /// </summary>
    private static void AlManejarExcepcionDeHilo(object sender, System.Threading.ThreadExceptionEventArgs e)
    {
        RegistrarErrorEnArchivo(e.Exception);
        MessageBox.Show(
            "Ha ocurrido un error inesperado.\n\n" +
            "El problema ha sido registrado. Si el error persiste,\n" +
            "contacte al administrador del sistema.",
            "Error del Sistema",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    /// <summary>
    /// Maneja excepciones no controladas en hilos secundarios.
    /// Registra el error y muestra alerta si es posible.
    /// </summary>
    private static void AlManejarExcepcionNoControlada(object sender, UnhandledExceptionEventArgs e)
    {
        if (e.ExceptionObject is Exception excepcion)
        {
            RegistrarErrorEnArchivo(excepcion);
            MessageBox.Show(
                "Ha ocurrido un error crítico en un proceso secundario.\n\n" +
                "El problema ha sido registrado. La aplicación intentará continuar.",
                "Error Crítico",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Registra la excepción en un archivo de texto plano dentro de la carpeta logs/.
    /// Nunca lanza excepciones — si el log falla, se ignora silenciosamente.
    /// </summary>
    private static void RegistrarErrorEnArchivo(Exception excepcion)
    {
        try
        {
            var rutaLogs = Path.Combine(AppContext.BaseDirectory, "logs");
            Directory.CreateDirectory(rutaLogs);

            var rutaArchivo = Path.Combine(rutaLogs, "errores.log");
            var entrada =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                $"{excepcion.GetType().Name}: {excepcion.Message}\n" +
                $"{excepcion.StackTrace}\n" +
                $"{"".PadRight(80, '─')}\n";

            File.AppendAllText(rutaArchivo, entrada);
        }
        catch
        {
            // Nunca fallar dentro del manejador de errores
        }
    }
}