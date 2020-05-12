using CliMed.Api.Controllers;
using CliMed.Api.Models;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
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
            roleServiceMock.Setup(m => m.GetAllItems()).Returns(new List<Role>());

            sut = new RolesController(roleServiceMock.Object);
        }

        [Fact]
        public void GetShouldReturn200OK()
        {
            var result = sut.Get();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetShouldReturnRoleList()
        {
            var result = sut.Get().Result as OkObjectResult;

            Assert.IsAssignableFrom<IList<Role>>(result.Value);
        }
    }
}
