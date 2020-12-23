using EShopper.Business.Options;
using EShopper.Common.Exceptions;
using EShopper.Common.Middleware;
using EShopper.Common.Middleware.Statics;
using EShopper.Contracts.V1.Requests.Authentication;
using EShopper.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.Business.Identity.Jwt.JwtManager
{
    public class JwtManager : IJwtManager
    {
        private readonly JwtOptions _jwtOptions;
        private readonly EShopperUserManager _eShopperUserManager;
        private readonly CurrentUser _currentUser;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public JwtManager(
            JwtOptions jwtOptions,
            EShopperUserManager eShopperUserManager,
            CurrentUser currentUser,
            TokenValidationParameters tokenValidationParameters)
        {
            _jwtOptions = jwtOptions;
            _eShopperUserManager = eShopperUserManager;
            _currentUser = currentUser;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<JwtManagerResponse> GenerateToken(EShopperUser eShopperUser)
        {
            var userClaims = await GenerateClaims(eShopperUser);
            string generateJwtToken = GenerateToken(userClaims);
            string generateRefreshToken = await GenerateRefreshToken(eShopperUser, _currentUser.AuthenticationType);

            return new JwtManagerResponse
            {
                AccessToken = generateJwtToken,
                RefreshToken = generateRefreshToken
            };
        }

        public async Task<JwtManagerResponse> RefreshTokenAsync(RefreshTokenRequestModel refreshTokenRequest)
        {
            if (refreshTokenRequest == null) throw new EShopperException("Please provide required information!");

            var validatedToken = GetPrincipalFromToken(refreshTokenRequest.AccessToken);

            if (validatedToken == null) throw new EShopperException("Invalid Access Token!");

            string userEmail = validatedToken.Claims.FirstOrDefault(x => x.Type == UserClaimsType.Email).Value;

            var userDetails = await _eShopperUserManager.FindByEmailAsync(userEmail);

            if (userDetails == null) throw new EShopperException("User not found!");

            var validateRefreshToken = await _eShopperUserManager.VerifyUserTokenAsync(userDetails, "EShopperAuthentication", "RefreshToken", refreshTokenRequest.RefreshToken);

            if (!validateRefreshToken)
            {
                // Invalid Refresh Token!
                throw new EShopperException("This refresh token is invalid!");
            }

            var newClaims = await GenerateClaims(userDetails);
            var newRefreshToken = await GenerateRefreshToken(userDetails, "EShopperAuthentication");
            var newAccessToken = GenerateToken(newClaims);

            return new JwtManagerResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        #region Private Functions

        private async Task<Claim[]> GenerateClaims(EShopperUser eShopperUser)
        {
            var roles = await _eShopperUserManager.GetRolesAsync(eShopperUser);

            var userClaims = new[]
           {
                new Claim(UserClaimsType.UserId, eShopperUser.Id),
                new Claim(UserClaimsType.Email, eShopperUser.Email),
                new Claim(UserClaimsType.Fullname, eShopperUser.UserDetails.Fullname),
                new Claim(UserClaimsType.RegisterDate, eShopperUser.UserDetails.RegisterDate.ToShortDateString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return userClaims;
        }

        private async Task<string> GenerateRefreshToken(EShopperUser user, string provider)
        {
            string getUserAuthenticationToken = await _eShopperUserManager.GetAuthenticationTokenAsync(user, provider, "RefreshToken");

            if (getUserAuthenticationToken != null)
            {
                IdentityResult removeUserAuthenticationTokenResult = await _eShopperUserManager.RemoveAuthenticationTokenAsync(user, provider, "RefreshToken");
            }

            var newUserRefreshToken = await _eShopperUserManager.GenerateUserTokenAsync(user, provider, "RefreshToken");
            var saveUserRefreshToken = await _eShopperUserManager.SetAuthenticationTokenAsync(user, provider, "RefreshToken", newUserRefreshToken);

            if (!saveUserRefreshToken.Succeeded)
            {
                //TODO: Throw Exception?? Try Again?? Return Refresh??
            }

            return newUserRefreshToken;
        }

        private string GenerateToken(Claim[] claims)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.Now.AddMinutes(_jwtOptions.TokenLifetime.TotalMinutes),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenAsString = tokenHandler.WriteToken(token);

            return tokenAsString;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameter = _tokenValidationParameters.Clone();
                tokenValidationParameter.ValidateLifetime = false;

                var principals = jwtHandler.ValidateToken(token, tokenValidationParameter, out SecurityToken validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principals;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }


        #endregion
    }
}