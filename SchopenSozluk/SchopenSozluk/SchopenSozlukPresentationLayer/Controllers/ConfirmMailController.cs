using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;

namespace SchopenSozlukPresentationLayer.Controllers
{
    public class ConfirmMailController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
         
        }

        [HttpGet]
        public  IActionResult Index()
        {
            var value = TempData["Mail"];
            ViewBag.v = value;
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Index(ConfirmMailViewModel confirm)
        {
            var value = TempData["Mail"];
            ViewBag.v = value;
            var user = await _userManager.FindByEmailAsync(confirm.Mail);
            if(user.ConfirmCode ==confirm.ConfirmCode)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
