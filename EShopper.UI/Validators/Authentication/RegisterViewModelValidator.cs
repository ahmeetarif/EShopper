using EShopper.UI.Extensions;
using EShopper.UI.Models.Authentication;
using FluentValidation;

namespace EShopper.UI.Validators.Authentication
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Fullname).Fullname();
            RuleFor(x => x.Username).Username();
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Password).Password();
        }
    }
}