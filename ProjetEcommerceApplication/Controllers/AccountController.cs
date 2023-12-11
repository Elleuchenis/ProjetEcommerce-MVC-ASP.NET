using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjetEcommerceApplication.ViewModels;

namespace ProjetEcommerceApplication.Controllers
{
    
    
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser>
        signInManager;
        public AccountController(UserManager<IdentityUser>
        userManager, SignInManager<IdentityUser> signInManager)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new IdentityUser
                {
                    
                    Email = model.Email,
                    UserName = model.Email,
                    
                };

            
                // Store user data in AspNetUsers database table
                var result = await userManager.CreateAsync(user, model.Password);
                // If user is successfully created, sign-in the user using
                // SignInManager and redirect to index action of HomeController
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Catalogue", "Article");
                }
                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Catalogue", "Article");
        }
        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)

        { 
            if
            (ModelState.IsValid)

            {
                var result = await signInManager.PasswordSignInAsync(model.Email,model.Password, model.RememberMe, false);
                if
                (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))

                    {
                        return LocalRedirect(returnUrl);

                    }
                    else
                    {
                        return RedirectToAction("Catalogue", "Article");

                    }
                }
                ModelState.AddModelError(string.Empty,"Invalid Login Attempt");
            
}
            return View(model);
        }
    }
}
