using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CliMed.Api.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("value")]
        public string Value { get; set; }
    }
}
