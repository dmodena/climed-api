using CliMed.Api.Controllers;
using CliMed.Api.Models;
using CliMed.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CliMed.Api.Tests.Controllers
{
    public class UsersControllerTests
    {
        private Mock<IUserService> userServiceMock;
        private UsersController sut;

        public UsersControllerTests()
        {
            userServiceMock = new Mock<IUserService>();
            sut = new UsersController(userServiceMock.Object);
        }

        [Fact]
        public void GetShouldReturn200OK()
        {
            var result = sut.Get();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetByIdShouldReturn200OK()
        {
            userServiceMock.Setup(u => u.GetById(It.IsAny<long>())).Returns(new User());

            var result = sut.GetById(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void AddUserShouldReturn400BadRequestForNonUniqueEmail()
        {
            userServiceMock.Setup(u => u.IsEmailUnique(It.IsAny<User>())).Returns(false);

            var user = new User
            {
                Email = "a@a.com",
                Password = "a"
            };

            var result = sut.Create(user);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateShouldReturn201Created()
        {
            userServiceMock.Setup(u => u.IsEmailUnique(It.IsAny<User>())).Returns(true);

            var user = new User
            {
                Email = "a@a.com",
                Password = "a"
            };

            var result = sut.Create(user);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
    }
}
