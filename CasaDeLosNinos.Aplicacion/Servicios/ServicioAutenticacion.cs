using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using BCrypt.Net;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioAutenticacion : IServicioAutenticacion
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IRepositorioPermisos _repositorioPermisos;
    private readonly IServicioAuditoria _servicioAuditoria;

    public ServicioAutenticacion(IRepositorioUsuario repositorioUsuario, 
                                IRepositorioPermisos repositorioPermisos,
                                IServicioAuditoria servicioAuditoria)
    {
        _repositorioUsuario = repositorioUsuario;
        _repositorioPermisos = repositorioPermisos;
        _servicioAuditoria = servicioAuditoria;
    }

    public async Task<Usuario?> ValidarCredencialesAsync(string usuario, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            return null;

        var dbUsuario = await _repositorioUsuario.ObtenerPorNombreUsuarioAsync(usuario);

        if (dbUsuario == null)
            return null;

        // Verificamos el hash con BCrypt
        bool esValida = BCrypt.Net.BCrypt.Verify(contrasena, dbUsuario.ContrasenaHash);

        if (esValida)
        {
            await _servicioAuditoria.RegistrarAccionAsync(dbUsuario.Id, "Seguridad", "Login", "Inicio de sesión exitoso.");
            return dbUsuario;
        }
        else
        {
            await _servicioAuditoria.RegistrarAccionAsync(null, "Seguridad", "Login Fallido", $"Intento fallido para el usuario: {usuario}");
            return null;
        }
    }

    public async Task AsegurarUsuarioAdminPorDefectoAsync()
    {
        bool existeAdmin = await _repositorioUsuario.ExisteAdminAsync();

        if (!existeAdmin)
        {
            // Creamos el hash para "admin123"
            string hashDefault = BCrypt.Net.BCrypt.HashPassword("admin123");

            var admin = new Usuario
            {
                NombreCompleto = "Administrador del Sistema",
                NombreUsuario = "admin",
                ContrasenaHash = hashDefault,
                IdRol = 1, // Administrador (Id 1 insertado en la semilla de la BD en Datos)
                Activo = true
            };

            int idAdmin = await _repositorioUsuario.InsertarAsync(admin);
            await _repositorioPermisos.OtorgarTodoAsync(idAdmin);
        }
        else
        {
            // Caso borde: El usuario existe pero tal vez no tiene los permisos (primera ejecución fallida)
            var admin = await _repositorioUsuario.ObtenerPorNombreUsuarioAsync("admin");
            if (admin != null) await _repositorioPermisos.OtorgarTodoAsync(admin.Id);
        }

        // Semilla para usuario de prueba (Funcionario)
        bool existeUsuario = await _repositorioUsuario.NombreUsuarioExisteAsync("usuario");
        if (!existeUsuario)
        {
            string hashPrueba = BCrypt.Net.BCrypt.HashPassword("password1");
            var nUsuario = new Usuario
            {
                NombreCompleto = "Usuario de Pruebas",
                NombreUsuario = "usuario",
                ContrasenaHash = hashPrueba,
                IdRol = 2, // Funcionario
                Activo = true
            };
            int idUsuario = await _repositorioUsuario.InsertarAsync(nUsuario);
            await _repositorioPermisos.InsertarPermisosDefaultAsync(idUsuario);
        }
    }
}
