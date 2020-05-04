using CliMed.Api.Controllers;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CliMed.Api.Tests.Controllers
{
    public class RolesControllerShould
    {
        [Fact]
        public void ReturnOkOnGetRoles()
        {
            var rolesServiceMock = new Mock<IRoleService>();
            var sut = new RolesController();

            var result = sut.Get(rolesServiceMock.Object);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
