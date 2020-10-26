using EShopper.Business.Options;
using EShopper.Common.Middleware.Statics;
using EShopper.DataAccess.UnitOfWork;
using EShopper.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EShopper.Business.Identity.Jwt.JwtManager
{
    public class JwtManager : IJwtManager
    {
        private readonly JwtOptions _jwtOptions;
        public JwtManager(
            JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public Claim[] GenerateClaims(EShopperUser eShopperUser)
        {
            var userClaims = new[]
            {
                new Claim(UserClaimsType.UserId, eShopperUser.Id),
                new Claim(UserClaimsType.Email, eShopperUser.Email),
                new Claim(UserClaimsType.Fullname, eShopperUser.UsersDetail.Fullname),
                new Claim(UserClaimsType.RegisterDate, eShopperUser.UsersDetail.RegisterDate.ToShortDateString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return userClaims;
        }

        public JwtResponse GenerateTokens(Claim[] claims, EShopperUser eShopperUser)
        {
            JwtResponse jwtToken = GenerateToken(eShopperUser, claims);
            return jwtToken;
        }

        #region Private Functions

        private JwtResponse GenerateToken(EShopperUser eShopperUser, Claim[] claims)
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

            return new JwtResponse
            {
                AccessToken = tokenAsString
            };
        }

        private RefreshTokens GenerateRefreshToken(EShopperUser user, string tokenId)
        {
            RefreshTokens refreshTokens = new RefreshTokens();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshTokens.Token = Convert.ToBase64String(randomNumber);
            }
            refreshTokens.ExpiryDate = DateTime.Now.AddMonths(6);
            refreshTokens.CreationDate = DateTime.Now;
            refreshTokens.User = user;
            refreshTokens.JwtId = tokenId;

            return refreshTokens;
        }

        #endregion

    }
}