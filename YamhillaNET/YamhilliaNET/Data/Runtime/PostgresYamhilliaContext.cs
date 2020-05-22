using Microsoft.EntityFrameworkCore;

namespace YamhillaNET.Data.Runtime
{
    public class PostgresYamhilliaContext : YamhilliaContext
    {
        protected PostgresYamhilliaContext() : base()
        {
            
        }
        public PostgresYamhilliaContext(DbContextOptions<YamhilliaContext> options) : base(options)
        {
            
        }
    }
}