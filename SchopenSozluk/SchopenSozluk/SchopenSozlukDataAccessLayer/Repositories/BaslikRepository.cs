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
    public class BaslikRepository : GenericRepository<Baslik>, IBaslikRepository
    {
        private readonly Context _context;

      
        public BaslikRepository(Context context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Baslik>> GetBasliklarByUserIdAsync(int userId)
        {
            return await Task.FromResult(_context.Basliklar.Where(b => b.UserId == userId).ToList());
        }

        public Task<List<Baslik>> GetListAsync()
        {
            throw new NotImplementedException();
        }
        public async Task DeleteBaslikAsync(int baslikId)
        {
            var baslik = await _context.Basliklar
        .Include(b => b.Entries)
        .ThenInclude(e => e.Likes)
        .Include(b => b.Entries)
        .ThenInclude(e => e.Dislikes)
        .Include(b => b.Entries)
        .ThenInclude(e => e.Comments)
        .FirstOrDefaultAsync(b => b.Id == baslikId);

            if (baslik != null)
            {
                foreach (var entry in baslik.Entries)
                {
                    _context.Likes.RemoveRange(entry.Likes);
                    _context.Dislikes.RemoveRange(entry.Dislikes);
                    _context.Comments.RemoveRange(entry.Comments);
                }

                _context.Entryler.RemoveRange(baslik.Entries);
                _context.Basliklar.Remove(baslik);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Baslik> GetBaslikByIdAsync(int baslikId)
        {
            return await _context.Basliklar.FindAsync(baslikId);
        }
    }
}
