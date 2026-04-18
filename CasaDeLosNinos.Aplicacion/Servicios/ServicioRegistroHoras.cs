using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioRegistroHoras : IServicioRegistroHoras
{
    private readonly IRepositorioRegistroHoras _repositorio;

    public ServicioRegistroHoras(IRepositorioRegistroHoras repositorio)
    {
        _repositorio = repositorio;
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
        return await _repositorio.CrearAsync(registro);
    }

    public async Task EliminarAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("El ID debe ser mayor a cero.", nameof(id));
        await _repositorio.EliminarAsync(id);
    }

    public async Task<decimal> ObtenerTotalHorasVoluntarioAsync(int idVoluntario)
    {
        if (idVoluntario <= 0) throw new ArgumentException("El ID del voluntario debe ser mayor a cero.", nameof(idVoluntario));
        return await _repositorio.ObtenerTotalHorasVoluntarioAsync(idVoluntario);
    }
}
