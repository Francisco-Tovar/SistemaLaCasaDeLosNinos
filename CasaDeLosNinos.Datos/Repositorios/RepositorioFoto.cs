using CasaDeLosNinos.Dominio.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CasaDeLosNinos.Datos.Repositorios
{
    public class RepositorioFoto : IRepositorioFoto
    {
        private readonly string _cadenaConexion;

        public RepositorioFoto(IConfiguration configuracion)
        {
            _cadenaConexion = configuracion.GetConnectionString("BaseDatosFotos")
                ?? throw new InvalidOperationException("Falta 'BaseDatosFotos' en appsettings.json.");
        }

        public async Task<byte[]?> ObtenerFotoAsync(int idNino)
        {
            await using var conexion = new SqliteConnection(_cadenaConexion);
            const string sql = "SELECT Imagen FROM FotosBeneficiarios WHERE IdNino = @IdNino;";
            return await conexion.QueryFirstOrDefaultAsync<byte[]>(sql, new { IdNino = idNino });
        }

        public async Task GuardarFotoAsync(int idNino, byte[] imagen)
        {
            await using var conexion = new SqliteConnection(_cadenaConexion);
            const string sql = @"
                INSERT INTO FotosBeneficiarios (IdNino, Imagen, FechaActualizacion)
                VALUES (@IdNino, @Imagen, CURRENT_TIMESTAMP)
                ON CONFLICT(IdNino) DO UPDATE SET
                    Imagen = @Imagen,
                    FechaActualizacion = CURRENT_TIMESTAMP;";
            
            await conexion.ExecuteAsync(sql, new { IdNino = idNino, Imagen = imagen });
        }

        public async Task EliminarFotoAsync(int idNino)
        {
            await using var conexion = new SqliteConnection(_cadenaConexion);
            const string sql = "DELETE FROM FotosBeneficiarios WHERE IdNino = @IdNino;";
            await conexion.ExecuteAsync(sql, new { IdNino = idNino });
        }

        public async Task<bool> ExisteFotoAsync(int idNino)
        {
            await using var conexion = new SqliteConnection(_cadenaConexion);
            const string sql = "SELECT COUNT(1) FROM FotosBeneficiarios WHERE IdNino = @IdNino;";
            var count = await conexion.ExecuteScalarAsync<int>(sql, new { IdNino = idNino });
            return count > 0;
        }
    }
}
