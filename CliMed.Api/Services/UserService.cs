using CliMed.Api.Data;
using CliMed.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CliMed.Api.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService([FromServices] DataContext context)
        {
            _context = context;
        }

        public IList<User> GetAllItems()
        {
            return _context.Users.ToList();
        }

        public User GetById(long id)
        {
            return _context.Users.FirstOrDefault(u => u.Id.Value == id);
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Add(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
