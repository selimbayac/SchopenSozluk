using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Abstract
{
    public interface ILikeRepository
    {
       Task<bool> UserLikedEntryAsync(int userId, int entryId);
       Task LikeEntryAsync(int userId, int entryId);
    }
}
