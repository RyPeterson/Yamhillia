using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YamhilliaNET.Constants;
using YamhilliaNET.Models.Entities;

namespace YamhilliaNET.Data
{
    public class YamhilliaContext : DbContext
    {
        protected YamhilliaContext() : base()
        {
            // Only for migration design time 
        }
        
        public YamhilliaContext(DbContextOptions<YamhilliaContext> options) : base(options)
        {

        }

        public DbSet<User> Users { set; get; }
        
        public DbSet<Farm> Farms { set; get; }
        
        public DbSet<FarmMembership> FarmMemberships { set; get; }

        /*
         * Support for triggers/autogenerated values are shakey at best for the DB providers, let alone
         * supporting both. While this isn't as nice as having the DB do the work itself and is probably
         * not accurate, its good enough.
         */
        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var addedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .ToList();

            addedEntities.ForEach(E =>
            {
                var now = DateTime.UtcNow;
                E.Property("CreatedAt").CurrentValue = now;
                E.Property("UpdatedAt").CurrentValue = now;
                E.Property("EntityUUID").CurrentValue = Guid.NewGuid().ToString();
            });

            var editedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified)
                .ToList();

            
            editedEntities.ForEach(E =>
            {
                E.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(p => new { p.Username })
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(p => p.EntityUUID).IsUnique();
            modelBuilder.Entity<Farm>()
                .HasIndex(p => p.EntityUUID).IsUnique();
            modelBuilder.Entity<FarmMembership>()
                .HasIndex(p => p.EntityUUID);
            modelBuilder.Entity<FarmMembership>()
                .HasIndex(m => new {m.UserId, m.MemberType});
            modelBuilder.Entity<FarmMembership>()
                .Property(p => p.MemberType)
                .HasConversion(new EnumToStringConverter<MemberType>());
        }

    }
}