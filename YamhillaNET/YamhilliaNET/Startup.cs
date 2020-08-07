using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using YamhillaNET.Data.Runtime;
using YamhillaNET.Exceptions;

namespace YamhillaNET
{
    public class Startup
    {
        private readonly string YamhilliaCorsOptions = "_yamhilliaCorsOptions";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
                services.AddControllers(options => options.Filters.Add(new YamhilliaStatusExceptionFilter()));
                AddConfiguration(services);
                ConfigureDatabase(services);
                AddServices(services);
                ConfigureAuthentication(services);
                ConfigureCORS(services);
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
                // The jank is real. I want to have a generic YamhilliaContext during runtime,
                // but have a subtype during migrations/db updates
                services.AddDbContext<YamhilliaContext>(options =>
                        options.UseNpgsql(Configuration.GetConnectionString("PGConnection")));
                services.AddDbContext<PostgresYamhilliaContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PGConnection")));
            }
            else
            {
                services.AddDbContext<YamhilliaContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
                services.AddDbContext<SqliteYamhilliaContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));
            }
        }

        protected virtual void AddServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IServerService, ServerService>();
        }
        
        protected virtual void ConfigureAuthentication(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = async context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId= long.Parse(context.Principal.Identity.Name ?? "-404");
                        var user = await userService.GetUserById(userId);
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                    }
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
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

        protected virtual void ConfigureCORS(IServiceCollection services)
        {            
            // Should be a semicolon separated list of strings
            var corsSettings = Configuration.GetSection("AllowedConsumers").Value;
            var consumers = corsSettings.Split(";");
            services.AddCors(options =>
            {
                options.AddPolicy(name: YamhilliaCorsOptions, builder =>
                {
                    builder.WithOrigins(consumers)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}