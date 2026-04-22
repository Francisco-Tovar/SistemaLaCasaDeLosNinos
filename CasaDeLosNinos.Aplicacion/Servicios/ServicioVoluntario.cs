using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioVoluntario : IServicioVoluntario
{
    private readonly IRepositorioVoluntario _repositorio;
    private readonly IServicioAuditoria _servicioAuditoria;

    public ServicioVoluntario(IRepositorioVoluntario repositorio, IServicioAuditoria servicioAuditoria)
    {
        _repositorio = repositorio;
        _servicioAuditoria = servicioAuditoria;
    }

    public async Task<IEnumerable<Voluntario>> ObtenerTodosAsync(bool incluirInactivos = false)
    {
        return await _repositorio.ObtenerTodosAsync(incluirInactivos);
    }

    public async Task<Voluntario?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("El ID debe ser mayor a cero.", nameof(id));
        return await _repositorio.ObtenerPorIdAsync(id);
    }

    public async Task<int> CrearAsync(Voluntario voluntario, int idUsuario)
    {
        ValidarVoluntario(voluntario);
        int nuevoId = await _repositorio.CrearAsync(voluntario);
        await _servicioAuditoria.RegistrarAccionAsync(idUsuario, "Voluntarios", "Creación", 
            $"Se registró al voluntario: {voluntario.NombreCompleto}");
        return nuevoId;
    }

    public async Task<bool> ActualizarAsync(Voluntario voluntario, int idUsuario)
    {
        ValidarVoluntario(voluntario);
        bool exito = await _repositorio.ActualizarAsync(voluntario);
        if (exito)
        {
            await _servicioAuditoria.RegistrarAccionAsync(idUsuario, "Voluntarios", "Modificación", 
                $"Se actualizaron los datos de: {voluntario.NombreCompleto}");
        }
        return exito;
    }

    public async Task CambiarEstadoAsync(int id, bool activo, int idUsuario)
    {
        if (id <= 0) throw new ArgumentException("El ID debe ser mayor a cero.", nameof(id));
        
        var voluntario = await _repositorio.ObtenerPorIdAsync(id);
        string nombre = voluntario?.NombreCompleto ?? $"ID {id}";
        
        await _repositorio.CambiarEstadoAsync(id, activo);
        
        string accion = activo ? "Activación" : "Desactivación";
        await _servicioAuditoria.RegistrarAccionAsync(idUsuario, "Voluntarios", accion, 
            $"Se cambió el estado de {nombre} a {(activo ? "Activo" : "Inactivo")}");
    }

    private void ValidarVoluntario(Voluntario voluntario)
    {
        if (voluntario == null) throw new ArgumentNullException(nameof(voluntario));
        if (string.IsNullOrWhiteSpace(voluntario.NombreCompleto))
            throw new ArgumentException("El nombre del voluntario es obligatorio.");
        
        // Se requiere al menos un medio de contacto (Correo o Teléfono)
        if (string.IsNullOrWhiteSpace(voluntario.Correo) && string.IsNullOrWhiteSpace(voluntario.Telefono))
            throw new ArgumentException("Debe proporcionar al menos un medio de contacto (Correo o Teléfono).");
    }
}
