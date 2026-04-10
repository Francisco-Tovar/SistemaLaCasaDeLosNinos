namespace CasaDeLosNinos.Dominio.Interfaces;

/// <summary>
/// Contrato para el inicializador de la base de datos.
/// Responsable de crear el esquema y aplicar migraciones de versión.
/// Implementado en la capa Datos — nunca en Dominio.
/// </summary>
public interface IInicializadorBaseDatos
{
    /// <summary>
    /// Crea todas las tablas si no existen y registra la versión inicial del esquema.
    /// </summary>
    Task InicializarAsync();
}
