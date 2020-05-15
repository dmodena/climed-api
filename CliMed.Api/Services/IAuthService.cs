using CliMed.Api.Dto;
using CliMed.Api.Models;

namespace CliMed.Api.Services
{
    public interface IAuthService
    {
        UserTokenDto Login(User user);
    }
}
