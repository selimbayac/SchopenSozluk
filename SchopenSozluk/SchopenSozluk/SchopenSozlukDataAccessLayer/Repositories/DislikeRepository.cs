using Microsoft.EntityFrameworkCore;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Concrete;
using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Repositories
{
    public class DislikeRepository : IDislikeRepository
    {
        private readonly Context _context;

        public DislikeRepository(Context context)
        {
            _context = context;
        }
        public async Task<bool> UserDislikedEntryAsync(int userId, int entryId)
        {
            return await _context.Dislikes.AnyAsync(d => d.UserId == userId && d.EntryId == entryId);
        }

        public async Task DislikeEntryAsync(int userId, int entryId)
        {
            if (!await UserDislikedEntryAsync(userId, entryId))
            {
                _context.Dislikes.Add(new Dislike { UserId = userId, EntryId = entryId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetDislikeCountAsync(int entryId)
        {
            return await _context.Dislikes.CountAsync(d => d.EntryId == entryId);
        }
    }
}
