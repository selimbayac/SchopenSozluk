using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Abstract
{
    public interface IUserRepository 
    {
        Task<AppUser> GetByIdAsync(int userId);
        Task<AppUser> GetByUsernameAsync(string username);
        Task<AppUser> GetUserWithDetails(int userId);
        Task DeleteUserWithRelatedData(int userId);
    }
}
