using EShopper.UI.Extensions;
using EShopper.UI.Models.Authentication;
using FluentValidation;

namespace EShopper.UI.Validators.Authentication
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Password).Password();
        }
    }
}