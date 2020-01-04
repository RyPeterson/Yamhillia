using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using YamhilliaNET.Models;

namespace YamhilliaNET.Data
{
    public class ApplicationDbContext : IdentityDbContext<YamhilliaUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Farm> Farms { set; get; }

        public DbSet<Animal> Animals { set; get; }
    }
}