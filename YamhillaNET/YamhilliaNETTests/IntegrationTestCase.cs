using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Xunit;
using YamhillaNET;
using YamhillaNET.Data;
using YamhillaNET.Models;
using YamhillaNET.Models.Entities;
using YamhillaNET.Services;
using YamhillaNET.Services.User;

namespace YamhilliaNETTests
{
    public class IntegrationTestCase: Startup
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
                    GetService<YamhilliaContext>().Database.EnsureDeleted();
                    GetService<YamhilliaContext>().Database.EnsureCreated();

                }
                configured = true;
                Interlocked.Exchange(ref configLock, 0);
            }
        }

        protected override void AddConfiguration(IServiceCollection services)
        {
            // Nope
        }

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            var builder = new SqliteConnectionStringBuilder()
            {
                DataSource = $"test_{Guid.NewGuid().ToString().Replace('-', '_')}.db"
            };
            services.AddDbContext<YamhilliaContext>(options =>
                options.UseSqlite(builder.ToString()));
        }

        protected override void ConfigureAuthentication(IServiceCollection services)
        {
            // lol
        }

        protected YamhilliaContext GetDbContext()
        {
            if (!configured)
            {
                throw new InvalidOperationException("ApplicationDbContext has not been configured");
            }

            return GetService<YamhilliaContext>();
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

        protected async Task<User> CreateTestUser()
        {
            return await GetService<IUserService>().CreateUser(new CreateUser()
            {
                Username = $@"test_user_{Guid.NewGuid().ToString()}@test.com",
                Password = UNIVERSAL_USER_PASSWORD,
            });
        }
            
    }
    
    
    
    public class TestConfiguration
    {

        public static IConfiguration Get()
        {
            return new TestConfig();
        }
    }

    
    public class TestConfig : IConfiguration
    {
        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public string this[string key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }

    public class Test: IntegrationTestCase
    {
        [Fact]
        public void TestSetup()
        {
            // A test... for test code...
            Assert.True(GetDbContext().Database.CanConnect());
        }
    }
}
