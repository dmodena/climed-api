using CliMed.Api.Controllers;
using CliMed.Api.Models;
using CliMed.Api.Services;
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
        public void Get_ShouldReturnRoleList()
        {
            var result = sut.Get().Result as OkObjectResult;

            Assert.IsAssignableFrom<IList<Role>>(result.Value);
        }
    }
}
