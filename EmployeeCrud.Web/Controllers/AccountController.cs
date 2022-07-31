using EmployeeCrud.Web.Models;
using EmployeeCrud.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeCrud.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IJwtAuthManager _jwtAuthManager;

        private readonly UserManager<IdentityUser> _userManager;


        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, IJwtAuthManager jwtAuthManager)
        {
            _logger = logger;
            _userManager = userManager;
            _jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name,  request.UserName),
                };

                var jwtResult = _jwtAuthManager.GenerateTokens(request.UserName, claims, DateTime.Now);

                _logger.LogInformation($"User [{request.UserName}] logged in the system.");

                return Ok(new LoginResult
                {
                    UserName = request.UserName,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            return new BadRequestObjectResult($"User with name {request.UserName} couldn't be found");
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult> GetCurrentUser()
        {
            var loginRes = await Task.Run(() => new LoginResult
            {
                UserName = User.Identity?.Name ?? string.Empty,
                OriginalUserName = User?.FindFirst("OriginalUserName")?.Value ?? string.Empty
            });
            return Ok(loginRes);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var userName = await Task.Run(() => User.Identity?.Name);
            if (userName == null) return BadRequest("usernmae is null");
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            _logger.LogInformation($"User [{userName}] logged out the system.");
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return Ok();
        }

    }
}
