using CliMed.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CliMed.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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