using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CliMed.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController([FromServices] IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IList<UserDto>), StatusCodes.Status200OK)]
        public ActionResult<IList<UserDto>> Get()
        {
            return Ok(_userService.GetAllItems());
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public ActionResult<UserDto> GetById([FromRoute] long id)
        {
            return Ok(_userService.GetById(id));
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> Create([FromBody] User user)
        {
            if (_userService.GetByEmail(user.Email) != null)
                return BadRequest($"The email {user.Email} already exists.");

            var userDto = _userService.Create(user);

            return CreatedAtAction(nameof(GetById), new { version = "1.0", id = userDto.Id }, userDto);
        }
    }
}
