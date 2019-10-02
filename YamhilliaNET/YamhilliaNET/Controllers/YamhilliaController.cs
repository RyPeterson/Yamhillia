using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using YamhilliaNET.Data.Providers;

namespace YamhilliaNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YamhilliaController : ControllerBase
    {

        private readonly DatabaseProviders provider;

        public YamhilliaController(DatabaseProviders dbProvider)
        {
            provider = dbProvider;
        }

        [HttpGet]
        public async Task<int> Ping()
        {
            using(var connection = await provider.DatabaseProvider.ConnectAsync())
            {
 
                connection.Query("SELECT 1");
            }
            return 200;
        }
    }
}