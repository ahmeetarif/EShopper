using EShopper.Business.Identity.Jwt;
using EShopper.Business.Identity.Jwt.JwtManager;
using EShopper.Business.Services.Abstract;
using EShopper.Common.Exceptions;
using EShopper.Contracts.V1.Requests.Authentication;
using EShopper.Contracts.V1.Responses.Authentication;
using EShopper.DataAccess.UnitOfWork;
using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShopper.Business.Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtManager _jwtManager;
        private readonly UserManager<EShopperUser> _userManager;
        public AuthenticationService(
            IUnitOfWork unitOfWork,
            UserManager<EShopperUser> userManager,
            IJwtManager jwtManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _jwtManager = jwtManager;
        }

        public async Task<AuthenticationResponseModel> RegisterAsync(RegisterRequestModel registerRequestModel)
        {
            if (registerRequestModel == null) throw new EShopperException("Please provide required information!");

            EShopperUser isEmailExist = await _userManager.FindByEmailAsync(registerRequestModel.Email);
            if (isEmailExist != null) throw new EShopperException("Email address already exist!");

            EShopperUser eShopperIdentity = new EShopperUser
            {
                Email = registerRequestModel.Email,
                UserName = registerRequestModel.Email
            };

            // Begin Transaction...
            using (var transaction = _unitOfWork.EShopperDbContext.Database.BeginTransaction())
            {
                IdentityResult registerEShopperUserResult = await _userManager.CreateAsync(eShopperIdentity, registerRequestModel.Password);

                if (registerEShopperUserResult.Succeeded)
                {

                    UsersDetail usersDetail = new UsersDetail
                    {
                        User = eShopperIdentity,
                        Fullname = registerRequestModel.Fullname,
                        RegisterDate = DateTime.UtcNow
                    };

                    _unitOfWork.UsersDetail.Add(usersDetail);
                    _unitOfWork.Complete();

                    transaction.Commit();
                    transaction.Dispose();

                    // TODO: Generate RefreshToken...
                    Claim[] userClaims = _jwtManager.GenerateClaims(eShopperIdentity);
                    JwtResponse jwtResponse = _jwtManager.GenerateTokens(userClaims, eShopperIdentity);

                    return new AuthenticationResponseModel
                    {
                        AccessToken = jwtResponse.AccessToken
                    };
                }
            }

            throw new EShopperException();
        }

        public async Task<AuthenticationResponseModel> LoginAsync(LoginRequestModel loginRequestModel)
        {
            if (loginRequestModel == null) throw new EShopperException("Please provide required information!");



            return null;
        }

    }
}