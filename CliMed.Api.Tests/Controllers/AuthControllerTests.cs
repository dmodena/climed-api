using CliMed.Api.Controllers;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace CliMed.Api.Tests.Controllers
{
    public class AuthControllerTests
    {
        private Mock<IAuthService> authServiceMock;
        private AuthController sut;

        public AuthControllerTests()
        {
            authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(m => m.Login(It.IsAny<User>())).Returns(new UserTokenDto());

            sut = new AuthController(authServiceMock.Object);
        }

        [Fact]
        public void Login_ShouldHaveAllowAnonymousAttribute()
        {
            MethodInfo sutMethod = sut.GetType().GetMethod("Login");
            Attribute[] attributes = Attribute.GetCustomAttributes(sutMethod);

            var allowAnonymousAttribute = attributes.FirstOrDefault(a => a is AllowAnonymousAttribute) as AllowAnonymousAttribute;

            Assert.NotNull(allowAnonymousAttribute);
        }

        [Fact]
        public void Login_ShouldReturn401Unauthorized()
        {
            UserTokenDto nonExistentUser = null;
            var user = UserBuilder.Simple().Build();
            authServiceMock.Setup(m => m.Login(It.IsAny<User>())).Returns(nonExistentUser);

            var result = sut.Login(user);

            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }

        [Fact]
        public void Login_ShouldReturn200OK()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Login(user);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Login_ShouldReturnUserTokenDto()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Login(user).Result as OkObjectResult;

            Assert.IsType<UserTokenDto>(result.Value);
        }
    }
}
