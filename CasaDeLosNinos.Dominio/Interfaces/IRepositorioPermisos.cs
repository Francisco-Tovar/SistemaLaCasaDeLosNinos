using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Repositorio de permisos por módulo para un usuario.
/// </summary>
public interface IRepositorioPermisos
{
    /// <summary>Obtiene todos los nombres de módulo que tiene permitidos el usuario.</summary>
    Task<IEnumerable<string>> ObtenerNombresPorUsuarioAsync(int idUsuario);

    /// <summary>Otorga acceso a un módulo. Si ya existe, no hace nada (IGNORE).</summary>
    Task OtorgarAsync(int idUsuario, string nombreModulo);

    /// <summary>Revoca acceso a un módulo. Si no existe, no hace nada.</summary>
    Task RevocarAsync(int idUsuario, string nombreModulo);

    /// <summary>Inserta los permisos por defecto para un usuario recién creado (Ninos + Asistencia).</summary>
    Task InsertarPermisosDefaultAsync(int idUsuario);

    /// <summary>Otorga acceso a todos los módulos disponibles (para administradores).</summary>
    Task OtorgarTodoAsync(int idUsuario);
}
