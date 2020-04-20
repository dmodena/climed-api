using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CliMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRoles([FromServices] IRoleService roleService)
        {
            return Ok(roleService.GetAllItems());
        }
    }
}