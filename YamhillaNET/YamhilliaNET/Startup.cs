using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YamhillaNET.Constants;
using YamhillaNET.Data;
using YamhillaNET.Services;
using YamhillaNET.Services.User;
using YamhillaNET.Util;

namespace YamhillaNET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            AddConfiguration(services);
            ConfigureDatabase(services);
            AddServices(services);
        }

        protected virtual void AddConfiguration(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var databaseMode = DatabaseMode.FromString(appSettings.DatabaseMode);
            Console.WriteLine($@"Starting up database connection with {databaseMode.Value}");
            if (databaseMode == DatabaseMode.POSTGRES)
            {
                services.AddDbContext<PostgresYamhilliaContext>(options => 
                    options.UseNpgsql(Configuration.GetConnectionString("PGConnection")));
            }
            else
            {
                services.AddDbContext<SqliteYamhilliaContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
            }
        }

        protected virtual void AddServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}