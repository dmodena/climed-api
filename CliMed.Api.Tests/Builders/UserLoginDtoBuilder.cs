using CliMed.Api.Dto;

namespace CliMed.Api.Tests.Builders
{
    public class UserLoginDtoBuilder
    {
        private readonly UserLoginDto _object = new UserLoginDto();

        public UserLoginDto Build()
        {
            return _object;
        }

        public static UserLoginDtoBuilder Default()
        {
            return new UserLoginDtoBuilder();
        }

        public static UserLoginDtoBuilder Simple()
        {
            return Default()
                .WithEmail("a@a.com")
                .WithPassword("a");
        }

        public UserLoginDtoBuilder WithEmail(string email)
        {
            _object.Email = email;
            return this;
        }

        public UserLoginDtoBuilder WithPassword(string password)
        {
            _object.Password = password;
            return this;
        }
    }
}
