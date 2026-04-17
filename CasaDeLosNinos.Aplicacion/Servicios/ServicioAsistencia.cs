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
    private readonly IServicioObservacion   _servicioObservacion;

    public ServicioAsistencia(
        IRepositorioNino       repositorioNino,
        IRepositorioAsistencia repositorioAsistencia,
        IServicioObservacion   servicioObservacion)
    {
        _repositorioNino       = repositorioNino;
        _repositorioAsistencia = repositorioAsistencia;
        _servicioObservacion   = servicioObservacion;
    }

    public async Task<IEnumerable<NinoAsistenciaDto>> ObtenerNinosParaAsistenciaAsync(DateTime fecha)
    {
        // 1. Obtener todos los niños (activos e inactivos)
        var todos      = await _repositorioNino.ObtenerTodosAsync();
        // 2. Obtener los registros de asistencia de la fecha específica (vía JOIN con Observaciones)
        var registros  = (await _repositorioAsistencia.ObtenerPorFechaAsync(fecha)).ToList();

        // Índice rápido por IdNino para la hidratación O(1)
        var mapaRegistros = registros.ToDictionary(r => r.IdNino, r => r);
        var idsConRegistro = mapaRegistros.Keys.ToHashSet();

        // 3. Filtrar: Niños activos HOY + Niños que tienen registro en ESA FECHA
        return todos
            .Where(n => n.Activo || idsConRegistro.Contains(n.Id))
            .Select(n => new NinoAsistenciaDto
            {
                IdNino        = n.Id,
                NombreCompleto = n.NombreCompleto,
                Presente      = mapaRegistros.TryGetValue(n.Id, out var reg) && reg.Presente,
                // Leemos el texto unificado desde el JOIN
                Observacion   = mapaRegistros.TryGetValue(n.Id, out var r) ? r.ObservacionTexto : string.Empty
            });
    }

    /// <summary>
    /// Guarda la asistencia y sincroniza la Bitácora (Fuente Única de Verdad).
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

        // Recuperar registros existentes para tener tanto el IdObservacion como el Texto actual para comparar
        var existentes = (await _repositorioAsistencia.ObtenerPorFechaAsync(fecha))
                        .ToDictionary(e => e.IdNino, e => e);

        var registrosParaGuardar = new List<Asistencia>();

        foreach (var dto in listaDto)
        {
            var registroExistente = existentes.TryGetValue(dto.IdNino, out var ex) ? ex : null;
            int? idObs = registroExistente?.IdObservacion;
            string textoNuevo = dto.Observacion?.Trim() ?? string.Empty;
            string textoAnterior = registroExistente?.ObservacionTexto?.Trim() ?? string.Empty;

            // 1. Calculamos la fecha y hora final: Fecha del form + Hora de este momento
            var fechaReferencia = fecha.Date.Add(DateTime.Now.TimeOfDay);

            if (!string.IsNullOrWhiteSpace(textoNuevo))
            {
                // Si el texto cambió respecto a lo que ya estaba guardado
                if (textoNuevo != textoAnterior)
                {
                    if (idObs.HasValue)
                    {
                        // ES UNA EDICIÓN: 
                        var textoLimpio = System.Text.RegularExpressions.Regex.Replace(textoNuevo, @" \(Modificado: \d{2}/\d{2} \d{2}:\d{2}\)$", "");
                        var textoConEdicion = $"{textoLimpio} (Modificado: {DateTime.Now:dd/MM HH:mm})";
                        
                        await _servicioObservacion.ActualizarAsync(idObs.Value, textoConEdicion);
                    }
                    else
                    {
                        // ES NUEVA O MIGRADA: Creamos el vínculo con el pasado
                        var textoConSello = $"[{DateTime.Now:dd/MM HH:mm}] {textoNuevo}";
                        idObs = await _servicioObservacion.RegistrarAsync(dto.IdNino, idUsuarioActual, textoConSello, fechaReferencia);
                    }
                }
            }
            else if (idObs.HasValue)
            {
                // Si el texto se vació completamente, eliminamos la observación
                await _servicioObservacion.EliminarAsync(idObs.Value);
                idObs = null;
            }

            registrosParaGuardar.Add(new Asistencia
            {
                IdNino        = dto.IdNino,
                Fecha         = fecha.Date,
                Presente      = dto.Presente,
                IdObservacion = idObs,
                IdUsuario     = idUsuarioActual
            });
        }

        await _repositorioAsistencia.GuardarAsistenciaMasivaAsync(registrosParaGuardar);

        int presentes = listaDto.Count(d => d.Presente);
        return (true, $"Asistencia guardada: {presentes} presente(s) de {listaDto.Count} niño(s). Registros de bitácora sincronizados.");
    }
}
