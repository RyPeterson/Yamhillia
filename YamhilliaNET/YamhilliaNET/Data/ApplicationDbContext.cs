using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using YamhilliaNET.Models;
using YamhilliaNET.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YamhilliaNET.Data
{
    public class ApplicationDbContext : IdentityDbContext<YamhilliaUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Farm>().HasIndex(f => f.Key).IsUnique();
            ConfigureCreatedAndUpdatedAt(modelBuilder);
            Seed(modelBuilder);
        }

        private void ConfigureCreatedAndUpdatedAt(ModelBuilder modelBuilder)
        {
            ConfigureCreatedAndUpdatedAt(modelBuilder.Entity<Farm>());
            ConfigureCreatedAndUpdatedAt(modelBuilder.Entity<Animal>());
        }

        private void ConfigureCreatedAndUpdatedAt<T>(EntityTypeBuilder<T> entityTypeBuilder) where T: AbstractYamhilliaModel
        {
            entityTypeBuilder
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("current_timestamp");
            entityTypeBuilder
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("current_timestamp");
            entityTypeBuilder.Property(p => p.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }

        protected virtual void Seed(ModelBuilder builder)
        {
            builder.Entity<Farm>().HasData(new Farm() {
                Id = 1,
                Name = DefaultFarm.DefaultFarmData.Name,
                Key = DefaultFarm.DefaultFarmData.Key,
                // Arbatrary, but needs to be constant so that the migrations don't keep repeating this
                CreatedAt = DateTime.Parse("2020-01-07 03:48:09.644639"),
                UpdatedAt =DateTime.Parse("2020-01-07 03:48:09.644639")
            });
        }

        public DbSet<Farm> Farms { set; get; }

        public DbSet<Animal> Animals { set; get; }
    }
}