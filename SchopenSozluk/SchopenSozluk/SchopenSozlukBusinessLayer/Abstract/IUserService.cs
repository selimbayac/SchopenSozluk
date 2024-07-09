using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukBusinessLayer.Abstract
{
    public interface IUserService
    {
        Task<AppUser> GetUserByIdAsync(int userId);
        Task<AppUser> GetByUsernameAsync(string username);
        Task<AppUser> GetUserWithDetailsAsync(int userId);
        Task DeleteUserWithRelatedDataAsync(int userId);

    }
}
