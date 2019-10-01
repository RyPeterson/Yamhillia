using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace YamhilliaNET.Data.Providers
{
    public class PostgresProvider : IDatabaseProvider
    {
        public readonly string connectionString; 

        public PostgresProvider(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("No connection string specified");
            }

            this.connectionString = connectionString;

        }

        public DbProviderFactory Factory => Npgsql.NpgsqlFactory.Instance;

        public string ConnectionString => connectionString;

        public DbConnection Connect()
        {
            var connection = Factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            if(connection.State != System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("Database connection not obtained");
            }
            return (Npgsql.NpgsqlConnection)connection;
        }
    }
}
