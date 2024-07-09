using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SchopenSozlukPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBaslikService _baslikService;
        private readonly IEntryService _entryService;
        private readonly IUserService _userService; // Ekledim
        

        public HomeController(IBaslikService baslikService, IEntryService entryService, IUserService userService )
        {
            _baslikService = baslikService;
            _entryService = entryService;
            _userService = userService;
         
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var basliklar = await _baslikService.GetAllAsync();
            var entries = await _entryService.GetAllAsync();

            var userIds = entries.Select(e => e.UserId).Distinct().ToList();
            var userNames = new Dictionary<int, string>();

            foreach (var userId in userIds)
            {
                var userName = await _entryService.GetUserNameByUserIdAsync(userId);
                userNames.Add(userId, userName);
            }

            foreach (var entry in entries)
            {
                entry.LikesCount = await _entryService.GetLikeCountAsync(entry.Id);
                entry.DislikesCount = await _entryService.GetDislikeCountAsync(entry.Id);
            }

            var model = new HomeIndexViewModel
            {
                Basliklar = basliklar.OrderBy(x => Guid.NewGuid()).Take(5).ToList(),// ayarı açarım birazdan 
                Entries = entries.OrderBy(x => Guid.NewGuid()).Take(10).ToList(),
                UserNames = userNames
            };

            return View(model);
        }

        public async Task<IActionResult> BaslikDetay(int id)
        {
            var baslik = await _baslikService.GetByIdAsync(id);
            if (baslik == null)
            {
                return NotFound();
            }

            return View(baslik);
        }

        [HttpPost]
        public async Task<IActionResult> EntryEkle(string name, string content)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingBaslik = await _baslikService.GetByNameAsync(name);

            if (existingBaslik == null)
            {
                var newBaslik = new Baslik
                {
                    Name = name,
                    CreatedAt = DateTime.Now,
                    UserId = int.Parse(userId)
                };

                await _baslikService.AddAsync(newBaslik);
                existingBaslik = newBaslik;
            }

            var entry = new Entry
            {
                BaslikId = existingBaslik.Id,
                Content = content,
                CreatedAt = DateTime.Now,
                UserId = int.Parse(userId)
            };

            await _entryService.AddAsync(entry);

            return RedirectToAction("BaslikDetay", new { id = existingBaslik.Id });
        }

        [HttpPost]
        public async Task<IActionResult> EntryYanıtla(int parentEntryId, string content)
        {
            var parentEntry = await _entryService.GetByIdAsync(parentEntryId);
            if (parentEntry == null)
            {
                return NotFound();
            }

            var entry = new Entry
            {
                BaslikId = parentEntry.BaslikId,
                Content = content,
                CreatedAt = DateTime.Now,
                UserId = 18, // Gerçek kullanıcı id'si buraya gelmeli
                ParentEntryId = parentEntryId
            };

            await _entryService.AddAsync(entry);
            return RedirectToAction("BaslikDetay", new { id = parentEntry.BaslikId });
        } 
        [HttpPost]
        public async Task<IActionResult> EntryBegeni(int entryId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _entryService.LikeEntryAsync(entryId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EntryDislike(int entryId)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _entryService.DislikeEntryAsync(entryId, userId);
            return RedirectToAction("Index");
        }
    }
}