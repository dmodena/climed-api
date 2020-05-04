using CliMed.Api.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CliMed.Api.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public long? Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Required]
        [UniqueEmail]
        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role_id")]
        public long? RoleId { get; set; }

        public Role Role { get; set; }
    }
}
