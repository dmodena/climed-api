using CliMed.Api.Dto;
using CliMed.Api.Models;

namespace CliMed.Api.Services
{
    public interface IAuthService
    {
        UserTokenDto Login(UserLoginDto userLoginDto);
        UserTokenDto SignUp(User user);
    }
}
