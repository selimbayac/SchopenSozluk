using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukBusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<AppUser> GetUserWithDetailsAsync(int userId)
        {
            return await _userRepository.GetUserWithDetails(userId);
        }

        public async Task DeleteUserWithRelatedDataAsync(int userId)
        {
            await _userRepository.DeleteUserWithRelatedData(userId);
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }
    }
}
