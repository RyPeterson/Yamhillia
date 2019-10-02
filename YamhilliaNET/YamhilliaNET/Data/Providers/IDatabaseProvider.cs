using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace YamhilliaNET.Data.Providers
{
    public interface IDatabaseProvider
    {
        DbProviderFactory Factory { get; }

        string ConnectionString { get; }

        DbConnection Connect();

        Task<DbConnection> ConnectAsync();
    }
}
