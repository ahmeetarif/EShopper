using EShopper.Business.Extensions;
using EShopper.Contracts.V1.Requests.Authentication;
using FluentValidation;

namespace EShopper.Business.Validators.Authentication
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequestModel>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Password).Password();
            RuleFor(x => x.ResetToken).NotEmpty().WithMessage("You should provide a Reset Token!");
        }
    }
}