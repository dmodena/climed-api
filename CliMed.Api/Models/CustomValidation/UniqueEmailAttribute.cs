using CliMed.Api.Services;
using System.ComponentModel.DataAnnotations;

namespace CliMed.Api.Models.CustomValidation
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userService = (IUserService)validationContext.GetService(typeof(IUserService));
            var user = (User)validationContext.ObjectInstance;
            var dbUser = userService.GetByEmail(user.Email);

            if (dbUser == null)
                return ValidationResult.Success;

            if (user.Id.HasValue && user.Id == dbUser.Id)
                return ValidationResult.Success;

            return new ValidationResult(GetErrorMessage(user));
        }

        public string GetErrorMessage(User user) => $"The email {user.Email} already exists.";
    }
}
