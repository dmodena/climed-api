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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController([FromServices] IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Role>), StatusCodes.Status200OK)]
        public ActionResult<IList<Role>> Get()
        {
            return Ok(_roleService.GetAllItems());
        }
    }
}