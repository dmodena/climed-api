using CliMed.Api.Models;
using CliMed.Api.Repositories;
using CliMed.Api.Tests.Data;
using Xunit;

namespace CliMed.Api.Tests.Repositories
{
    public class RoleRepositoryTests
    {
        private readonly InMemoryDataContext _context;
        private readonly RoleRepository sut;

        public RoleRepositoryTests()
        {
            _context = InMemoryDataContext.GetInstance() as InMemoryDataContext;
            sut = new RoleRepository(_context);
        }

        [Fact]
        public void GetByValue_ShouldReturnRole()
        {
            var result = sut.GetByValue("admin");

            Assert.NotNull(result);
            Assert.IsType<Role>(result);
        }
    }
}
