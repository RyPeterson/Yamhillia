using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Services;

namespace YamhilliaNET.Controllers
{
    [ApiController]
    [Route("api/server")]
    public class YamhilliaServerController : Controller
    {
        
        private readonly IServerService _serverService;

        public YamhilliaServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpGet("ping")]
        public IActionResult Index()
        {
            return Ok(_serverService.Ping());
        }
    }
}