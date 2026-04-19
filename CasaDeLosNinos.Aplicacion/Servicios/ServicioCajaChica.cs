using System.Text;
using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;

namespace CasaDeLosNinos.Aplicacion.Servicios;

public class ServicioCajaChica : IServicioCajaChica
{
    private readonly IRepositorioCajaChica _repositorio;

    public ServicioCajaChica(IRepositorioCajaChica repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<CajaChica>> ObtenerPorMesAsync(int anio, int mes)
    {
        return await _repositorio.ObtenerPorMesAsync(anio, mes);
    }

    public async Task<decimal> ObtenerSaldoMensualAsync(int anio, int mes)
    {
        return await _repositorio.ObtenerSaldoMensualAsync(anio, mes);
    }

    public async Task<CajaChica?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));
        return await _repositorio.ObtenerPorIdAsync(id);
    }

    public async Task<int> RegistrarMovimientoAsync(CajaChica movimiento)
    {
        ValidarMovimiento(movimiento);
        return await _repositorio.CrearAsync(movimiento);
    }

    public async Task<(bool Exito, string Mensaje)> ModificarMovimientoAsync(CajaChica movimientoEditado, int idUsuarioQueEdita)
    {
        ValidarMovimiento(movimientoEditado);

        // 1. Obtener el estado actual (antes de editar) para comparar
        var movimientoOriginal = await _repositorio.ObtenerPorIdAsync(movimientoEditado.Id);
        if (movimientoOriginal == null)
            return (false, "El registro financiero original no existe o fue eliminado.");

        // 2. Generar texto de diferencias
        var diferencias = GenerarTextoDiferencias(movimientoOriginal, movimientoEditado);
        
        // Si no hay cambios reales, abortar silenciamente como éxito.
        if (string.IsNullOrWhiteSpace(diferencias))
            return (true, "Sin cambios.");

        // 3. Ejecutar actualización 
        var exitoUpdate = await _repositorio.ActualizarAsync(movimientoEditado);
        if (!exitoUpdate)
            return (false, "Fallo crítico al actualizar base de datos financiera.");

        // 4. Inserción del LOG obligatorio y no editable
        var auditoria = new AuditoriaCajaChica
        {
            IdMovimiento = movimientoEditado.Id,
            IdUsuario = idUsuarioQueEdita,
            DetallesDelCambio = diferencias
        };

        await _repositorio.InsertarAuditoriaAsync(auditoria);

        return (true, "Movimiento modificado y auditado exitosamente.");
    }

    public async Task<IEnumerable<AuditoriaCajaChica>> ObtenerHistorialAuditoriaAsync(int idMovimiento)
    {
        return await _repositorio.ObtenerAuditoriasPorMovimientoAsync(idMovimiento);
    }

    public async Task<IEnumerable<AuditoriaDetalleDTO>> ObtenerAuditoriaMensualAsync(int anio, int mes)
    {
        return await _repositorio.ObtenerAuditoriasDetalladasPorMesAsync(anio, mes);
    }

    // --- Métodos Privados ---

    private void ValidarMovimiento(CajaChica m)
    {
        if (m == null) throw new ArgumentNullException(nameof(m));
        
        if (m.Monto <= 0)
            throw new ArgumentException("El monto financiero debe ser mayor a cero.");

        if (string.IsNullOrWhiteSpace(m.Concepto))
            throw new ArgumentException("Debe ingresar un concepto, justificación o número de comprobante.");

        if (m.TipoMovimiento != "Ingreso" && m.TipoMovimiento != "Egreso")
            throw new ArgumentException("Operación no autorizada. Debe ser Ingreso o Egreso.");
    }

    private string GenerarTextoDiferencias(CajaChica original, CajaChica editado)
    {
        var sb = new StringBuilder();

        // Validamos cada campo clave que afecte balances o referencias
        if (original.Monto != editado.Monto)
            sb.AppendLine($"Monto modificado: ₡{original.Monto:N2} -> ₡{editado.Monto:N2}");
        
        if (original.TipoMovimiento != editado.TipoMovimiento)
            sb.AppendLine($"Tipo modificado: {original.TipoMovimiento} -> {editado.TipoMovimiento}");
        
        if (original.Fecha.Date != editado.Fecha.Date)
            sb.AppendLine($"Fecha desplazada: {original.Fecha:dd/MM/yyyy} -> {editado.Fecha:dd/MM/yyyy}");
        
        if (original.Concepto != editado.Concepto)
            sb.AppendLine($"Concepto modificado: '{original.Concepto}' -> '{editado.Concepto}'");

        if (original.IdFotoRecibo != editado.IdFotoRecibo)
            sb.AppendLine($"Comprobante fotográfico actualizado (ID: {(original.IdFotoRecibo?.ToString() ?? "Ninguno")} -> {(editado.IdFotoRecibo?.ToString() ?? "Ninguno")})");

        return sb.ToString().TrimEnd();
    }
}
