using CliMed.Api.Models;
using System.Collections.Generic;

namespace CliMed.Api.Repositories
{
    public interface IUserRepository : IReadableRepository<User>, IWritableRepository<User>
    {
        User GetByEmail(string email);
        IList<User> GetByRoleValue(string roleValue);
    }
}
