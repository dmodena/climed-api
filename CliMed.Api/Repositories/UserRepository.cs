using CliMed.Api.Data;
using CliMed.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return GetById(user.Id.Value);
        }

        public IList<User> GetAllItems()
        {
            return _context.Users.Include(u => u.Role).ToList();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == email);
        }

        public User GetById(long id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id.Value == id);
        }
    }
}
