namespace AILLM.Services
{
    using Dapper;
    using Microsoft.Data.SqlClient;

    public class DapperService
    {
        private readonly IConfiguration _configuration;

        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection CreateConnection()
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            return new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.QueryAsync<T>(sql, parameters);
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<T>(
                sql,
                parameters);
        }

        public async Task<int> ExecuteAsync(
            string sql,
            object? parameters = null)
        {
            using var connection = CreateConnection();

            return await connection.ExecuteAsync(sql, parameters);
        }
    }
}
