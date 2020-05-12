using CliMed.Api.Controllers;
using CliMed.Api.Models;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
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
            userServiceMock.Setup(m => m.GetAllItems()).Returns(new List<User>());
            userServiceMock.Setup(m => m.GetById(It.IsAny<long>())).Returns(new User());
            userServiceMock.Setup(m => m.Create(It.IsAny<User>())).Returns(new User());

            sut = new UsersController(userServiceMock.Object);
        }

        [Fact]
        public void GetShouldReturn200OK()
        {
            var result = sut.Get();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetShouldReturnUserList()
        {
            var result = sut.Get().Result as OkObjectResult;

            Assert.IsAssignableFrom<IList<User>>(result.Value);
        }

        [Fact]
        public void GetByIdShouldReturn200OK()
        {
            var result = sut.GetById(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetByIdShouldReturnUser()
        {
            var result = sut.GetById(1).Result as OkObjectResult;

            Assert.IsType<User>(result.Value);
        }

        [Fact]
        public void AddUserShouldReturn400BadRequestForNonUniqueEmail()
        {
            var user = UserBuilder.Simple().Build();
            sut.ModelState.AddModelError("Email", "The email already exists.");

            var result = sut.Create(user);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void CreateShouldReturn201Created()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Create(user);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void CreateShouldReturnUser()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Create(user).Result as CreatedAtActionResult;

            Assert.IsType<User>(result.Value);
        }
    }
}
