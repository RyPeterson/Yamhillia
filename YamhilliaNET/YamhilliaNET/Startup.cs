using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YamhilliaNET.Constants;
using YamhilliaNET.Data;
using YamhilliaNET.Exceptions;
using YamhilliaNET.Services;
using YamhilliaNET.Services.Farms;
using YamhilliaNET.Services.Users;
using YamhilliaNET.Util;

namespace YamhilliaNET
{
    public class Startup
    {
        private const string YamhilliaCorsOptions = "_yamhilliaCorsOptions";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
                services
                    .AddControllers(options => options.Filters.Add(new YamhilliaStatusExceptionFilter()))
                    .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        }
                    );
                AddConfiguration(services);
                ConfigureDatabase(services);
                AddServices(services);
                ConfigureAuthentication(services);
                ConfigureCors(services);
                services.AddLogging(config => config.AddConsole());
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

                services.AddDbContext<YamhilliaContext>(options =>
                        options.UseNpgsql(Configuration.GetConnectionString("PGConnection")));
            }
            else
            {
                services.AddDbContext<YamhilliaContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
            }
        }

        protected virtual void AddServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IServerService, ServerService>();
            services.AddTransient<IFarmService, FarmService>();
        }
        
        protected virtual void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.Cookie.Name = "auth";
                options.Cookie.SameSite = SameSiteMode.Strict;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors(YamhilliaCorsOptions);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        protected virtual void ConfigureCors(IServiceCollection services)
        {            
            // Should be a semicolon separated list of strings
            var corsSettings = Configuration.GetSection("AllowedConsumers").Value;
            var consumers = corsSettings.Split(";");
            services.AddCors(options =>
            {
                options.AddPolicy(YamhilliaCorsOptions, builder =>
                {
                    builder.WithOrigins(consumers)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}