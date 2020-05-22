using Microsoft.EntityFrameworkCore;
using YamhillaNET.Data;
using YamhillaNET.Data.Runtime;

namespace YamhilliaNETTests
{
    public class TestDbContext: SqliteYamhilliaContext
    {
        public TestDbContext() : base()
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=yamhillia-test.db");
    }
}