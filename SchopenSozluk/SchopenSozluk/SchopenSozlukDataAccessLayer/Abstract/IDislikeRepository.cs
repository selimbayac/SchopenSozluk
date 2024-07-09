using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Abstract
{
    public interface IDislikeRepository
    {
        Task<bool> UserDislikedEntryAsync(int userId, int entryId);
        Task DislikeEntryAsync(int userId, int entryId);
        Task<int> GetDislikeCountAsync(int entryId);
    }
}
