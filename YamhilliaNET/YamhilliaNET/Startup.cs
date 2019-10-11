using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YamhilliaNET.Data;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Models;
using Microsoft.AspNetCore.Identity;

namespace YamhilliaNET
{
    public class Startup
    {
        private static readonly string MODE_SQLITE = "sqlite";
        private static readonly string MODE_POSTGRE = "pg";

        private static readonly string CONNECTION_KEY_SQLITE = "SqliteConnection";
        private static readonly string CONNECTION_KEY_PG = "PGConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SetupServices(services);
            ConfigureDatabase(services);
            ConfigureAuth(services);
        }

        protected virtual void SetupServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers();
        }

        protected virtual void ConfigureAuth(IServiceCollection services)
        {

        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            
            var mode = Configuration["DBMode"];
            if(string.IsNullOrEmpty(mode))
            {
                mode = MODE_POSTGRE;
            } 
            else if(mode != MODE_POSTGRE && mode != MODE_SQLITE)
            {
                throw new ArgumentException("Unkown database mode");
            }
            var connectionKey = mode == MODE_POSTGRE ? CONNECTION_KEY_PG : CONNECTION_KEY_SQLITE;
            var connectionStr = Configuration["ConnectionStrings:" + connectionKey];

            if(string.IsNullOrEmpty(connectionStr))
            {
                throw new ArgumentException("No connection string specified");
            }

            if(mode == MODE_SQLITE)
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionStr));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionStr));
            }
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
