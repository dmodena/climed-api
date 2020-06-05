using AutoMapper;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CliMed.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICrypto _crypto;

        public UserService([FromServices] IUserRepository repository, [FromServices] IMapper mapper, [FromServices] ICrypto crypto)
        {
            _repository = repository;
            _mapper = mapper;
            _crypto = crypto;
        }

        public IList<UserDto> GetAllItems()
        {
            var userList = _repository.GetAllItems();
            return _mapper.Map<IList<UserDto>>(userList);
        }

        public UserDto GetById(long id)
        {
            var user = _repository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetByEmail(string email)
        {
            var user = _repository.GetByEmail(email);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto Create(User user)
        {
            user.Password = _crypto.HashPassword(user.Password);
            var userDb = _repository.Create(user);
            return _mapper.Map<UserDto>(userDb);
        }

        public UserDto UpdatePassword(UserLoginDto userLoginDto)
        {
            var userDb = _repository.GetByEmail(userLoginDto.Email);
            userDb.Password = _crypto.HashPassword(userLoginDto.Password);
            _repository.Update(userDb);
            return _mapper.Map<UserDto>(userDb);
        }

        public IList<UserDto> GetByRoleValue(string roleValue)
        {
            var users = _repository.GetByRoleValue(roleValue);
            return _mapper.Map<IList<UserDto>>(users);
        }

        public UserDto Validate(UserLoginDto userLoginDto)
        {
            var userDb = _repository.GetByEmail(userLoginDto.Email);
            if (userDb == null)
                return null;

            if (!_crypto.IsMatchPassword(userLoginDto.Password, userDb.Password))
                return null;

            return _mapper.Map<UserDto>(userDb);
        }
    }
}
