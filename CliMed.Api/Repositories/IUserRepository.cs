using CliMed.Api.Models;

namespace CliMed.Api.Repositories
{
    public interface IUserRepository : IReadableRepository<User>
    {
        User GetByEmail(string email);
        User Create(User user);
    }
}
