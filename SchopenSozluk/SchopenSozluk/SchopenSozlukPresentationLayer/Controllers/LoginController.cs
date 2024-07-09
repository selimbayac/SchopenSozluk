using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;

namespace SchopenSozlukPresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Usarname, loginViewModel.Password, false, true); // kullanıcı hatırlasın  bence sondan 2. olan 

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.Usarname);

                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return RedirectToAction("AssignAdminRole", "Admin");
                }
                else if (user.EmailConfirmed)
                {
                    return RedirectToAction("Index", "MyProfile");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lütfen email adresinizi onaylayın.");
                    await _signInManager.SignOutAsync(); // Kullanıcıyı çıkış yapmaya zorla
                    return View(loginViewModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi.");
                return View(loginViewModel);
            }
        }
    }
}

