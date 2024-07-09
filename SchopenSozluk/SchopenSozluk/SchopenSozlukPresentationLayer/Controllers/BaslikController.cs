// BaslikController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukBusinessLayer.Concrete;
using SchopenSozlukDataAccessLayer.Repositories;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchopenSozlukPresentationLayer.Controllers
{
    public class BaslikController : Controller
    {
        private readonly IBaslikService _baslikService;
        private readonly IEntryService _entryService;
        private readonly UserManager<AppUser> _userManager;

        public BaslikController(IBaslikService baslikService , IEntryService entryService , UserManager<AppUser> userManager)
        {
            _baslikService = baslikService;
            _entryService = entryService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Baslik baslik)
        {
            var  userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          
            int.TryParse(userId, out var id);
            

            if (!ModelState.IsValid)
            {
                baslik.CreatedAt = DateTime.Now;
                baslik.UserId = id;
                await _baslikService.AddAsync(baslik);
                return RedirectToAction("BaslikDetay", "Baslik", new { id = baslik.Id });
            }
            return View("Create", baslik);
        }

        public async Task<IActionResult> BaslikDetay(string id)
        {
            if (!int.TryParse(id, out var baslikId))
            {
                return NotFound();
            }

            var baslik = await _baslikService.GetByIdAsync(baslikId);

            if (baslik == null)
            {
                return NotFound(); // Başlık bulunamazsa NotFound döndürün
            }

            var entries = await _entryService.GetEntriesByBaslikIdAsync(baslikId);

            // Entry ID'lerini topla
            var entryIds = entries.Select(e => e.Id).ToList();

            // Tüm yorumları toplu olarak al
            var allComments = await _entryService.GetCommentsByEntryIdsAsync(entryIds);

            // Kullanıcı ID'lerini topla
            var userIds = entries.Select(e => e.UserId).Distinct().ToList();

            // Tüm kullanıcı adlarını toplu olarak al
            var userNames = await _entryService.GetUserNamesByUserIdsAsync(userIds);

            // Her entry'ye ilgili yorumları ve kullanıcı adlarını ata
            foreach (var entry in entries)
            {
                entry.Comments = allComments.Where(c => c.EntryId == entry.Id).ToList();

                if (userNames.TryGetValue(entry.UserId, out var userName))
                {
                    entry.User.UserName = userName;
                }
                else
                {
                    entry.User.UserName = "Bilinmeyen Kullanıcı";
                }
            }

            var viewModel = new BaslikDetayViewModel
            {

                Baslik = baslik,
                Entry = entries,
                Comments = new List<Comment>()
            };
            

            return View(viewModel);
        }
    }
}
