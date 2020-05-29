using CliMed.Api.Dto;
using CliMed.Api.Models;
using System.Collections.Generic;

namespace CliMed.Api.Services
{
    public interface IUserService
    {
        IList<UserDto> GetAllItems();
        UserDto GetById(long id);
        UserDto GetByEmail(string email);
        UserDto Create(User user);
        UserDto UpdatePassword(User user);
    }
}
