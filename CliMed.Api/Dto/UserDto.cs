using CliMed.Api.Models;

namespace CliMed.Api.Dto
{
    public class UserDto
    {
        public long? Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
