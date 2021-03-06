﻿using EShopper.Business.Extensions;
using EShopper.Contracts.V1.Requests.Authentication;
using FluentValidation;

namespace EShopper.Business.Validators.Authentication
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).Email();
            RuleFor(x => x.Password).Password();
        }
    }
}