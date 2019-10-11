using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YamhilliaNET;
using YamhilliaNET.Data;

namespace YamhilliaNETTests
{
    public class IntegrationTestCase : Startup
    {
        /// <summary>
        /// Password used for all users created via test cases unless explicitly changed
        /// </summary>
        public static readonly string UNIVERSAL_USER_PASSWORD = "Password1@";
        private ServiceCollection services;
        private ServiceProvider serviceProvider;

        static long COUNTER = 0;

        private bool configured;
        private int configLock; 

        public IntegrationTestCase() : base(TestConfiguration.Get())
        {
            Configure();
        }

        protected virtual void Configure()
        {
            if (Interlocked.Exchange(ref configLock, 1) == 0)
            {
                if (!configured)
                {
                    services = new ServiceCollection();
                    ConfigureServices(services);
                    serviceProvider = services.BuildServiceProvider();
                    //Nuke db between each test
                    GetService<ApplicationDbContext>().Database.EnsureDeleted();
                    GetService<ApplicationDbContext>().Database.EnsureCreated();

                }
                configured = true;
                Interlocked.Exchange(ref configLock, 0);
            }
        }


        protected override void ConfigureDatabase(IServiceCollection services)
        {
            var builder = new SqliteConnectionStringBuilder()
            {
                DataSource = $"test_{Guid.NewGuid().ToString().Replace('-', '_')}.db"
            };
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.ToString()));
        }

        protected override void SetupServices(IServiceCollection services)
        {
            base.SetupServices(services);
        }

        protected ApplicationDbContext GetApplicationDbContext()
        {
            if (!configured)
            {
                throw new InvalidOperationException("ApplicationDbContext has not been configured");
            }

            return GetService<ApplicationDbContext>();
        }

        public void Dispose()
        {
            if (configured)
            {
                configured = false;
            }
        }

        ~IntegrationTestCase()
        {
            Dispose();
        }

        protected T GetService<T>() where T : class
        {
            return serviceProvider.GetService<T>();
        }


        public class TestConfiguration
        {
            private static IConfiguration configuration;

            public static IConfiguration Get()
            {
                if (configuration == null)
                {
                    configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("test-appsettings.json")
                    .Build();
                }

                return configuration;
            }
        }
    }
}
