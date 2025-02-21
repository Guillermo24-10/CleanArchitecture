using CleanArchitecture.Application.Abstractions.Data;
using System.Data;

namespace CleanArchitecture.Infrastructure.Data
{
    internal sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new Npgsql.NpgsqlConnection(_connectionString);//llamada para postgre
            connection.Open();

            return connection;
        }
    }
}
