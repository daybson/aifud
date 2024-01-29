using Aifud.ViewModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aifud.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await userManager.FindByEmailAsync(loginViewModel.UserName);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync
                    (user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginViewModel.ReturnUrl);
                }
            }

            ModelState.AddModelError("falhaLogin", "Falha ao realizar login!");
            return View(loginViewModel);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel registroViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registroViewModel);
            }

            var user = new IdentityUser
            {
                UserName = registroViewModel.UserName,
                Email = registroViewModel.UserName,
                EmailConfirmed = true
            };
            await userManager.AddToRoleAsync(user, "Cliente");
            var result = await userManager.CreateAsync(user, registroViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ModelState.AddModelError("RegistroErro", "Falha ao registrar usuário");
            }

            return View(registroViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
