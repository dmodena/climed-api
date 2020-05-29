﻿using AutoMapper;
using CliMed.Api.Dto;
using CliMed.Api.Models;
using CliMed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CliMed.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService([FromServices] IUserRepository repository, [FromServices] IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var userDb = _repository.Create(user);
            return _mapper.Map<UserDto>(userDb);
        }

        public UserDto UpdatePassword(User user)
        {
            var userDb = _repository.GetByEmail(user.Email);
            userDb.Password = user.Password;
            _repository.Update(userDb);
            return _mapper.Map<UserDto>(userDb);
        }
    }
}
