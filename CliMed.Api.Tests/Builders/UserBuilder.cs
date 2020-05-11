using CliMed.Api.Models;

namespace CliMed.Api.Tests.Builders
{
    public class UserBuilder
    {
        private readonly User _object = new User();

        public User Build()
        {
            return _object;
        }

        public static UserBuilder Default()
        {
            return new UserBuilder();
        }

        public static UserBuilder Simple()
        {
            return Default()
                .WithEmail("a@a.com")
                .WithPassword("a");
        }

        public static UserBuilder Typical()
        {
            return Simple()
                .WithUsername("a")
                .WithRole(RoleBuilder.Simple().Build());
        }

        public UserBuilder WithId(long id)
        {
            _object.Id = id;
            return this;
        }

        public UserBuilder WithUsername(string username)
        {
            _object.Username = username;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _object.Email = email;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _object.Password = password;
            return this;
        }

        public UserBuilder WithRoleId(long roleId)
        {
            _object.RoleId = roleId;
            _object.Role = null;
            return this;
        }

        public UserBuilder WithRole(Role role)
        {
            _object.Role = role;
            _object.RoleId = role.Id;
            return this;
        }
    }
}
