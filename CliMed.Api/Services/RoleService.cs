using CliMed.Api.Data;
using CliMed.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CliMed.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        public RoleService([FromServices] DataContext context)
        {
            _context = context;
        }
        public IList<Role> GetAllItems()
        {
            return _context.Roles.ToList();
        }
    }
}
