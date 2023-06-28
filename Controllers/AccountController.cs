using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVC_CarsForSale.Data;
using MVC_CarsForSale.Models;
using MVC_CarsForSale.ViewModels.Account;

namespace MVC_CarsForSale.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Error, email already registered");
                return View(registerVM);
            }
            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                EmailConfirmed = true
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Login", "Account");
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) { return View(loginVM); }

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            // User not found.
            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "This account doesn't exist.");
            }

            if (user != null)// User is found. Check password
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                // If passwordCheck Failed
                if (!await _userManager.CheckPasswordAsync(user, loginVM.Password))
                {
                    ModelState.AddModelError(string.Empty, "Invalid Password.");
                }

                if (passwordCheck)
                {
                    
                    //passsword correct signIn.
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "DashBoard");
                    }

                }
                return View(loginVM);
            }
            return View(loginVM);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
