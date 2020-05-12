using CliMed.Api.Models;
using System.Collections.Generic;

namespace CliMed.Api.Services
{
    public interface IUserService
    {
        IList<User> GetAllItems();
        User GetById(long id);
        User GetByEmail(string email);
        User Create(User user);
    }
}
