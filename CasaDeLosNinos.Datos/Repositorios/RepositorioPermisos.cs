using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

/// <summary>
/// Repositorio de permisos por módulo. Usa INSERT OR IGNORE para evitar duplicados.
/// Los 5 módulos configurables son: Ninos, Asistencia, Voluntarios, CajaChica, Reportes.
/// </summary>
public class RepositorioPermisos : IRepositorioPermisos
{
    private readonly string _cadenaConexion;

    public RepositorioPermisos(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException("Cadena de conexión 'BaseDatos' no encontrada.");
    }

    public async Task<IEnumerable<string>> ObtenerNombresPorUsuarioAsync(int idUsuario)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT NombreModulo FROM PermisosModulo WHERE IdUsuario = @idUsuario";
        return await conexion.QueryAsync<string>(sql, new { idUsuario });
    }

    public async Task OtorgarAsync(int idUsuario, string nombreModulo)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT OR IGNORE INTO PermisosModulo (IdUsuario, NombreModulo)
            VALUES (@idUsuario, @nombreModulo)";
        await conexion.ExecuteAsync(sql, new { idUsuario, nombreModulo });
    }

    public async Task RevocarAsync(int idUsuario, string nombreModulo)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            DELETE FROM PermisosModulo 
            WHERE IdUsuario = @idUsuario AND NombreModulo = @nombreModulo";
        await conexion.ExecuteAsync(sql, new { idUsuario, nombreModulo });
    }

    public async Task InsertarPermisosDefaultAsync(int idUsuario)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT OR IGNORE INTO PermisosModulo (IdUsuario, NombreModulo)
            VALUES (@idUsuario, 'Ninos'),
                   (@idUsuario, 'Asistencia')";
        await conexion.ExecuteAsync(sql, new { idUsuario });
    }

    public async Task OtorgarTodoAsync(int idUsuario)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT OR IGNORE INTO PermisosModulo (IdUsuario, NombreModulo)
            VALUES (@idUsuario, 'Ninos'),
                   (@idUsuario, 'Asistencia'),
                   (@idUsuario, 'Voluntarios'),
                   (@idUsuario, 'CajaChica'),
                   (@idUsuario, 'Reportes')";
        await conexion.ExecuteAsync(sql, new { idUsuario });
    }
}
