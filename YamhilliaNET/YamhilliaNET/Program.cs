using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
                switch (args[0])
                {
                    case "Migrate":
                        new MigrationRunner(args).UpdateDatabase();
                        return;
                    case "Rollback":
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