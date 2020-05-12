using CliMed.Api.Models;
using CliMed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CliMed.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService([FromServices] IUserRepository repository)
        {
            _repository = repository;
        }

        public IList<User> GetAllItems()
        {
            return _repository.GetAllItems();
        }

        public User GetById(long id)
        {
            return _repository.GetById(id);
        }

        public User GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public User Create(User user)
        {
            return _repository.Create(user);
        }
    }
}
