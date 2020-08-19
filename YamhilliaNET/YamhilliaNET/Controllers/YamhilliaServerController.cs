using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Services;

namespace YamhilliaNET.Controllers
{
    [ApiController]
    [Route("api/server")]
    public class YamhilliaServerController : Controller
    {
        
        private IServerService _serverService;

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