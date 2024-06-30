using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.User;
using ApiAggregation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiAggregation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthTokenService _tokenService;
        private readonly LoginModelSettings _settings;

        public AuthController(AuthTokenService tokenService, IOptions<ApiSettings> settings)
        {
            _tokenService = tokenService;
            _settings = _settings = settings.Value.LoginModel;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromQuery] LoginModel login)
        {
            if (string.Equals(login.Username, _settings.Username) && string.Equals(login.Password, _settings.Password))
            {
                var token = _tokenService.GenerateToken(login.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
