using AutoMapper;
using CliMed.Api.Auth;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using Moq;
using Xunit;

namespace CliMed.Api.Tests.Services
{
    public class AuthServiceTests
    {
        private Mock<IUserRepository> userRepositoryMock;
        private Mock<ITokens> tokensMock;
        private Mock<IMapper> mapperMock;
        private AuthService sut;

        public AuthServiceTests()
        {
            userRepositoryMock = new Mock<IUserRepository>();

            tokensMock = new Mock<ITokens>();

            mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns<User>(u => new UserDto {
                Id = u.Id,
                Email = u.Email,
                Username = u.Username,
                Role = u.Role,
            });

            sut = new AuthService(userRepositoryMock.Object, tokensMock.Object, mapperMock.Object);
        }

        [Fact]
        public void Login_ShouldReturnUserTokenDto()
        {
            var user = UserBuilder.Simple().Build();
            userRepositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>()))
                .Returns(user);

            var result = sut.Login(user);

            Assert.IsType<UserTokenDto>(result);
        }

        [Fact]
        public void Login_ShouldReturnUserTokenDtoWithRole()
        {
            var role = new Role { Id = 1, Value = "admin" };
            var user = UserBuilder.Simple().WithRole(role).Build();
            userRepositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>()))
                .Returns(user);

            var result = sut.Login(user);

            Assert.NotNull(result.User.Role);
        }

        [Fact]
        public void Login_ShouldReturnNullForUnexistentUser()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.Login(user);

            Assert.Null(result);
        }

        [Fact]
        public void Login_ShouldReturnNullForIncorrectPassword()
        {
            var userDb = UserBuilder.Simple().WithPassword("a").Build();
            var user = UserBuilder.Simple().WithPassword("b").Build();
            userRepositoryMock.Setup(m => m.GetByEmail(It.IsAny<string>()))
                .Returns(userDb);

            var result = sut.Login(user);

            Assert.Null(result);
        }
    }
}
