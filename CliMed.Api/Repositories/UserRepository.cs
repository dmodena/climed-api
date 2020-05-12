using CliMed.Api.Data;
using CliMed.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CliMed.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository([FromServices] DataContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public IList<User> GetAllItems()
        {
            return _context.Users.ToList();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(long id)
        {
            return _context.Users.FirstOrDefault(u => u.Id.Value == id);
        }
    }
}
