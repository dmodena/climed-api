using CliMed.Api.Data;
using CliMed.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CliMed.Api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository([FromServices] DataContext context)
        {
            _context = context;
        }

        public IList<Role> GetlAllItems()
        {
            return _context.Roles.ToList();
        }

        public Role GetByValue(string value)
        {
            return _context.Roles.FirstOrDefault(r => r.Value == value);
        }
    }
}
