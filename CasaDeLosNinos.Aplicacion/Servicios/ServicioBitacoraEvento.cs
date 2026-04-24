using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioBitacoraEvento : IServicioBitacoraEvento
{
    private readonly IRepositorioBitacoraEvento _repositorioEvento;
    private readonly IRepositorioFotoEvento _repositorioFoto;
    private readonly IServicioAuditoria _auditoria;

    public ServicioBitacoraEvento(
        IRepositorioBitacoraEvento repositorioEvento,
        IRepositorioFotoEvento repositorioFoto,
        IServicioAuditoria auditoria)
    {
        _repositorioEvento = repositorioEvento;
        _repositorioFoto = repositorioFoto;
        _auditoria = auditoria;
    }

    public async Task<int> RegistrarEventoAsync(BitacoraEvento evento, IEnumerable<byte[]> fotos)
    {
        int idEvento = await _repositorioEvento.AgregarAsync(evento);
        
        foreach (var imagen in fotos)
        {
            await _repositorioFoto.AgregarAsync(new FotoEvento
            {
                IdEvento = idEvento,
                Imagen = imagen
            });
        }

        await _auditoria.RegistrarAccionAsync(evento.IdUsuario, "BitacoraEventos", "Crear", $"Evento registrado: {evento.Titulo}");
        
        return idEvento;
    }

    public async Task ActualizarEventoAsync(BitacoraEvento evento, IEnumerable<byte[]> fotosNuevas, IEnumerable<int> idsFotosAEliminar)
    {
        await _repositorioEvento.ActualizarAsync(evento);

        foreach (var idFoto in idsFotosAEliminar)
        {
            await _repositorioFoto.EliminarAsync(idFoto);
        }

        foreach (var imagen in fotosNuevas)
        {
            await _repositorioFoto.AgregarAsync(new FotoEvento
            {
                IdEvento = evento.Id,
                Imagen = imagen
            });
        }

        await _auditoria.RegistrarAccionAsync(evento.IdUsuario, "BitacoraEventos", "Actualizar", $"Evento actualizado: {evento.Titulo} (ID: {evento.Id})");
    }

    public async Task EliminarEventoAsync(int id)
    {
        var evento = await _repositorioEvento.ObtenerPorIdAsync(id);
        if (evento != null)
        {
            await _repositorioFoto.EliminarPorEventoAsync(id);
            await _repositorioEvento.EliminarAsync(id);
            await _auditoria.RegistrarAccionAsync(null, "BitacoraEventos", "Eliminar", $"Evento eliminado: {evento.Titulo} (ID: {id})");
        }
    }

    public async Task<BitacoraEvento?> ObtenerEventoAsync(int id)
    {
        return await _repositorioEvento.ObtenerPorIdAsync(id);
    }

    public async Task<IEnumerable<BitacoraEvento>> ObtenerTodosAsync()
    {
        return await _repositorioEvento.ObtenerTodosAsync();
    }

    public async Task<IEnumerable<FotoEvento>> ObtenerFotosEventoAsync(int idEvento)
    {
        return await _repositorioFoto.ObtenerPorEventoAsync(idEvento);
    }
}
