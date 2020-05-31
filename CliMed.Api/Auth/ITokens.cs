using CliMed.Api.Dto;

namespace CliMed.Api.Auth
{
    public interface ITokens
    {
        string GenerateToken(UserDto userDto);
    }
}
