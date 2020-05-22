using Microsoft.EntityFrameworkCore;
using YamhillaNET.Data.Runtime;

namespace YamhillaNET.Data.Design
{
    /// <summary>
    /// DBContext to only be used when generating migrations
    /// </summary>
    public class DesignTimeSqliteContext: SqliteYamhilliaContext
    {
        public DesignTimeSqliteContext() : base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=yamhillia_design.db");
    }
}