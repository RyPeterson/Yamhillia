using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Data;

namespace YamhilliaNETTests
{
    public class TestDbContext: YamhilliaContext
    {
        public TestDbContext() : base()
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=yamhillia-test.db");
    }
}