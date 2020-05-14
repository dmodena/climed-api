using CliMed.Api.Models;

namespace CliMed.Api.Auth
{
    public interface ITokens
    {
        string GenerateToken(User user);
    }
}
