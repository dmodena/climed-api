using CliMed.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CliMed.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
