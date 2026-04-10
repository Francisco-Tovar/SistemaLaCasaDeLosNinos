namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Rol de acceso dentro del sistema (Administrador, Funcionario, etc.).
/// </summary>
public class Rol
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}
