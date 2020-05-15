﻿using AutoMapper;
using CliMed.Api.Auth;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CliMed.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokens _tokens;
        private readonly IMapper _mapper;

        public AuthService([FromServices] IUserRepository userRepository, [FromServices] ITokens tokens, [FromServices] IMapper mapper)
        {
            _userRepository = userRepository;
            _tokens = tokens;
            _mapper = mapper;
        }

        public UserTokenDto Login(User user)
        {
            var userDb = _userRepository.GetByEmail(user.Email);

            if (userDb == null)
                return null;

            if (user.Password != userDb.Password)
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
