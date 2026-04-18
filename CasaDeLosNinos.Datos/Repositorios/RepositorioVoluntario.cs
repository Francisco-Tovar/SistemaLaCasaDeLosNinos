using CasaDeLosNinos.Dominio.Entidades;
using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios;

public class RepositorioVoluntario : IRepositorioVoluntario
{
    private readonly string _cadenaConexion;

    public RepositorioVoluntario(IConfiguration configuracion)
    {
        _cadenaConexion = configuracion.GetConnectionString("BaseDatos")
            ?? throw new InvalidOperationException("Falta 'BaseDatos' en appsettings.json.");
    }

    public async Task<IEnumerable<Voluntario>> ObtenerTodosAsync(bool incluirInactivos = false)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        string sql = "SELECT * FROM Voluntarios";
        if (!incluirInactivos) sql += " WHERE Activo = 1";
        
        return await conexion.QueryAsync<Voluntario>(sql);
    }

    public async Task<Voluntario?> ObtenerPorIdAsync(int id)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "SELECT * FROM Voluntarios WHERE Id = @Id;";
        return await conexion.QuerySingleOrDefaultAsync<Voluntario>(sql, new { Id = id });
    }

    public async Task<int> CrearAsync(Voluntario voluntario)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            INSERT INTO Voluntarios (NombreCompleto, Correo, Telefono, Especialidad, Activo, FechaIngreso)
            VALUES (@NombreCompleto, @Correo, @Telefono, @Especialidad, @Activo, @FechaIngreso);
            SELECT last_insert_rowid();";
            
        return await conexion.ExecuteScalarAsync<int>(sql, voluntario);
    }

    public async Task ActualizarAsync(Voluntario voluntario)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = @"
            UPDATE Voluntarios SET 
                NombreCompleto = @NombreCompleto,
                Correo = @Correo,
                Telefono = @Telefono,
                Especialidad = @Especialidad
            WHERE Id = @Id;";
            
        await conexion.ExecuteAsync(sql, voluntario);
    }

    public async Task CambiarEstadoAsync(int id, bool activo)
    {
        await using var conexion = new SqliteConnection(_cadenaConexion);
        const string sql = "UPDATE Voluntarios SET Activo = @Activo WHERE Id = @Id;";
        await conexion.ExecuteAsync(sql, new { Id = id, Activo = activo ? 1 : 0 });
    }
}
