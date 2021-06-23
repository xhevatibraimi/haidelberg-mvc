using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.WebApp.Controllers.Identity
{
    public class IdentityController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByNameAsync(loginRequest.Login);
            if (user == null)
            {
                ViewBag.Errors = new List<string> { "user does not exist" };
                return View(loginRequest);
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);
            if(!signInResult.Succeeded)
            {
                ViewBag.Errors = new List<string>();
                if (signInResult.IsLockedOut)
                {
                    ViewBag.Errors.Add("account locked");
                }
                else
                {
                    ViewBag.Errors.Add("invalid password");
                }

                return View(loginRequest);
            }

            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new RegisterUserRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var identityResult = await _userManager.CreateAsync(new User(registerUserRequest.Login), registerUserRequest.Password);
            if (!identityResult.Succeeded)
            {
                ViewBag.Errors = identityResult.Errors.Select(x => x.Description).ToList();
                return View(registerUserRequest);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
