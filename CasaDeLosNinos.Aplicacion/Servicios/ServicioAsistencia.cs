using CasaDeLosNinos.Dominio.Dtos;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

/// <summary>
/// Caso de uso: Toma de asistencia diaria masiva.
/// Orquesta repositorioNino y repositorioAsistencia.
/// La doble barrera anti-duplicados: validación aquí + índice UNIQUE en BD.
/// </summary>
public class ServicioAsistencia : IServicioAsistencia
{
    private readonly IRepositorioNino       _repositorioNino;
    private readonly IRepositorioAsistencia _repositorioAsistencia;

    public ServicioAsistencia(
        IRepositorioNino       repositorioNino,
        IRepositorioAsistencia repositorioAsistencia)
    {
        _repositorioNino       = repositorioNino;
        _repositorioAsistencia = repositorioAsistencia;
    }

    /// <summary>
    /// Carga los niños activos. Si ya existe asistencia para la fecha,
    /// hidrata el campo Presente con el valor guardado (para permitir correcciones).
    /// </summary>
    public async Task<IEnumerable<NinoAsistenciaDto>> ObtenerNinosParaAsistenciaAsync(DateTime fecha)
    {
        var ninos      = await _repositorioNino.ObtenerActivosAsync();
        var registros  = await _repositorioAsistencia.ObtenerPorFechaAsync(fecha);

        // Índice rápido por IdNino para la hidratación O(1)
        var mapaRegistros = registros.ToDictionary(r => r.IdNino, r => r.Presente);

        return ninos.Select(n => new NinoAsistenciaDto
        {
            IdNino        = n.Id,
            NombreCompleto = n.NombreCompleto,
            Presente      = mapaRegistros.TryGetValue(n.Id, out var presente) && presente
        });
    }

    /// <summary>
    /// Guarda la asistencia masiva del día en una transacción.
    /// Permite guardar aunque ya existan registros previos (corrección).
    /// </summary>
    public async Task<(bool Exito, string Mensaje)> GuardarAsistenciaAsync(
        DateTime fecha,
        IEnumerable<NinoAsistenciaDto> lista,
        int idUsuarioActual)
    {
        if (idUsuarioActual <= 0)
            return (false, "No hay un usuario activo identificado para registrar la asistencia.");

        var listaDto = lista.ToList();
        if (listaDto.Count == 0)
            return (false, "No hay niños activos para registrar asistencia.");

        // Convertir DTOs a entidades de dominio
        var registros = listaDto.Select(dto => new Asistencia
        {
            IdNino      = dto.IdNino,
            Fecha       = fecha.Date,
            Presente    = dto.Presente,
            Observacion = string.Empty,
            IdUsuario   = idUsuarioActual
        });

        await _repositorioAsistencia.GuardarAsistenciaMasivaAsync(registros);

        int presentes = listaDto.Count(d => d.Presente);
        return (true, $"Asistencia guardada: {presentes} presente(s) de {listaDto.Count} niño(s).");
    }
}
