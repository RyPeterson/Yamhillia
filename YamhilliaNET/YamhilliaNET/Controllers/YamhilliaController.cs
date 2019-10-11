using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YamhilliaNET.Data;

namespace YamhilliaNET.Controllers
{
    [ApiController]
    [Route("/api/yamhillia")]
    public class YamhilliaController
    {
        private readonly ApplicationDbContext dbContext;
        public YamhilliaController(ApplicationDbContext db)
        {
            this.dbContext = db;
        }
        [HttpGet]
        public async Task<int> Ping()
        {
            await dbContext.Database.ExecuteSqlRawAsync("SELECT 1;");
            return 200;
        }
    }
}