using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CliMed.Api.Tests.Services
{
    public class RoleServiceTests
    {
        private Mock<IRoleRepository> roleRepositoryMock;
        private RoleService sut;

        public RoleServiceTests()
        {
            roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(m => m.GetlAllItems()).Returns(new List<Role>());
            roleRepositoryMock.Setup(m => m.GetByValue(It.IsAny<string>())).Returns(new Role());

            sut = new RoleService(roleRepositoryMock.Object);
        }

        [Fact]
        public void GetAllItems_ShouldReturnRoleList()
        {
            var result = sut.GetAllItems();

            Assert.IsAssignableFrom<IList<Role>>(result);
        }

        [Fact]
        public void GetByValue_ShouldReturnRole()
        {
            var result = sut.GetByValue("admin");

            Assert.IsType<Role>(result);
        }
    }
}
