using CliMed.Api.Controllers;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CliMed.Api.Tests.Controllers
{
    public class RolesControllerTests
    {
        private Mock<IRoleService> roleServiceMock;
        private RolesController sut;

        public RolesControllerTests()
        {
            roleServiceMock = new Mock<IRoleService>();
            sut = new RolesController(roleServiceMock.Object);
        }

        [Fact]
        public void GetShouldReturn200OK()
        {
            var result = sut.Get();

            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
