using CliMed.Api.Models;

namespace CliMed.Api.Tests.Builders
{
    public class RoleBuilder
    {
        private readonly Role _object = new Role();

        public Role Build()
        {
            return _object;
        }

        public static RoleBuilder Default()
        {
            return new RoleBuilder();
        }

        public static RoleBuilder Simple()
        {
            return Default()
                .WithValue("admin");
        }

        public static RoleBuilder Typical()
        {
            return Simple();
        }

        public RoleBuilder WithId(long id)
        {
            _object.Id = id;
            return this;
        }

        public RoleBuilder WithValue(string value)
        {
            _object.Value = value;
            return this;
        }
    }
}
