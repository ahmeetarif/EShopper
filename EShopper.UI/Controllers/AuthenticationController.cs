using AutoMapper;
using EShopper.ApiContracts.V1.Requests.Authentication;
using EShopper.ApiService.Abstract;
using EShopper.UI.Common.Statics;
using EShopper.UI.Models.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EShopper.UI.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly IEShopperAuthenticationApiService _eShopperAuthenticationApiService;
        private readonly IMapper _mapper;
        public AuthenticationController(IEShopperAuthenticationApiService eShopperAuthenticationApiService,
            IMapper mapper)
        {
            _eShopperAuthenticationApiService = eShopperAuthenticationApiService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login() => View();

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var mappedRequestData = _mapper.Map<LoginRequestApiModel>(loginViewModel);
            var apiResponse = await _eShopperAuthenticationApiService.LoginAsync(mappedRequestData);

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(UserClaimTypes.Email,apiResponse.EShopperUser.Email),
                new Claim(UserClaimTypes.Fullname, apiResponse.EShopperUser.UserDetails.Fullname),
                new Claim(UserClaimTypes.AccessToken, apiResponse.AccessToken),
                new Claim(UserClaimTypes.RefreshToken, apiResponse.RefreshToken)
            }, "EShopperAuthentication");

            await HttpContext.SignOutAsync(scheme: "EShopperAuthentication");

            var principals = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("EShopperAuthentication", principals);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register() => View();
    }
}