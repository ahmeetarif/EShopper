using EShopper.Entities.Models;
using System.Security.Claims;

namespace EShopper.Business.Identity.Jwt.JwtManager
{
    public interface IJwtManager
    {
        JwtResponse GenerateTokens(Claim[] claims, EShopperUser eShopperUser);
        Claim[] GenerateClaims(EShopperUser eShopperUser);
    }
}