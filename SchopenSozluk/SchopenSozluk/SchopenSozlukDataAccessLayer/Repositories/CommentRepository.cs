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
    public class CommentRepository : ICommentRepository
    {
        private readonly Context _context;

        public CommentRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetCommentsByEntryIdAsync(int entryId)
        {
              // Belirli bir girişin tüm yorumlarını al bu kodun aynısı daha yazma 
            return await _context.Comments
                .Where(c => c.EntryId == entryId)
                .ToListAsync();
        }

        public async Task AddCommentAsync(Comment comment)
        {
             _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }    
    }
}
