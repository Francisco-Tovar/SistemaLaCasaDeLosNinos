using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using BCrypt.Net;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioUsuario : IServicioUsuario
{
    private readonly IRepositorioUsuario _repositorioUsuario;
    private readonly IRepositorioPermisos _repositorioPermisos;
    private readonly IServicioAuditoria _servicioAuditoria;

    public ServicioUsuario(IRepositorioUsuario repositorioUsuario, IRepositorioPermisos repositorioPermisos, IServicioAuditoria servicioAuditoria)
    {
        _repositorioUsuario = repositorioUsuario;
        _repositorioPermisos = repositorioPermisos;
        _servicioAuditoria = servicioAuditoria;
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        return await _repositorioUsuario.ObtenerTodosAsync();
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        return await _repositorioUsuario.ObtenerPorIdAsync(id);
    }

    public async Task<int> CrearAsync(Usuario usuario, string contrasenaPlana)
    {
        if (await _repositorioUsuario.NombreUsuarioExisteAsync(usuario.NombreUsuario))
        {
            throw new InvalidOperationException("El nombre de usuario ya está en uso.");
        }

        if (string.IsNullOrWhiteSpace(contrasenaPlana))
        {
            throw new InvalidOperationException("La contraseña es requerida para nuevos usuarios.");
        }

        usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(contrasenaPlana);
        int nuevoId = await _repositorioUsuario.InsertarAsync(usuario);

        // Otorgar permisos por defecto: Niños y Asistencia
        await _repositorioPermisos.InsertarPermisosDefaultAsync(nuevoId);

        await _servicioAuditoria.RegistrarAccionAsync(null, "Usuarios", "Creación", 
            $"Se creó la cuenta de usuario: {usuario.NombreUsuario}");

        return nuevoId;
    }

    public async Task<bool> ActualizarAsync(Usuario usuario, string? nuevaContrasenaPlana = null, int idEditorActual = 0)
    {
        if (await _repositorioUsuario.NombreUsuarioExisteAsync(usuario.NombreUsuario, usuario.Id))
        {
            throw new InvalidOperationException("El nombre de usuario ya está en uso por otra cuenta.");
        }

        var usuarioExistente = await _repositorioUsuario.ObtenerPorIdAsync(usuario.Id);
        if (usuarioExistente == null) return false;

        // El administrador maestro (Id=1) nunca puede perder su rol
        if (usuarioExistente.Id == 1 && usuario.IdRol != 1)
        {
            throw new InvalidOperationException("No se puede cambiar el rol del administrador maestro del sistema.");
        }

        // Un administrador no puede degradarse a sí mismo
        if (idEditorActual != 0 && idEditorActual == usuario.Id
            && usuarioExistente.IdRol == 1 && usuario.IdRol != 1)
        {
            throw new InvalidOperationException("Un administrador no puede cambiar su propio rol. Solicítelo a otro administrador.");
        }

        if (!string.IsNullOrWhiteSpace(nuevaContrasenaPlana))
        {
            usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(nuevaContrasenaPlana);
        }
        else
        {
            usuario.ContrasenaHash = usuarioExistente.ContrasenaHash;
        }

        bool exito = await _repositorioUsuario.ActualizarAsync(usuario);
        if (exito)
        {
            await _servicioAuditoria.RegistrarAccionAsync(idEditorActual, "Usuarios", "Modificación", 
                $"Se actualizaron los datos del usuario: {usuario.NombreUsuario}");
        }
        return exito;
    }

    public async Task<bool> CambiarEstadoAsync(int id, bool estado)
    {
        // El admin maestro (Id = 1) nunca se puede desactivar
        if (id == 1)
        {
            throw new InvalidOperationException("No se puede modificar el estado del administrador maestro del sistema.");
        }

        if (!estado) // intentando desactivar
        {
            var usuarioDb = await _repositorioUsuario.ObtenerPorIdAsync(id);
            if (usuarioDb != null && usuarioDb.IdRol == 1)
            {
                // Verificar que no sea el último admin activo
                var usuarios = await _repositorioUsuario.ObtenerTodosAsync();
                int adminsActivos = usuarios.Count(u => u.IdRol == 1 && u.Activo && u.Id != id);
                if (adminsActivos == 0)
                {
                    throw new InvalidOperationException("No se puede desactivar al único administrador activo del sistema.");
                }
            }
        }

        return await _repositorioUsuario.CambiarEstadoAsync(id, estado);
    }

    public async Task<bool> CambiarEstadoAsync(int id, bool estado, int idUsuarioEditor)
    {
        // Reutilizar lógica de validación
        await CambiarEstadoAsync(id, estado);
        
        var user = await _repositorioUsuario.ObtenerPorIdAsync(id);
        string nombre = user?.NombreUsuario ?? $"ID {id}";
        
        bool exito = await _repositorioUsuario.CambiarEstadoAsync(id, estado);
        if (exito)
        {
            string accion = estado ? "Activación" : "Desactivación";
            await _servicioAuditoria.RegistrarAccionAsync(idUsuarioEditor, "Usuarios", accion, 
                $"Se {(estado ? "activó" : "desactivó")} la cuenta de: {nombre}");
        }
        return exito;
    }

    // ── Permisos por módulo ───────────────────────────────────────────────────

    public async Task<IEnumerable<string>> ObtenerPermisosAsync(int idUsuario)
    {
        return await _repositorioPermisos.ObtenerNombresPorUsuarioAsync(idUsuario);
    }

    public async Task OtorgarPermisoAsync(int idUsuario, string nombreModulo, int idEditorActual)
    {
        if (idUsuario == 1)
        {
            throw new InvalidOperationException("No se pueden modificar los permisos del administrador maestro del sistema.");
        }
        await _repositorioPermisos.OtorgarAsync(idUsuario, nombreModulo);
        
        var user = await _repositorioUsuario.ObtenerPorIdAsync(idUsuario);
        await _servicioAuditoria.RegistrarAccionAsync(idEditorActual, "Seguridad", "Permisos", 
            $"Se otorgó acceso al módulo {nombreModulo} al usuario {user?.NombreUsuario}");
    }

    public async Task RevocarPermisoAsync(int idUsuario, string nombreModulo, int idEditorActual)
    {
        if (idUsuario == 1)
        {
            throw new InvalidOperationException("No se pueden modificar los permisos del administrador maestro del sistema.");
        }
        await _repositorioPermisos.RevocarAsync(idUsuario, nombreModulo);
        
        var user = await _repositorioUsuario.ObtenerPorIdAsync(idUsuario);
        await _servicioAuditoria.RegistrarAccionAsync(idEditorActual, "Seguridad", "Permisos", 
            $"Se revocó acceso al módulo {nombreModulo} al usuario {user?.NombreUsuario}");
    }
}
