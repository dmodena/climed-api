using CliMed.Api.Auth;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Services;
using CliMed.Api.Tests.Builders;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CliMed.Api.Tests.Services
{
    public class AuthServiceTests
    {
        private Mock<IRoleRepository> roleRepositoryMock;
        private Mock<IUserService> userServiceMock;
        private Mock<ITokens> tokensMock;
        private AuthService sut;

        public AuthServiceTests()
        {
            roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(m => m.GetByValue("admin")).Returns(new Role { Id = 1, Value = "admin" });

            userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.GetByRoleValue("admin")).Returns(new List<UserDto>());
            userServiceMock.Setup(m => m.Create(It.IsAny<User>())).Returns(new UserDto());
            userServiceMock.Setup(m => m.Validate(It.IsAny<UserLoginDto>())).Returns(new UserDto());

            tokensMock = new Mock<ITokens>();
            tokensMock.Setup(m => m.GenerateToken(It.IsAny<UserDto>())).Returns(It.IsAny<string>());

            sut = new AuthService(roleRepositoryMock.Object, userServiceMock.Object, tokensMock.Object);
        }

        [Fact]
        public void SignUp_ShouldReturnUserTokenDto()
        {
            var user = UserBuilder.Simple().Build();

            var result = sut.SignUp(user);

            Assert.NotNull(result);
            Assert.IsType<UserTokenDto>(result);
        }

        [Fact]
        public void SignUp_ShouldReturnUserTokenDtoWithAdminRole()
        {
            var user = UserBuilder.Simple().Build();
            userServiceMock.Setup(m => m.Create(It.IsAny<User>())).Returns((User u) => new UserDto { Email = u.Email, Role = u.Role });

            var result = sut.SignUp(user);

            Assert.NotNull(result);
            Assert.IsType<UserTokenDto>(result);
            Assert.NotNull(result.User.Role);
            Assert.Equal("admin", result.User.Role.Value);
        }

        [Fact]
        public void SignUp_ShouldReturnNullIfAdminUserAlreadyExists()
        {
            var user = UserBuilder.Simple().Build();
            userServiceMock.Setup(m => m.GetByRoleValue("admin")).Returns(new List<UserDto> { new UserDto() });

            var result = sut.SignUp(user);

            Assert.Null(result);
        }

        [Fact]
        public void Login_ShouldReturnUserTokenDto()
        {
            var userLoginDto = UserLoginDtoBuilder.Simple().Build();

            var result = sut.Login(userLoginDto);

            Assert.IsType<UserTokenDto>(result);
        }

        [Fact]
        public void Login_ShouldReturnUserTokenDtoWithRole()
        {
            var role = new Role { Id = 1, Value = "admin" };
            var userLoginDto = UserLoginDtoBuilder.Simple().Build();
            userServiceMock.Setup(m => m.Validate(It.IsAny<UserLoginDto>())).Returns(new UserDto { Role = role });

            var result = sut.Login(userLoginDto);

            Assert.NotNull(result.User.Role);
        }

        [Fact]
        public void Login_ShouldReturnNullForIvalidUser()
        {
            var userLoginDto = UserLoginDtoBuilder.Simple().Build();
            userServiceMock.Setup(m => m.Validate(It.IsAny<UserLoginDto>())).Returns(null as UserDto);

            var result = sut.Login(userLoginDto);

            Assert.Null(result);
        }
    }
}
