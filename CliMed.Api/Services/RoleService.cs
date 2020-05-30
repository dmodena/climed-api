using CliMed.Api.Models;
using CliMed.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CliMed.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService([FromServices] IRoleRepository repository)
        {
            _repository = repository;
        }
        public IList<Role> GetAllItems()
        {
            return _repository.GetlAllItems();
        }

        public Role GetByValue(string value)
        {
            return _repository.GetByValue(value);
        }
    }
}
