namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Registro de versiones del esquema de base de datos.
/// Permite control de migraciones futuras sin herramientas externas.
/// </summary>
public class VersionBD
{
    public int Id { get; set; }
    public string NumeroVersion { get; set; } = string.Empty;
    public DateTime FechaAplicacion { get; set; } = DateTime.Now;
    public string Descripcion { get; set; } = string.Empty;
}
