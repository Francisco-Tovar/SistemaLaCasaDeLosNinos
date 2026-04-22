using CasaDeLosNinos.Dominio.Entidades;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public interface IServicioUsuario
{
    Task<IEnumerable<Usuario>> ObtenerTodosAsync();
    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<int> CrearAsync(Usuario usuario, string contrasenaPlana);
    Task<bool> ActualizarAsync(Usuario usuario, string? nuevaContrasenaPlana = null, int idEditorActual = 0);
    Task<bool> CambiarEstadoAsync(int id, bool estado);
    Task<bool> CambiarEstadoAsync(int id, bool estado, int idUsuarioEditor);

    // ── Permisos por módulo ───────────────────────────────────────────────────
    /// <summary>Devuelve los nombres de módulo que tiene habilitados el usuario.</summary>
    Task<IEnumerable<string>> ObtenerPermisosAsync(int idUsuario);

    /// <summary>Otorga acceso a un módulo. Lanza excepción si idUsuario == 1.</summary>
    Task OtorgarPermisoAsync(int idUsuario, string nombreModulo, int idEditorActual);

    /// <summary>Revoca acceso a un módulo. Lanza excepción si idUsuario == 1.</summary>
    Task RevocarPermisoAsync(int idUsuario, string nombreModulo, int idEditorActual);
}

