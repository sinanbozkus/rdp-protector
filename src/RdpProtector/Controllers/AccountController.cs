using FormHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RdpProtector.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RdpProtector.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        public string SecurityKey => _config["SecurityKey"];

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [FormValidator] // http://github.com/sinanbozkus/formhelper
        public async Task<IActionResult> Index(AccountViewModel formData)
        {
            if(SecurityKey == formData.SecurityKey)
            {
                var claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = new DateTimeOffset(DateTime.Now.AddHours(1))
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return FormResult.CreateSuccessResult("Please wait...", Url.Action("Index", "Home"));
            }

            return FormResult.CreateErrorResult("Wrong security key!");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }
    }
}
