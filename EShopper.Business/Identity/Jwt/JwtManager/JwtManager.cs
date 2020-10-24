using EShopper.Business.Options;
using EShopper.Common.Middleware.Statics;
using EShopper.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace EShopper.Business.Identity.Jwt.JwtManager
{
    public class JwtManager : IJwtManager
    {
        private readonly JwtOptions _jwtOptions;

        public JwtManager(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public Claim[] GenerateClaims(EShopperUser eShopperUser)
        {
            var userClaims = new[]
            {
                new Claim(UserClaimsType.UserId, eShopperUser.Id)
            };

            return userClaims;
        }

        public JwtResponse GenerateTokens(string email, Claim[] claims)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.TokenLifetime.TotalMinutes),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtResponse
            {
                AccessToken = tokenAsString
            };
        }
    }
}
