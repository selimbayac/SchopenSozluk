using Microsoft.EntityFrameworkCore;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Concrete;
using SchopenSozlukDataAccessLayer.Repository;
using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Repositories
{
    public class EntryRepository : GenericRepository<Entry>, IEntryRepository
    {
        private readonly Context _context;

        public EntryRepository(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Entry>> GetEntriesByBaslikIdAsync(int baslikId)
        {
            return await _context.Entryler.Where(e => e.BaslikId == baslikId).ToListAsync();
        }
        public async Task<int> GetLikeCountAsync(int entryId)
        {
            return await _context.Likes.CountAsync(l => l.EntryId == entryId);
        }

        public async Task<int> GetDislikeCountAsync(int entryId)
        {
            return await _context.Dislikes.CountAsync(d => d.EntryId == entryId);
        }
        public async Task<Entry> GetEntryWithCommentsAsync(int entryId)
        {
            return await _context.Entryler
                .Include(e => e.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(e => e.Id == entryId);
        }

        public async Task<List<Comment>> GetCommentsByEntryIdAsync(int entryId)
        {
            return await _context.Comments
          .Where(c => c.EntryId == entryId)
          .ToListAsync();
        }
        public async Task<List<Comment>> GetCommentsByEntryIdsAsync(List<int> entryIds)
        {
            return await _context.Comments
                .Where(c => entryIds.Contains(c.EntryId))
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Dictionary<int, string>> GetUserNamesByUserIdsAsync(List<int> userIds)
        {
            return await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.UserName);
        }

        public async Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(int userId)
        {
            return await Task.FromResult(_context.Entryler.Where(e => e.UserId == userId).ToList());
        }
        public async Task DeleteEntryWithRelatedDataAsync(int entryId)
        {
            var entryToDelete = await GetEntryWithRelatedDataAsync(entryId);

            if (entryToDelete == null)
                return;

            _context.Dislikes.RemoveRange(entryToDelete.Dislikes);
            _context.Likes.RemoveRange(entryToDelete.Likes);
            _context.Comments.RemoveRange(entryToDelete.Comments);
            _context.Entryler.Remove(entryToDelete);

            await _context.SaveChangesAsync();
        }

        public async Task<Entry> GetEntryByIdAsync(int entryId)
        {
            return await _context.Entryler
                .Include(e => e.Likes)
                .Include(e => e.Dislikes)
                .FirstOrDefaultAsync(e => e.Id == entryId);
        }
        public async Task<List<Entry>> GetAllAsync()
        {
            return await _context.Entryler
                             .Include(e => e.Baslik) // Başlık ilişkisini de dahil ediyoruz
                             .ToListAsync();
        }
        public async Task<Entry> GetEntryWithRelatedDataAsync(int entryId)
        {
            return await _context.Entryler
                  .Include(e => e.Dislikes)
                  .Include(e => e.Likes)
                  .Include(e => e.Comments)
                  .FirstOrDefaultAsync(e => e.Id == entryId);
        }
    }
}

