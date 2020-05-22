using Microsoft.EntityFrameworkCore;

namespace YamhillaNET.Data.Runtime
{
    public class SqliteYamhilliaContext : YamhilliaContext
    {
        protected SqliteYamhilliaContext() : base()
        {
            
        }
        
        public SqliteYamhilliaContext(DbContextOptions<YamhilliaContext> options) : base(options)
        {
            
        }
    }
}