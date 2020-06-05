using System.ComponentModel.DataAnnotations;

namespace CliMed.Api.Dto
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
