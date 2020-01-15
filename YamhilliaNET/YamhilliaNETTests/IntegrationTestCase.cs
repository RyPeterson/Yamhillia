using System.Xml;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YamhilliaNET;
using YamhilliaNET.Constants;
using YamhilliaNET.Data;
using YamhilliaNET.Models;
using YamhilliaNET.Services;
using YamhilliaNET.Services.User;

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
            services.AddLogging(config =>
            {
                config.AddDebug();
            });
        }

        protected override void ConfigureCORS(IServiceCollection services)
        {
            // lawl
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

        protected async Task<YamhilliaUser> CreateUser(Farm farm = null)
        {
            return await GetService<IUserService>().Create(new CreateUserModel()
            {
                Email = $"{Guid.NewGuid().ToString()}@test.com",
                FirstName = "Test",
                Password = UNIVERSAL_USER_PASSWORD,
                LastName = "User",
                FarmKey = farm != null ? farm.Key : null 
            });
        }

        protected async Task<Farm> GetDefaultFarm()
        {
            return await GetService<IFarmService>().GetFarmByKey(DefaultFarm.DefaultFarmKey);
        }

        protected async Task<Farm> CreateFarm()
        {
            return await GetService<IFarmService>().Create(new Farm()
            {
                Name = Guid.NewGuid().ToString(),
                Key = Guid.NewGuid().ToString()
            });
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
