using EShopper.Entities.Models;
using System.Security.Claims;

namespace EShopper.Business.Identity.Jwt.JwtManager
{
    public interface IJwtManager
    {
        JwtResponse GenerateTokens(string email, Claim[] claims);
        Claim[] GenerateClaims(EShopperUser eShopperUser);
    }
}