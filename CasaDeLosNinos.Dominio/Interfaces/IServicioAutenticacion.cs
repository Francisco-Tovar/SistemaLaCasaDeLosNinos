using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

public interface IServicioAutenticacion
{
    /// <summary>
    /// Valida las credenciales y retorna el Usuario si es exitoso, null de lo contrario.
    /// </summary>
    Task<Usuario?> ValidarCredencialesAsync(string usuario, string contrasena);

    /// <summary>
    /// Crea el usuario admin por defecto si no existen usuarios en el sistema.
    /// </summary>
    Task AsegurarUsuarioAdminPorDefectoAsync();
}
