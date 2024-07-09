using Microsoft.AspNetCore.Mvc;
using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukEntityLayer.Concrete;
using SchopenSozlukPresentationLayer.Models;
using System.Security.Claims;

namespace SchopenSozlukPresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentService;
        private readonly IBaslikService _baslikService;
        private readonly IEntryService _entryService;
        public CommentController(ICommentRepository commentService ,IEntryService entryService ,IBaslikService baslikService)
        {
            _commentService = commentService;
            _baslikService = baslikService;
            _entryService = entryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Comment comment)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(userId, out var id);
            comment.UserId = id;
            comment.CreatedAt = DateTime.Now;

            if (!ModelState.IsValid)
            {
                if (comment.UserId == null || comment.UserId == 0)
                {
                    return RedirectToAction("Index", "Login");
                }

                else 
                { 
                  await _commentService.AddCommentAsync(comment);
                }

                // Yorum ekleme işlemi tamamlandıktan sonra başlık detayına yönlendir
                var entry = await _entryService.GetByIdAsync(comment.EntryId);
                if (entry == null)
                {
                    return NotFound(); // Entry bulunamazsa NotFound döndürün
                }
               
                return RedirectToAction("BaslikDetay", "Baslik" , new { id = entry.BaslikId });
            }

            // Eğer model geçerli değilse aynı sayfaya geri dön ve hataları göster
            var baslikId = comment.EntryId; // Yorum eklenmeye çalışılan entry'nin Id'sini al
            var baslik = await _baslikService.GetByIdAsync(baslikId);
            var entries = await _entryService.GetEntriesByBaslikIdAsync(baslikId);
            var viewModel = new BaslikDetayViewModel
            {
                Baslik = baslik,
                Entry = entries
            };

            return RedirectToAction("BaslikDetay" , viewModel);
        }
    }
}
