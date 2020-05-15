using CliMed.Api.Controllers;
using CliMed.Api.Dto;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            userServiceMock.Setup(m => m.GetAllItems()).Returns(new List<UserDto>());
            userServiceMock.Setup(m => m.GetById(It.IsAny<long>())).Returns(new UserDto());
            userServiceMock.Setup(m => m.Create(It.IsAny<Models.User>())).Returns(new UserDto());

            sut = new UsersController(userServiceMock.Object);
        }

        [Fact]
        public void Get_ShouldHaveAuthorizeAttribute()
        {
            MethodInfo sutMethod = sut.GetType().GetMethod("Get");
            Attribute[] attributes = Attribute.GetCustomAttributes(sutMethod);

            var authorizationAttribute = attributes.FirstOrDefault(a => a is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.NotNull(authorizationAttribute);
            Assert.Null(authorizationAttribute.Roles);
        }

        [Fact]
        public void Get_ShouldReturn200OK()
        {
            var result = sut.Get();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void Get_ShouldReturnUserDtoList()
        {
            var result = sut.Get().Result as OkObjectResult;

            Assert.IsAssignableFrom<IList<UserDto>>(result.Value);
        }

        [Fact]
        public void GetById_ShouldHaveAuthorizeAttribute()
        {
            MethodInfo sutMethod = sut.GetType().GetMethod("GetById");
            Attribute[] attributes = Attribute.GetCustomAttributes(sutMethod);

            var authorizationAttribute = attributes.FirstOrDefault(a => a is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.NotNull(authorizationAttribute);
            Assert.Null(authorizationAttribute.Roles);
        }

        [Fact]
        public void GetById_ShouldReturn200OK()
        {
            var result = sut.GetById(1);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetById_ShouldReturnUserDto()
        {
            var result = sut.GetById(1).Result as OkObjectResult;

            Assert.IsType<UserDto>(result.Value);
        }

        [Fact]
        public void Create_ShouldHaveAuthorizeAttribute()
        {
            MethodInfo sutMethod = sut.GetType().GetMethod("Create");
            Attribute[] attributes = Attribute.GetCustomAttributes(sutMethod);

            var authorizationAttribute = attributes.FirstOrDefault(a => a is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.NotNull(authorizationAttribute);
        }

        [Fact]
        public void Create_ShouldAuthorizeAdmin()
        {
            MethodInfo sutMethod = sut.GetType().GetMethod("Create");
            Attribute[] attributes = Attribute.GetCustomAttributes(sutMethod);

            var authorizationAttribute = attributes.FirstOrDefault(a => a is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.NotNull(authorizationAttribute);
            Assert.Contains("admin", authorizationAttribute.Roles);
        }

        [Fact]
        public void Create_ShouldReturn400BadRequestForNonUniqueEmail()
        {
            var user = UserBuilder.Simple().Build();
            userServiceMock.Setup(m => m.GetByEmail(user.Email))
                .Returns(new UserDto { Email = user.Email });

            var result = sut.Create(user);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void Create_ShouldReturn201Created()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Create(user);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void Create_ShouldReturnUserDto()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Create(user).Result as CreatedAtActionResult;

            Assert.IsType<UserDto>(result.Value);
        }
    }
}
