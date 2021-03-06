﻿using EShopper.Contracts.V1.Requests.Authentication;
using EShopper.Contracts.V1.Responses.Authentication;
using System.Threading.Tasks;

namespace EShopper.Business.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseModel> RegisterAsync(RegisterRequestModel registerRequestModel);

        Task<AuthenticationResponseModel> LoginAsync(LoginRequestModel loginRequestModel);

        Task<AuthenticationResponseModel> SendEmailConfirmationLinkAsync(string email);
        Task<AuthenticationResponseModel> ConfirmEmailAsync(ConfirmEmailRequestModel confirmEmailRequestModel);

        Task<AuthenticationResponseModel> ResetPasswordAsync(ResetPasswordRequestModel resetPasswordRequestModel);
        Task<AuthenticationResponseModel> SendResetPasswordLinkAsync(ForgotPasswordRequestModel forgotPasswordRequestModel);

    }
}