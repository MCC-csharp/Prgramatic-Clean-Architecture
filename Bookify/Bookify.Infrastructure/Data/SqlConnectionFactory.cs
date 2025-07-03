using System.Data;
using Bookify.Application.Abstractions.Data;

namespace Bookify.Infrastructure.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        var connection = new Npgsql.NpgsqlConnection(_connectionString);
        connection.Open();


        return connection;
    }
}
