using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Abstract
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByEntryIdAsync(int entryId);
        Task AddCommentAsync(Comment comment);
        Task DeleteCommentAsync(Comment comment);
    }
}
