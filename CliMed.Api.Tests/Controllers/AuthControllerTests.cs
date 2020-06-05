using CliMed.Api.Controllers;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;

namespace CliMed.Api.Tests.Controllers
{
    public class AuthControllerTests
    {
        private Mock<IAuthService> authServiceMock;
        private Mock<IUserService> userServiceMock;
        private AuthController sut;

        public AuthControllerTests()
        {
            authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(m => m.Login(It.IsAny<UserLoginDto>())).Returns(new UserTokenDto());

            userServiceMock = new Mock<IUserService>();


            sut = new AuthController(authServiceMock.Object, userServiceMock.Object);
        }

        [Fact]
        public void SignUp_ShouldHaveAllowAnonymousAttribute()
        {
            MethodInfo sutMethod = sut.GetType().GetMethod("SignUp");
            Attribute[] attributes = Attribute.GetCustomAttributes(sutMethod);

            var allowAnonymousAttribute = attributes.FirstOrDefault(a => a is AllowAnonymousAttribute) as AllowAnonymousAttribute;

            Assert.NotNull(allowAnonymousAttribute);
        }

        [Fact]
        public void SignUp_ShouldReturn403Forbidden()
        {
            var user = UserBuilder.Simple().Build();
            authServiceMock.Setup(m => m.SignUp(It.IsAny<User>())).Returns(null as UserTokenDto);

            var result = sut.SignUp(user);

            Assert.IsType<ForbidResult>(result.Result);
        }

        [Fact]
        public void SignUp_ShouldReturn200OK()
        {
            var user = UserBuilder.Simple().Build();
            authServiceMock.Setup(m => m.SignUp(It.IsAny<User>())).Returns(new UserTokenDto());

            var result = sut.SignUp(user);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void SignUp_ShouldReturnUserTokenDto()
        {
            var user = UserBuilder.Simple().Build();
            authServiceMock.Setup(m => m.SignUp(It.IsAny<User>())).Returns(new UserTokenDto());

            var result = sut.SignUp(user).Result as OkObjectResult;

            Assert.NotNull(result.Value);
            Assert.IsType<UserTokenDto>(result.Value);
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
            var userLoginDto = UserLoginDtoBuilder.Simple().Build();
            authServiceMock.Setup(m => m.Login(It.IsAny<UserLoginDto>())).Returns(nonExistentUser);

            var result = sut.Login(userLoginDto);

            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }

        [Fact]
        public void Login_ShouldReturn200OK()
        {
            var userLoginDto = UserLoginDtoBuilder.Simple().Build();

            var result = sut.Login(userLoginDto);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Login_ShouldReturnUserTokenDto()
        {
            var userLoginDto = UserLoginDtoBuilder.Simple().Build();

            var result = sut.Login(userLoginDto).Result as OkObjectResult;

            Assert.IsType<UserTokenDto>(result.Value);
        }

        [Fact]
        public void ResetPassword_ShouldHaveAuthorizeAttribute()
        {
            MethodInfo sutMethod = sut.GetType().GetMethod("ResetPassword");
            Attribute[] attributes = Attribute.GetCustomAttributes(sutMethod);

            var authorizeAttribute = attributes.FirstOrDefault(a => a is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.NotNull(authorizeAttribute);
        }

        [Fact]
        public void ResetPassword_ShouldAllowAdmin()
        {
            var adminRole = RoleBuilder.Admin().Build();
            var user = UserBuilder.Typical().WithRole(adminRole).Build();
            var controllerContext = GetControllerContextFake(user);
            sut.ControllerContext = controllerContext;

            var result = sut.ResetPassword(user);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void ResetPassword_ShouldAllowSameUser()
        {
            var user = UserBuilder.Simple().Build();
            var controllerContext = GetControllerContextFake(user);
            sut.ControllerContext = controllerContext;

            var result = sut.ResetPassword(user);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void ResetPassword_ShouldDenyOtherUser()
        {
            var usera = UserBuilder.Simple().WithEmail("a@a.com").Build();
            var userb = UserBuilder.Simple().WithEmail("b@b.com").Build();
            var controllerContext = GetControllerContextFake(usera);
            sut.ControllerContext = controllerContext;

            var result = sut.ResetPassword(userb);

            Assert.IsType<ForbidResult>(result.Result);
        }

        [Fact]
        public void ResetPassword_ShouldReturnUserDto()
        {
            var user = UserBuilder.Simple().Build();
            userServiceMock.Setup(m => m.UpdatePassword(It.IsAny<User>())).Returns(new UserDto());
            var controllerContext = GetControllerContextFake(user);
            sut.ControllerContext = controllerContext;

            var result = sut.ResetPassword(user).Result as OkObjectResult;

            Assert.IsType<UserDto>(result.Value);
        }

        private ControllerContext GetControllerContextFake(User user)
        {
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(m => m.User.FindFirst(ClaimTypes.Email)).Returns(new Claim(ClaimTypes.Email, user.Email));
            httpContextMock.Setup(m => m.User.IsInRole("admin")).Returns(user.Role?.Value == "admin");

            var controllerContext = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()));

            return controllerContext;
        }
    }
}
