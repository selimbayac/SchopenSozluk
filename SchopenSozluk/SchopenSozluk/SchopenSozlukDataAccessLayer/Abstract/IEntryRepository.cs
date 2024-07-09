using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Abstract
{
    public interface IEntryRepository : IGenericDal<Entry>
    {
        Task<List<Entry>> GetEntriesByBaslikIdAsync(int baslikId);
        Task<int> GetLikeCountAsync(int entryId);
        Task<int> GetDislikeCountAsync(int entryId);
        Task<Entry> GetEntryWithCommentsAsync(int entryId);
        Task<List<Comment>> GetCommentsByEntryIdAsync(int entryId);
        Task<List<Comment>> GetCommentsByEntryIdsAsync(List<int> entryIds);
        Task<Dictionary<int, string>> GetUserNamesByUserIdsAsync(List<int> userIds);
        Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(int userId);




        Task<Entry> GetByIdAsync(int id);   
       

        Task<Entry> GetEntryByIdAsync(int entryId);
        Task DeleteEntryWithRelatedDataAsync(int entryId); 
    }
}
