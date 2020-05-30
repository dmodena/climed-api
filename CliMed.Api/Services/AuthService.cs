using AutoMapper;
using CliMed.Api.Auth;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CliMed.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokens _tokens;
        private readonly IMapper _mapper;
        private readonly ICrypto _crypto;

        public AuthService([FromServices] IUserRepository userRepository, [FromServices] ITokens tokens, [FromServices] IMapper mapper, [FromServices] ICrypto crypto)
        {
            _userRepository = userRepository;
            _tokens = tokens;
            _mapper = mapper;
            _crypto = crypto;
        }

        public UserTokenDto Login(User user)
        {
            var userDb = _userRepository.GetByEmail(user.Email);

            if (userDb == null)
                return null;

            if (!_crypto.IsMatchPassword(user.Password, userDb.Password))
                return null;

            var token = _tokens.GenerateToken(userDb);

            var userTokenDto = new UserTokenDto
            {
                User = _mapper.Map<UserDto>(userDb),
                Token = token
            };

            return userTokenDto;
        }
    }
}
