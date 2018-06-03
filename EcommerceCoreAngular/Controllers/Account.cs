using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.ViewModels;
using EcommerceCoreAngular.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceCoreAngular.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<Customer> _signInManager;
        //https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity?tabs=visual-studio%2Caspnetcore2x
        //https://docs.microsoft.com/pt-br/aspnet/core/security/authentication/identity-configuration?tabs=aspnetcore2x
        public Account(UserManager<Customer> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<Customer> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        #region Register Settings
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer { UserName = registerVM.Email, Email = registerVM.Email };
                var result = await _userManager.CreateAsync(customer, registerVM.Password);

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("SiteUser").Result)
                    {
                        ApplicationRole role = new ApplicationRole();
                        role.Name = "SiteUser";

                        IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Somethings went wrong !");
                            return View(registerVM);
                        }
                    }

                    _userManager.AddToRoleAsync(customer, "SiteUser").Wait();
                    await _signInManager.SignInAsync(customer, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(registerVM);
        }
        #endregion

        #region Login Settings

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(
                    loginVM.Email,
                    loginVM.Password,
                    loginVM.RememberMe,
                    false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Somethings went wrong !");
                }

            }
            return View(loginVM);
        }

        #endregion

        #region Logout
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}