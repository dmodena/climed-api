using AutoMapper;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using CliMed.Api.Utils;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CliMed.Api.Tests.Services
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<IMapper> mapperMock;
        private Mock<ICrypto> cryptoMock;
        private UserService sut;

        public UserServiceTests()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(m => m.GetAllItems()).Returns(new List<User>());
            userRepositoryMock.Setup(m => m.GetById(It.IsAny<long>())).Returns(new User());
            userRepositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(new User());
            userRepositoryMock.Setup(m => m.Create(It.IsAny<User>())).Returns(new User());

            mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(new UserDto());
            mapperMock.Setup(m => m.Map<IList<UserDto>>(It.IsAny<IList<User>>())).Returns(new List<UserDto>());

            cryptoMock = new Mock<ICrypto>();

            sut = new UserService(userRepositoryMock.Object, mapperMock.Object, cryptoMock.Object);
        }

        [Fact]
        public void GetAllItems_ShouldReturnUserDtoList()
        {
            var result = sut.GetAllItems();

            Assert.IsAssignableFrom<IList<UserDto>>(result);
        }

        [Fact]
        public void GetById_ShouldReturnUserDto()
        {
            var result = sut.GetById(1);

            Assert.IsType<UserDto>(result);
        }

        [Fact]
        public void GetByEmail_ShouldReturnUserDto()
        {
            var result = sut.GetByEmail("a@a.com");

            Assert.IsType<UserDto>(result);
        }

        [Fact]
        public void GetByRoleValue_ShouldReturnUserDtoList()
        {
            var result = sut.GetByRoleValue("admin");

            Assert.IsAssignableFrom<IList<UserDto>>(result);
        }

        [Fact]
        public void Create_ShouldReturnUserDto()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Create(user);

            Assert.IsType<UserDto>(result);
        }

        [Fact]
        public void Create_ShouldSaveHashedPassword()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Create(user);

            cryptoMock.Verify(x => x.HashPassword(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void UpdatePassword_ShouldReturnUserDto()
        {
            var user = UserBuilder.Simple().Build();
            user.Password = "newPassword";

            var result = sut.UpdatePassword(user);

            Assert.IsType<UserDto>(result);
        }

        [Fact]
        public void UpdatePassword_ShouldSaveHashedPassword()
        {
            var user = UserBuilder.Simple().Build();
            user.Password = "newPassword";

            var result = sut.UpdatePassword(user);

            cryptoMock.Verify(x => x.HashPassword(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Validate_ShouldReturnNullForInexistentUser()
        {
            var user = UserBuilder.Simple().Build();
            userRepositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(null as User);

            var result = sut.Validate(user);

            Assert.Null(result);
        }

        [Fact]
        public void Validate_ShouldReturnNullIfPasswordsDontMatch()
        {
            var user = UserBuilder.Simple().Build();
            cryptoMock.Setup(m => m.IsMatchPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var result = sut.Validate(user);

            Assert.Null(result);
        }

        [Fact]
        public void Validate_ShouldReturnUserDto()
        {
            var user = UserBuilder.Simple().Build();
            cryptoMock.Setup(m => m.IsMatchPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var result = sut.Validate(user);

            Assert.NotNull(result);
            Assert.IsType<UserDto>(result);
        }
    }
}
