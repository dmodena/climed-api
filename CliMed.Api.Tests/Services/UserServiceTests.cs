using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CliMed.Api.Tests.Services
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> userRepositoryMock;
        private UserService sut;

        public UserServiceTests()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.GetAllItems()).Returns(new List<User>());
            userRepositoryMock.Setup(m => m.GetById(It.IsAny<long>())).Returns(new User());
            userRepositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(new User());
            userRepositoryMock.Setup(m => m.Create(It.IsAny<User>())).Returns(new User());

            sut = new UserService(userRepositoryMock.Object);
        }

        [Fact]
        public void GetAllItems_ShouldReturnUserList()
        {
            var result = sut.GetAllItems();

            Assert.IsAssignableFrom<IList<User>>(result);
        }

        [Fact]
        public void GetById_ShouldReturnUser()
        {
            var result = sut.GetById(1);

            Assert.IsType<User>(result);
        }

        [Fact]
        public void GetByEmail_ShouldReturnUser()
        {
            var result = sut.GetByEmail("a@a.com");

            Assert.IsType<User>(result);
        }

        [Fact]
        public void Create_ShouldReturnUser()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Create(user);

            Assert.IsType<User>(result);
        }
    }
}
