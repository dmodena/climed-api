using CliMed.Api.Models;
using CliMed.Api.Services;
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
        [ProducesResponseType(typeof(IList<User>), StatusCodes.Status200OK)]
        public ActionResult<IList<User>> Get()
        {
            return Ok(_userService.GetAllItems());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public ActionResult<User> GetById([FromRoute] long id)
        {
            return Ok(_userService.GetById(id));
        }


        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(user);

            _userService.Add(user);

            return CreatedAtAction(nameof(GetById), new { version = "1.0", id = user.Id }, user);
        }
    }
}
