// EntryController.cs
using Microsoft.AspNetCore.Mvc;
using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchopenSozlukPresentationLayer.Controllers
{
    public class EntryController : Controller
    {
        private readonly IEntryService _entryService;
        private readonly IBaslikService _baslikService;
       
        public EntryController(IEntryService entryService, IBaslikService baslikService)
        {
            _entryService = entryService;
            _baslikService = baslikService;
        }

        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }

       [HttpPost]
       public async Task<IActionResult> Create(Entry entry, int baslikId)
       {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    // Kullanıcı oturumu yoksa, işlem yapamayız
                    return RedirectToAction("Index", "Login"); // veya hata mesajı gösterebilirsiniz
                }

                int.TryParse(userId, out var id);
                var userName = entry.User?.UserName; // User null olabilir, bu yüzden null kontrolü yapın
                if (!ModelState.IsValid)
                {
                    entry.UserId = id;
                    entry.CreatedAt = DateTime.Now;
                    entry.BaslikId = baslikId; // BaslikId'yi dışardan alacak mıydınız? Şu anda id'yi atadım.

                    // Eğer userName null değilse, Entry'nin User'ını güncelleyin
                    if (userName != null)
                    {
                        entry.User = new AppUser { UserName = userName };
                    }

                    await _entryService.AddAsync(entry);
                    return RedirectToAction("Index", "Home");
                }
                return View("Create", entry);
       }

       

        public async Task<IActionResult> Details(int id)
       {
            var entry = await _entryService.GetByIdAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return View(entry);
       }
    }   

}
