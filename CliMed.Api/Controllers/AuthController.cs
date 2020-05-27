using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CliMed.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController([FromServices] IAuthService authService, [FromServices] IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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

        [HttpPost]
        [Authorize]
        [Route("resetpassword")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<UserDto> ResetPassword([FromBody] User user)
        {
            if (User.FindFirst(ClaimTypes.Email).Value == user.Email || User.IsInRole("admin"))
            {
                var userDto = _userService.UpdatePassword(user);
                return Ok(userDto);
            }

            return Forbid();
        }
    }
}
