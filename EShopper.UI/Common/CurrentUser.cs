using EShopper.UI.Common.Statics;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace EShopper.UI.Common
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Fullname
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.Fullname).Value;
            }
        }

        public string Email
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.Email).Value;
            }
        }
    }
}