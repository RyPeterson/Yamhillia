using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YamhilliaNET.Models;
using YamhilliaNET.Services.User;
using YamhilliaNET.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using IAuthenticationService = YamhilliaNET.Services.User.IAuthenticationService;

namespace YamhilliaNET.Controllers
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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claim = await _authenticationService.GenerateClaim(authenticateUser.Username, authenticateUser.Password);
            await HttpContext.SignInAsync(claim, new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.Now.AddDays(3),
            });
            return Ok();
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
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

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}