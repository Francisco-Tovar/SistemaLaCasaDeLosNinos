namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Funcionario o administrador que opera el sistema.
/// La contraseña siempre se almacena hasheada con BCrypt — nunca en texto plano.
/// </summary>
public class Usuario
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public string ContrasenaHash { get; set; } = string.Empty;
    public int IdRol { get; set; }          // FK plana — no objeto anidado
    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
}
