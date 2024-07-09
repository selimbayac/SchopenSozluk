using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;
using System.Threading.Tasks;

namespace SchopenSozlukPresentationLayer.Controllers
{
    public class MyProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IBaslikService _baslikService;
        private readonly IEntryService _entryService;
        public MyProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IBaslikService baslikService, IEntryService entryService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _baslikService = baslikService;
            _entryService = entryService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            // Fetch Basliklar (Titles) for the user

            var basliklar = await _baslikService.GetBasliklarByUserIdAsync(user.Id);
            var entries = await _entryService.GetEntriesByUserIdAsync(user.Id);

            var model = new UserProfileViewModel
            {
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Bio = user.Bio,
                ProfileImage = user.ProfileImage,
                Basliklar = basliklar.ToList(),
                Entries = entries.ToList()
                //Basliklar = user.Basliklar != null ? user.Basliklar.ToList() : new List<Baslik>(),
                //Entries = user.Entries != null ? user.Entries.ToList() : new List<Entry>()
            };

            return View(model);
        }

        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }     

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.UserName = model.UserName;
            user.Bio = model.Bio;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UserProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var basliklar = await _baslikService.GetBasliklarByUserIdAsync(user.Id);
            var entries = await _entryService.GetEntriesByUserIdAsync(user.Id);

            var model = new UserProfileViewModel
            {
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Bio = user.Bio,
                ProfileImage = user.ProfileImage,
                Basliklar = basliklar.ToList(),
                Entries = entries.ToList()
            };

            return View(model);
        }
    }
}
