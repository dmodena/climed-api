using CliMed.Api.Data;
using CliMed.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CliMed.Api.Tests.Data
{
    public class InMemoryDataContext : DataContext
    {
        private static InMemoryDataContext _instance;

        private InMemoryDataContext(DbContextOptions<DataContext> options) : base(options) { }

        public static DataContext GetInstance()
        {
            if (_instance != null)
                return _instance;

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _instance = new InMemoryDataContext(options);

            Seed();

            return _instance;
        }

        private static void Seed()
        {
            var adminRole = new Role { Id = 1, Value = "admin" };

            _instance.Roles.Add(adminRole);

            _instance.Users.AddRange(
                new[]
                {
                    new User { Id = 1, Email = "john.doe@example.com", Password = "johndoe", Username = "johndoe" },
                    new User { Id = 2, Email = "admmin@example.com", Password = "admin", Username = "mradmin", RoleId = 1, Role = adminRole }
                });

            _instance.SaveChanges();
        }
    }
}
