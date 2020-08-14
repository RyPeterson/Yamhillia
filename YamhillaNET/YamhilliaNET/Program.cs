using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YamhilliaNET.Constants;
using YamhilliaNET.ViewModels;

namespace YamhilliaNET
{
    public class Program
    {

        public static void Main(string[] args)
        {
            // The jank...
            // Its power is over 9000!
            if (args.Length > 0)
            {
                if (args[0] == "Migrate")
                {
                    new MigrationRunner(args).UpdateDatabase();
                    return;
                }

                if (args[0] == "Rollback")
                {
                    new MigrationRunner(args).Rollback();
                    return;
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        
    }
}