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
    public class LikeRepository : ILikeRepository
    {

        private readonly Context _context;

        public LikeRepository(Context context)
        {
            _context = context;
        }
        public async Task<bool> UserLikedEntryAsync(int userId, int entryId)
        {
            return await _context.Likes.AnyAsync(l => l.UserId == userId && l.EntryId == entryId);
        }

        public async Task LikeEntryAsync(int userId, int entryId)
        {
            if (!await UserLikedEntryAsync(userId, entryId))
            {
                _context.Likes.Add(new Like { UserId = userId, EntryId = entryId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetLikeCountAsync(int entryId)
        {
            return await _context.Likes.CountAsync(l => l.EntryId == entryId);
        }
    }
}
