using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukBusinessLayer.Abstract
{
    public interface IEntryService
    {
        Task<List<Entry>> GetAllAsync();
        Task<Entry> GetByIdAsync(int id);
        Task AddAsync(Entry entry);
        Task UpdateAsync(Entry entry);
        Task DeleteAsync(Entry entry);
        Task LikeEntryAsync(int entryId, int userId);
        Task DislikeEntryAsync(int entryId, int userId);
        Task<List<Entry>> GetEntriesByBaslikIdAsync(int baslikId);
        Task<string> GetUserNameByUserIdAsync(int userId);
        Task<int> GetLikeCountAsync(int entryId);
        Task<int> GetDislikeCountAsync(int entryId);
        Task<Entry> GetEntryWithCommentsAsync(int entryId);

        Task<Dictionary<int, string>> GetUserNamesByUserIdsAsync(List<int> userIds);
        Task<List<Comment>> GetCommentsByEntryIdsAsync(List<int> entryIds);

        Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(int userId);

        Task DeleteEntryWithRelatedDataAsync(int entryId);
        Task<Entry> GetEntryByIdAsync(int entryId);



       
    }
}
