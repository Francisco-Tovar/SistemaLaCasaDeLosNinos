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
}
