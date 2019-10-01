using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YamhilliaNET.Data.Providers
{
    public class SqliteProvider : IDatabaseProvider
    {
        public readonly string connectionString;

        public SqliteProvider(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("No connection string specified");
            }

            this.connectionString = string.Format("Data Source=file:{0};", Path.Combine(Environment.CurrentDirectory, connectionString));

        }

        public DbProviderFactory Factory => SqliteFactory.Instance;

        public string ConnectionString => connectionString;


        public DbConnection Connect()
        {
            // TODO: why no work?
            var connection = Factory.CreateConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("Database connection not obtained");
            }
            return connection;
        }
    }
   }
