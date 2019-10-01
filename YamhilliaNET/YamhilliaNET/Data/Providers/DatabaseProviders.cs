using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YamhilliaNET.Data.Providers
{
    public class DatabaseProviders
    {
        private readonly IDatabaseProvider provider;

        public DatabaseProviders(IConfiguration config)
        {
            if(config.GetValue<string>("DBMode") == "light")
            {
                provider = new SqliteProvider(config);
            }
            else
            {
                provider = new PostgresProvider(config);
            }
        }

        public IDatabaseProvider DatabaseProvider => provider;
    }
}
