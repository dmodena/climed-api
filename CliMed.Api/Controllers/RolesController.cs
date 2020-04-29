using CliMed.Api.Models;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CliMed.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Role>), 200)]
        public IActionResult GetRoles([FromServices] IRoleService roleService)
        {
            return Ok(roleService.GetAllItems());
        }
    }
}