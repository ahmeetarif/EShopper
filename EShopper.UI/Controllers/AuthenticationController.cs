using EShopper.UI.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.UI.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            return View();
        }
    }
}