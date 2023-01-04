using Microsoft.AspNetCore.Mvc;
using ProductsCatalogCleanArch.Domain.Account;
using ProductsCatalogCleanArch.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authentication;

        public AccountController(IAuthenticate authentication)
        {
            _authentication = authentication;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                //cria o obj setando a url que o usuário se encontra
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _authentication.Authenticate(model.Email, model.Password);

            if (result)//se true redireciona o usuário de acordo com a url que ele se encontra
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Password must be strong.");
                return View(model);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _authentication.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Redirect("/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();
            return Redirect("/Account/Login");
        }
    }
}
