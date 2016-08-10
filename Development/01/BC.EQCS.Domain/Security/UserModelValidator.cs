using BC.EQCS.Security.Models;
using FluentValidation;

namespace BC.EQCS.Domain.Security
{
    public class UserModelValidator : ModelValidator<SecurityUserModel>
    {
        public UserModelValidator()
        {
            RuleFor(userModel => userModel.EmailAddress).NotEmpty().EmailAddress();
            RuleFor(userModel => userModel.Surname).NotEmpty();
            RuleFor(userModel => userModel.FirstName).NotEmpty();
        }
    }
}