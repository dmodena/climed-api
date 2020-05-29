using CliMed.Api.Models;

namespace CliMed.Api.Repositories
{
    public interface IUserRepository : IReadableRepository<User>, IWritableRepository<User>
    {
        User GetByEmail(string email);
    }
}
