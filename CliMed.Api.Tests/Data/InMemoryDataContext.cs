using CliMed.Api.Data;
using CliMed.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CliMed.Api.Tests.Data
{
    public class InMemoryDataContext : DataContext
    {
        private InMemoryDataContext(DbContextOptions<DataContext> options) : base(options) { }

        public static DataContext GetInstance()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new InMemoryDataContext(options);

            Seed(context);

            return context;
        }

        private static void Seed(InMemoryDataContext context)
        {
            var adminRole = new Role { Id = 1, Value = "admin" };

            context.Roles.Add(adminRole);

            context.Users.AddRange(
                new[]
                {
                    new User { Id = 1, Email = "john.doe@example.com", Password = "johndoe", Username = "johndoe" },
                    new User { Id = 2, Email = "admmin@example.com", Password = "admin", Username = "mradmin", RoleId = 1, Role = adminRole }
                });

            context.SaveChanges();
        }
    }
}
