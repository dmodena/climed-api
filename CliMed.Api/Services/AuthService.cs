using CliMed.Api.Auth;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CliMed.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserService _userService;
        private readonly ITokens _tokens;

        public AuthService([FromServices] IRoleRepository roleRepository, [FromServices] IUserService userService, [FromServices] ITokens tokens)
        {
            _roleRepository = roleRepository;
            _userService = userService;
            _tokens = tokens;
        }

        public UserTokenDto SignUp(User user)
        {
            var anyAdminUser = _userService.GetByRoleValue("admin").Count > 0;
            if (anyAdminUser)
                return null;

            var adminRole = _roleRepository.GetByValue("admin");
            user.Role = adminRole;

            var userDb = _userService.Create(user);

            return GetUserTokenDto(userDb);
        }

        public UserTokenDto Login(User user)
        {
            var userDto = _userService.Validate(user);
            if (userDto == null)
                return null;

            return GetUserTokenDto(userDto);
        }

        private UserTokenDto GetUserTokenDto(UserDto userDto) => new UserTokenDto
        {
            User = userDto,
            Token = _tokens.GenerateToken(userDto)
        };
    }
}
