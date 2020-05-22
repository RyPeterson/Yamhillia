using Microsoft.EntityFrameworkCore;
using YamhillaNET.Data.Runtime;

namespace YamhillaNET.Data.Design
{
    /// <summary>
    /// DBContext to only be used when generating migrations
    /// </summary>
    public class DesignTimePostrgresContext: PostgresYamhilliaContext
    {
        public DesignTimePostrgresContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=localhost;Database=yamhillia_design;User ID=postgres;Password=kappa;timeout=1000;");
    }
}