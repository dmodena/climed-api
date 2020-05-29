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
            return user;
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

        public User Update(User item)
        {
            var userDb = _context.Users.SingleOrDefault(u => u.Id == item.Id);
            if (userDb == null)
                return null;

            _context.Entry(userDb).CurrentValues.SetValues(item);
            _context.SaveChanges();
            return userDb;
        }
    }
}
