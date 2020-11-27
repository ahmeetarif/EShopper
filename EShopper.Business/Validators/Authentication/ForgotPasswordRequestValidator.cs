using EShopper.Business.Extensions;
using EShopper.Contracts.V1.Requests.Authentication;
using FluentValidation;

namespace EShopper.Business.Validators.Authentication
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequestModel>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Email).Email();
        }
    }
}