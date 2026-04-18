using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioUsuario : IRepositorioUsuario
{
    private readonly string _cadenaConexion;

    public RepositorioUsuario(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos") 
            ?? throw new InvalidOperationException("Cadena de conexión no encontrada.");
    }

    public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM Usuarios WHERE NombreUsuario = @nombreUsuario AND Activo = 1";
        return await conexion.QueryFirstOrDefaultAsync<Usuario>(sql, new { nombreUsuario });
    }

    public async Task<bool> ExisteAdminAsync()
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT COUNT(1) FROM Usuarios";
        var count = await conexion.ExecuteScalarAsync<int>(sql);
        return count > 0;
    }

    public async Task<int> InsertarAsync(Usuario usuario)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO Usuarios (NombreCompleto, NombreUsuario, ContrasenaHash, IdRol, Activo)
            VALUES (@NombreCompleto, @NombreUsuario, @ContrasenaHash, @IdRol, @Activo);
            SELECT last_insert_rowid();";
        
        return await conexion.ExecuteScalarAsync<int>(sql, usuario);
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM Usuarios";
        return await conexion.QueryAsync<Usuario>(sql);
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM Usuarios WHERE Id = @id";
        return await conexion.QueryFirstOrDefaultAsync<Usuario>(sql, new { id });
    }

    public async Task<bool> NombreUsuarioExisteAsync(string nombreUsuario, int? idExcluido = null)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        string sql = "SELECT COUNT(1) FROM Usuarios WHERE NombreUsuario = @nombreUsuario";
        var pameters = new DynamicParameters();
        pameters.Add("@nombreUsuario", nombreUsuario);

        if (idExcluido.HasValue)
        {
            sql += " AND Id != @idExcluido";
            pameters.Add("@idExcluido", idExcluido.Value);
        }

        var count = await conexion.ExecuteScalarAsync<int>(sql, pameters);
        return count > 0;
    }

    public async Task<bool> ActualizarAsync(Usuario usuario)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            UPDATE Usuarios 
            SET NombreCompleto = @NombreCompleto,
                NombreUsuario = @NombreUsuario,
                ContrasenaHash = @ContrasenaHash,
                IdRol = @IdRol
            WHERE Id = @Id";
        
        var afectados = await conexion.ExecuteAsync(sql, usuario);
        return afectados > 0;
    }

    public async Task<bool> CambiarEstadoAsync(int id, bool estado)
    {
        using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "UPDATE Usuarios SET Activo = @estado WHERE Id = @id";
        var afectados = await conexion.ExecuteAsync(sql, new { id, estado = estado ? 1 : 0 });
        return afectados > 0;
    }
}
