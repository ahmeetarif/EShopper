using EShopper.Business.Extensions;
using EShopper.Contracts.V1.Requests.Authentication;
using FluentValidation;

namespace EShopper.Business.Validators.Authentication
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Fullname).Fullname();
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.Username).Username();
        }
    }
}