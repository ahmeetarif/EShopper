using EShopper.Common.Middleware.Statics;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace EShopper.Common.Middleware
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Id
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == UserClaimsType.UserId).Value;
            }
        }
    }
}