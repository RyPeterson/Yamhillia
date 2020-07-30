using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamhillaNET.Models;
using YamhillaNET.Services.User;
using YamhillaNET.ViewModels;

namespace YamhillaNET.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPut("register")]
        public async Task<IActionResult> Register([FromBody] CreateUser user)
        {
            await _userService.CreateUser(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser authenticateUser)
        {
            var token = await _authenticationService.GenerateToken(authenticateUser.Username, authenticateUser.Password);
            return Ok(new
            {
                Token = token,
                Username = authenticateUser.Username
            });
        }

        [HttpGet("user")]
        public async Task<IActionResult> User()
        {
            var user = await _userService.GetUserById(long.Parse(HttpContext.User.Identity.Name ?? "-404"));
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(new
            {
                user = new UserViewModel(user)
            });
        }
    }
}