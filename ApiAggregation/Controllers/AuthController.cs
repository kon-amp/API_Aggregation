using ApiAggregation.Models.AppSettings;
using ApiAggregation.Models.User;
using ApiAggregation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiAggregation.Controllers
{
    /// <summary>
    /// Controller for handling authentication.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthTokenService _tokenService;
        private readonly LoginModelSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="tokenService">Service for generating authentication tokens.</param>
        /// <param name="settings">Settings for the login model.</param>
        public AuthController(AuthTokenService tokenService, IOptions<ApiSettings> settings)
        {
            _tokenService = tokenService;
            _settings = _settings = settings.Value.LoginModel;
        }

        /// <summary>
        /// Authenticates the user and returns a JWT token if successful.
        /// </summary>
        /// <param name="login">The login credentials.</param>
        /// <returns>An <see cref="IActionResult"/> containing the JWT token if successful, or Unauthorized if not.</returns>
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
