using EShopper.Contracts.V1.Requests.Authentication;
using EShopper.Entities.Models;
using System.Threading.Tasks;

namespace EShopper.Business.Identity.Jwt.JwtManager
{
    public interface IJwtManager
    {
        Task<JwtManagerResponse> GenerateToken(EShopperUser applicationUser);
        Task<JwtManagerResponse> RefreshTokenAsync(RefreshTokenRequestModel refreshTokenRequest);
    }
}