using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioRegistroHoras : IServicioRegistroHoras
{
    private readonly IRepositorioRegistroHoras _repositorio;
    private readonly IServicioAuditoria _servicioAuditoria;

    public ServicioRegistroHoras(IRepositorioRegistroHoras repositorio, IServicioAuditoria servicioAuditoria)
    {
        _repositorio = repositorio;
        _servicioAuditoria = servicioAuditoria;
    }

    public async Task<IEnumerable<RegistroHoras>> ObtenerPorVoluntarioAsync(int idVoluntario)
    {
        if (idVoluntario <= 0) throw new ArgumentException("El ID del voluntario debe ser mayor a cero.", nameof(idVoluntario));
        return await _repositorio.ObtenerPorVoluntarioAsync(idVoluntario);
    }

    public async Task<int> RegistrarHorasAsync(RegistroHoras registro)
    {
        if (registro == null) throw new ArgumentNullException(nameof(registro));
        if (registro.IdVoluntario <= 0) throw new ArgumentException("Debe asociarse a un voluntario válido.");
        if (registro.HorasAportadas <= 0) throw new ArgumentException("Las horas aportadas deben ser mayores a cero.");
        if (registro.IdUsuario <= 0) throw new ArgumentException("Debe indicar qué usuario realiza el registro.");
        
        // Se permite registrar horas históricas, no validamos que la fecha sea igual o mayor a hoy.
        int nuevoId = await _repositorio.CrearAsync(registro);
        
        await _servicioAuditoria.RegistrarAccionAsync(registro.IdUsuario, "Voluntarios", "Registro Horas", 
            $"Se registraron {registro.HorasAportadas} horas para el voluntario ID: {registro.IdVoluntario}");
            
        return nuevoId;
    }

    public async Task EliminarAsync(int id, int idUsuarioSesion)
    {
        if (id <= 0) throw new ArgumentException("El ID debe ser mayor a cero.", nameof(id));
        await _repositorio.EliminarAsync(id);
        
        await _servicioAuditoria.RegistrarAccionAsync(idUsuarioSesion, "Voluntarios", "Baja Horas", 
            $"Se eliminó un registro de horas aportadas (ID: {id})");
    }

    public async Task<decimal> ObtenerTotalHorasVoluntarioAsync(int idVoluntario)
    {
        if (idVoluntario <= 0) throw new ArgumentException("El ID del voluntario debe ser mayor a cero.", nameof(idVoluntario));
        return await _repositorio.ObtenerTotalHorasVoluntarioAsync(idVoluntario);
    }
}
