using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Integrador.Web.Models;
using Integrador.Web.Services;

namespace Integrador.Web.Controllers
{
    public class IdentityController : MainController
    {
        private readonly IAuthService _authenticationService;

        public IdentityController(
            IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<IActionResult> Register(UserRegister user)
        {
            if (!ModelState.IsValid) return View(user);

            var response = await _authenticationService.NewUSer(user);

            if (ResponseWithError(response.ResponseResult)) return View(user);

            //Fazer login
            await _authenticationService.AccomplishLogin(response);

            return RedirectToAction("Index", "Catalog");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(userLogin);

            var response = await _authenticationService.Login(userLogin);

            if (ResponseWithError(response.ResponseResult)) return View(userLogin);

            await _authenticationService.AccomplishLogin(response);

            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Catalog");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return RedirectToAction("Index", "Catalog");
        }
    }
}
