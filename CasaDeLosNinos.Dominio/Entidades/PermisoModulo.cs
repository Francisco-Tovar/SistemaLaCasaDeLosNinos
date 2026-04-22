namespace CasaDeLosNinos.Dominio.Entidades;

/// <summary>
/// Representa un permiso individual que un usuario tiene sobre un módulo del sistema.
/// Los módulos gestionados son: Ninos, Asistencia, Voluntarios, CajaChica, Reportes.
/// Gestión de Usuarios y Mantenimiento son exclusivos del rol Administrador y no se almacenan aquí.
/// </summary>
public class PermisoModulo
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }       // FK a Usuarios
    public string NombreModulo { get; set; } = string.Empty;
}
