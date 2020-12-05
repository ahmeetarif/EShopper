using AutoMapper;
using EShopper.Business.Extensions;
using EShopper.Business.Identity;
using EShopper.Business.Identity.Jwt;
using EShopper.Business.Identity.Jwt.JwtManager;
using EShopper.Business.Services.Abstract;
using EShopper.Common.Exceptions;
using EShopper.Contracts.V1.Requests.Authentication;
using EShopper.Contracts.V1.Responses.Authentication;
using EShopper.DataAccess.UnitOfWork;
using EShopper.Dto.Models;
using EShopper.EmailManager.Abstract;
using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EShopper.Business.Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtManager _jwtManager;
        private readonly EShopperUserManager _eShopperUserManager;
        private readonly IEmailSender _emailSender;
        public AuthenticationService(
            IUnitOfWork unitOfWork,
            EShopperUserManager eShopperUserManager,
            IJwtManager jwtManager,
            IMapper mapper,
            IEmailSender emailSender,
            ILogger<AuthenticationService> logger)
        {
            _unitOfWork = unitOfWork;
            _eShopperUserManager = eShopperUserManager;
            _jwtManager = jwtManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _logger = logger;
        }

        #region Register

        public async Task<AuthenticationResponseModel> RegisterAsync(RegisterRequestModel registerRequestModel)
        {
            if (registerRequestModel == null) throw new EShopperException("Please provide required information!");

            EShopperUser isEmailExist = await _eShopperUserManager.FindByEmailAsync(registerRequestModel.Email);
            if (isEmailExist != null) throw new EShopperException("Email address already exist!");

            EShopperUser eShopperIdentity = new EShopperUser
            {
                Email = registerRequestModel.Email,
                UserName = registerRequestModel.Username
            };

            // Begin Transaction...
            using (var transaction = _unitOfWork.EShopperDbContext.Database.BeginTransaction())
            {
                IdentityResult registerEShopperUserResult = await _eShopperUserManager.CreateAsync(eShopperIdentity, registerRequestModel.Password);

                if (registerEShopperUserResult.Succeeded)
                {
                    UserDetails usersDetail = new UserDetails
                    {
                        User = eShopperIdentity,
                        Fullname = registerRequestModel.Fullname,
                        RegisterDate = DateTime.UtcNow
                    };

                    _unitOfWork.UsersDetail.Add(usersDetail);
                    _unitOfWork.Complete();

                    transaction.Commit();
                    transaction.Dispose();

                    JwtManagerResponse jwtResponse = await _jwtManager.GenerateToken(eShopperIdentity);

                    EShopperUser getUserDetails = _eShopperUserManager.GetUserWithUserDetailsByEmail(registerRequestModel.Email);

                    EShopperUserDto mappedUserDetails = _mapper.Map<EShopperUserDto>(getUserDetails);

                    _logger.LogInformation($"{getUserDetails.Email} - Registered with EShopperAuthentication");

                    return new AuthenticationResponseModel
                    {
                        AccessToken = jwtResponse.AccessToken,
                        RefreshToken = jwtResponse.RefreshToken,
                        EShopperUser = mappedUserDetails
                    };
                }
            }

            throw new EShopperException();
        }

        #endregion

        #region Login

        public async Task<AuthenticationResponseModel> LoginAsync(LoginRequestModel loginRequestModel)
        {
            if (loginRequestModel == null) throw new EShopperException("Please provide required information!");

            EShopperUser getUserDetails = _eShopperUserManager.GetUserWithUserDetailsByEmail(loginRequestModel.Email);
            if (getUserDetails == null) throw new EShopperException("User not found!");

            if (_eShopperUserManager.CheckUserDeletion(getUserDetails)) throw new EShopperException("This user is Deleted!");
            if (_eShopperUserManager.IsUserDeactivated(getUserDetails)) throw new EShopperException("This is is Deactivated!");

            bool checkUserPassword = await _eShopperUserManager.CheckPasswordAsync(getUserDetails, loginRequestModel.Password);
            if (!checkUserPassword) throw new EShopperException("Password is invalid!");

            JwtManagerResponse getToken = await _jwtManager.GenerateToken(getUserDetails);

            EShopperUserDto mappedUserDetails = _mapper.Map<EShopperUserDto>(getUserDetails);

            _logger.LogInformation($"{getUserDetails.Email} - Just landed!");

            return new AuthenticationResponseModel
            {
                AccessToken = getToken.AccessToken,
                RefreshToken = getToken.RefreshToken,
                EShopperUser = mappedUserDetails
            };
        }

        #endregion

        #region Email Confirmation

        public async Task<AuthenticationResponseModel> SendEmailConfirmationLinkAsync(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) throw new EShopperException("Please provide required information!");

            EShopperUser getUserDetails = await _eShopperUserManager.FindByEmailAsync(email);
            if (getUserDetails == null) throw new EShopperException("User not found!");

            bool isUserEmailConfirmed = await _eShopperUserManager.IsEmailConfirmedAsync(getUserDetails);
            if (isUserEmailConfirmed) throw new EShopperException("Email address already confirmed!");

            string emailConfirmationLink = await _eShopperUserManager.GenerateEmailConfirmationTokenAsync(getUserDetails);

            try
            {
                await _emailSender.SendEmailConfirmation(email, emailConfirmationLink);
            }
            catch (Exception ex)
            {
                // TODO : Log Exception
                _logger.LogError(ex.Message);
                throw new EShopperException();
            }

            return new AuthenticationResponseModel
            {
                Message = "Email Confirmation Link has successfuly sent to your Mail address! Please check your inbox!"
            };
        }

        public async Task<AuthenticationResponseModel> ConfirmEmailAsync(ConfirmEmailRequestModel confirmEmailRequestModel)
        {
            if (confirmEmailRequestModel == null) throw new EShopperException("Please provide required information!");

            EShopperUser getUserDetails = await _eShopperUserManager.FindByEmailAsync(confirmEmailRequestModel.Email);
            if (getUserDetails == null) throw new EShopperException("User not found!");

            bool isEmailConfirmed = await _eShopperUserManager.IsEmailConfirmedAsync(getUserDetails);
            if (isEmailConfirmed) throw new EShopperException("Email address already confirmed!");

            IdentityResult confirmEmailResult = await _eShopperUserManager.ConfirmEmailAsync(getUserDetails, confirmEmailRequestModel.ConfirmationToken);

            if (confirmEmailResult.Succeeded)
            {
                return new AuthenticationResponseModel
                {
                    Message = "Email address successfully confirmed!"
                };
            }

            throw new EShopperException();
        }

        #endregion

        #region Password Reset

        public async Task<AuthenticationResponseModel> SendResetPasswordLinkAsync(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            if (forgotPasswordRequestModel == null) throw new EShopperException("Please provide required information!");

            EShopperUser getUserDetails = await _eShopperUserManager.FindByEmailAsync(forgotPasswordRequestModel.Email);
            if (getUserDetails == null) throw new EShopperException("User not found!");

            string userResetToken = await _eShopperUserManager.GeneratePasswordResetTokenAsync(getUserDetails);

            try
            {
                await _emailSender.SendResetPasswordLinkAsync(forgotPasswordRequestModel.Email, userResetToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new EShopperException();
            }

            return new AuthenticationResponseModel
            {
                Message = "Reset Password Link has successfuly sent to your Mail address! Please check your inbox!"
            };
        }

        public async Task<AuthenticationResponseModel> ResetPasswordAsync(ResetPasswordRequestModel resetPasswordRequestModel)
        {
            if (string.IsNullOrEmpty(resetPasswordRequestModel.Email) && string.IsNullOrWhiteSpace(resetPasswordRequestModel.Email) || string.IsNullOrEmpty(resetPasswordRequestModel.ResetToken) && string.IsNullOrWhiteSpace(resetPasswordRequestModel.ResetToken)) throw new EShopperException("Please provide required information!");

            EShopperUser getUserDetails = await _eShopperUserManager.FindByEmailAsync(resetPasswordRequestModel.Email);
            if (getUserDetails == null) throw new EShopperException("User not found!");

            IdentityResult resetPasswordResult = await _eShopperUserManager.ResetPasswordAsync(getUserDetails, resetPasswordRequestModel.ResetToken, resetPasswordRequestModel.Password);

            if (!resetPasswordResult.Succeeded)
            {
                _logger.LogError(resetPasswordResult.Errors.Select(x => x.Description).FirstOrDefault());
                throw new EShopperException();
            };

            return new AuthenticationResponseModel
            {
                Message = "Password has successfully changed!"
            };
        }

        #endregion

        #region External Logins



        #endregion

    }
}