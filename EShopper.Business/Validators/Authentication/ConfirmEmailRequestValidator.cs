using EShopper.Business.Extensions;
using EShopper.Contracts.V1.Requests.Authentication;
using FluentValidation;

namespace EShopper.Business.Validators.Authentication
{
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequestModel>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.ConfirmationToken).NotEmpty().WithMessage("Please provide required information!");
        }
    }
}