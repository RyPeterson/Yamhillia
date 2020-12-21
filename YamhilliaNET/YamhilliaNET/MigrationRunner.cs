using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YamhilliaNET.Constants;
using YamhilliaNET.Util;

namespace YamhilliaNET
{
    internal static class MigrationRunnerExtension
    {
        public static IMigrationRunnerBuilder ConfigureRunner(this IMigrationRunnerBuilder runner, DatabaseMode mode)
        {
            return mode ==  DatabaseMode.POSTGRES ? runner.AddPostgres() : runner.AddSQLite();
        }
    }
    public class MigrationRunner
    {
        private readonly IConfigurationRoot _config;

        public MigrationRunner(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                configurationBuilder.AddJsonFile("appsettings.Development.json");
            }

            _config = configurationBuilder.AddCommandLine(args).Build();
        }
        private IServiceProvider CreateServiceProvider()
        {
            var appSettingsSection = _config.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var databaseMode = DatabaseMode.FromString(appSettings.DatabaseMode);
            Console.WriteLine($@"Starting up database connection with {databaseMode.Value}");
            var connectionString = "";
            if (databaseMode == DatabaseMode.POSTGRES)
            {
                connectionString = _config.GetConnectionString("PGConnection");
            }
            else
            {
                connectionString = _config.GetConnectionString("SqliteConnection");
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }
            
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .ConfigureRunner(databaseMode)
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()
                    .ConfigureGlobalProcessorOptions(p =>
                    {
                        p.ConnectionString = connectionString;
                        p.StripComments = true;
                    })
                )
                .AddLogging(l => l.AddFluentMigratorConsole())
                
                .BuildServiceProvider();
        }

        public void UpdateDatabase()
        {
            using var scope = CreateServiceProvider().CreateScope();
            scope.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
        }

        public void Rollback()
        {
            {
                using (var scope = CreateServiceProvider().CreateScope())
                {
                    var rollbackVersion = _config.GetValue<string>("version");
                    var parseAttempt = long.TryParse(rollbackVersion, out var version);
                    if (!parseAttempt)
                    {
                        scope.ServiceProvider.GetRequiredService<IMigrationRunner>().ListMigrations();
                    }
                    else
                    {
                        scope.ServiceProvider.GetRequiredService<IMigrationRunner>().MigrateDown(version);
                    }
                }
            }
        }
    }
}