using AutoMapper;
using EShopper.Business.Identity;
using EShopper.Business.Identity.Jwt;
using EShopper.Business.Identity.Jwt.JwtManager;
using EShopper.Business.Services.Abstract;
using EShopper.Common.Exceptions;
using EShopper.Contracts.V1.Requests.Authentication;
using EShopper.Contracts.V1.Responses.Authentication;
using EShopper.DataAccess.UnitOfWork;
using EShopper.Dto.Models;
using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace EShopper.Business.Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtManager _jwtManager;
        private readonly EShopperUserManager _eShopperUserManager;
        public AuthenticationService(
            IUnitOfWork unitOfWork,
            EShopperUserManager eShopperUserManager,
            IJwtManager jwtManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _eShopperUserManager = eShopperUserManager;
            _jwtManager = jwtManager;
            _mapper = mapper;
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

            return new AuthenticationResponseModel
            {
                AccessToken = getToken.AccessToken,
                RefreshToken = getToken.RefreshToken,
                EShopperUser = mappedUserDetails
            };
        }

        #endregion

    }
}