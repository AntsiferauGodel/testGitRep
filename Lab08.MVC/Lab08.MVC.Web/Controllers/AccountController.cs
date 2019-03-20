using System;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Lab08.MVC.Web.Converters;
using Lab08.MVC.Business.Interfaces;
using Lab08.MVC.Business.Models;

namespace Lab08.MVC.Web.Controllers
{
    public class AccountController : Controller
    {
        private const string FailedLoginMessage = "Wrong login or password";

        private readonly IUserService userService;
        private readonly IAuthenticationManager authenticationManager;

        public AccountController(IUserService userService, IAuthenticationManager authenticationManager)
        {
            this.userService = userService;
            this.authenticationManager = authenticationManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = UserViewConverter.ConvertLoginToUserView(loginModel);
                var claim = userService.Authenticate(user);
                if (claim == null)
                {
                    ModelState.AddModelError(String.Empty, FailedLoginMessage);
                }
                else
                {
                    authenticationManager.SignOut();
                    authenticationManager.SignIn(
                        new AuthenticationProperties
                    {
                        IsPersistent = true
                    },
                        claim);
                    return RedirectToAction("Advertisements", "Home");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOut()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserViewConverter.ConvertRegisterToUserView(model);
                var operationResult = userService.Create(user);
                if (operationResult.Succeed)
                {
                    var claim = userService.Authenticate(user);
                    authenticationManager.SignIn(
                        new AuthenticationProperties
                    {
                        IsPersistent = true,
                    },
                        claim);
                    return View("SucceedRegister");
                }
                else
                {
                    ModelState.AddModelError(operationResult.Property, operationResult.Message);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            authenticationManager.SignOut();
            return View("Login");
        }

        public ActionResult ProfileDetails(string userId)
        {
            var user = userService.FindUserById(userId);
            return View(ApplicationUserToUserViewConverter.Convert(user));
        }
    }
}