using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchopenSozlukPresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IEntryService _entryService; // Entry işlemleri için servis ekledik
        private readonly IBaslikService _baslikService; // Başlık işlemleri için servis ekledik

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IUserService userService, IEntryService entryService, IBaslikService baslikService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userService = userService;
            _entryService = entryService;
            _baslikService = baslikService;
        }
        public async Task<IActionResult> Index()
        {
            var basliklar = await _baslikService.GetAllAsync(); // Örnek bir metot adı, servis ismiyle değiştirilmeli

            return View(basliklar); // View'a model olarak basliklar'ı gönder
        }
        // Kullanıcıları listeleme
        public async Task<IActionResult> ListUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        // Kullanıcı silme işlemi
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUserWithRelatedDataAsync(int.Parse(id)); // Eğer id int ise bu şekilde kullanın
            return RedirectToAction(nameof(ListUsers));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignAdminRole()
        {
            var users = await _userManager.Users.ToListAsync();
            var model = users.Select(u => new AssignAdminRoleViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToList();

            ViewBag.UserManager = _userManager;
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AssignAdminRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                var adminRole = await _roleManager.FindByNameAsync("Admin");
                if (adminRole == null)
                {
                    adminRole = new AppRole { Name = "Admin" };
                    await _roleManager.CreateAsync(adminRole);
                }

                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Admin rolü atama işlemi başarısız.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı zaten Admin rolüne sahip.");
            }

            var users = await _userManager.Users.ToListAsync(); // Kullanıcıları tekrar listele
            return View("Index", users);
        }

        // Başlık silme işlemi
        public async Task<IActionResult> DeleteBaslik(int id)
        {
            var baslik = await _baslikService.GetBaslikByIdAsync(id);
            if (baslik == null)
            {
                return NotFound();
            }

            // Başlık altındaki tüm entry'leri sil
            if (baslik.Entries != null)
            {
                foreach (var entry in baslik.Entries.ToList())
                {
                    await _entryService.DeleteEntryWithRelatedDataAsync(entry.Id);
                }
            }

            // Başlığı sil
            await _baslikService.DeleteBaslikAsync(id);

            return RedirectToAction(nameof(Index)); // veya başka bir sayfaya yönlendirme
        
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEntry()
        {
            var entries = await _entryService.GetAllAsync();
            return View(entries);
        }
        // Entry silme işlemi
        [HttpPost]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            try
            {
                await _entryService.DeleteEntryWithRelatedDataAsync(id);
                return RedirectToAction(nameof(DeleteEntry));
            }
            catch
            {
                ViewBag.ErrorMessage = $"An error occurred while deleting the entry with id {id}. Please try again later.";
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AssignAdminRoleToSelectedUsers(List<string> selectedUserIds)
        {
            if (selectedUserIds == null || !selectedUserIds.Any())
            {
                ModelState.AddModelError(string.Empty, "Lütfen admin rolü atanacak en az bir kullanıcı seçin.");
                return RedirectToAction(nameof(ListUsers));
            }

            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                adminRole = new AppRole { Name = "Admin" };
                await _roleManager.CreateAsync(adminRole);
            }

            foreach (var userId in selectedUserIds)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    continue;
                }

                bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if (!isAdmin)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }

            return RedirectToAction(nameof(ListUsers));
        }
       
    }
}
