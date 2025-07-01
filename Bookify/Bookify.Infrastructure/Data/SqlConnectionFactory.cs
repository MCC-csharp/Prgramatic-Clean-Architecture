using Bookify.Application.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Data
{
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
}
