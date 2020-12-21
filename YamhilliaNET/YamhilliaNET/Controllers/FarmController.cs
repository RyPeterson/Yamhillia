using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Models.Farms;
using YamhilliaNET.Services.Farms;

namespace YamhilliaNET.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/farm")]
    public class FarmController : YamhilliaController
    {
        private readonly IFarmService _farmService;
        
        public FarmController(IFarmService farmService)
        {
            _farmService = farmService;
        }

        [HttpGet("for-owner")]
        public async Task<IActionResult> GetFarmForOwner()
        {
            return Ok(await _farmService.GetFarmByOwner(GetLoggedInUserId()));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateFarmParams createFarmParams)
        {
            return Ok(await _farmService.CreateFarm(GetLoggedInUserId(), createFarmParams));
        }
    }
    
}