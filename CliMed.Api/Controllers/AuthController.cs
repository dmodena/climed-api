using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CliMed.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController([FromServices] IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [ProducesResponseType(typeof(UserTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UserTokenDto> Login([FromBody] User user)
        {
            var userTokenDto = _authService.Login(user);

            if (userTokenDto == null)
                return Unauthorized("Invalid user or password.");

            return Ok(userTokenDto);
        }
    }
}
