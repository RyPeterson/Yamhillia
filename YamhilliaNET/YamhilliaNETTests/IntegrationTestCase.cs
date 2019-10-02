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
using YamhilliaNET.Data.Providers;

namespace YamhilliaNETTests
{
    public class IntegrationTestCase : Startup
    {
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

                }
                configured = true;
                Interlocked.Exchange(ref configLock, 0);
            }
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

        /*
        protected async Task<ApplicationUser> CreateUser()
        {
            Interlocked.Increment(ref COUNTER);
            var email = "test_" + Guid.NewGuid().ToString() + "@test.com";

        }
        */

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

        private class TestSqliteProvider : SqliteProvider
        {
            public TestSqliteProvider() : base("yamhillia-test.db")
            {
                
            }
        }

        private class TestPostgresProvider : PostgresProvider
        {
            public TestPostgresProvider() : base("Host=localhost;Database=yamhillia-tests;User ID=postgres;Password=kappa;timeout=1000;")
            {
                
            }
        }

        public class TestDatabaseProviders : DatabaseProviders
        {
            public TestDatabaseProviders() : base(null)
            {
            }

            public override IDatabaseProvider DatabaseProvider => new TestPostgresProvider();

        }
    }
}
