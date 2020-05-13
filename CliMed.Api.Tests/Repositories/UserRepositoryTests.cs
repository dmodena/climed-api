using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Tests.Builders;
using CliMed.Api.Tests.Data;
using System.Linq;
using Xunit;

namespace CliMed.Api.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly InMemoryDataContext _context;
        private readonly UserRepository sut;

        public UserRepositoryTests()
        {
            _context = InMemoryDataContext.GetInstance() as InMemoryDataContext;
            sut = new UserRepository(_context);
        }

        [Fact]
        public void Create_ShouldSaveUserWithRole()
        {
            var user = UserBuilder.Simple().WithRoleId(1).Build();

            var result = sut.Create(user);

            Assert.IsType<User>(result);
            Assert.NotNull(result.Role);
        }

        [Fact]
        public void GetAllItems_ShouldReturnUsersWithRoles()
        {
            var result = sut.GetAllItems();
            var usersWithRoles = result.Where(u => u.Role?.Id > 0).ToList();

            Assert.True(usersWithRoles.Count > 0);
        }

    }
}
