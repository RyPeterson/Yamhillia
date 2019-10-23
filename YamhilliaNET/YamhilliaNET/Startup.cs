
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YamhilliaNET.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YamhilliaNET.Services.User;
using YamhilliaNET.Models;

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
            ConfigureCORS(services);
        }

        protected virtual void SetupServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(provider => Configuration);
            services.AddTransient<Services.Auth.IAuthenticationService, Services.Auth.AuthenticationService>();
            services.AddTransient<IUserService, UserService>();

        }

        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            var issuer = Configuration["JWT:Issuer"];
            if(string.IsNullOrEmpty(issuer))
            {
                throw new ArgumentException("No JWT:Issuer set");
            }

            var key = Configuration["JWT:Key"];
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("No JWT:Key set");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = issuer,
                  ValidAudience = issuer, 
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)) 
                };
            });

            services.AddIdentity<YamhilliaUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
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

        protected virtual void ConfigureCORS(IServiceCollection services)
        {
            var corsConfig = Configuration["AllowedCORS"];
            if(string.IsNullOrEmpty(corsConfig))
            {
                throw new ArgumentException("No CORS orgins specified");
            }
            var orgins = corsConfig.Split(',');
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins(orgins).AllowAnyMethod().AllowAnyHeader();
            }));
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

            app.UseCors("ApiCorsPolicy");

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
