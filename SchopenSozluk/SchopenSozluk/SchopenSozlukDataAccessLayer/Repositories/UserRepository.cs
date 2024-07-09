using Microsoft.Data.SqlClient;
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
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<AppUser> GetUserWithDetails(int userId)
        {
            return await _context.Users
              .Include(u => u.Entries) // Kullanıcıya ait entryler
              .ThenInclude(e => e.Comments) // Entryler ile ilişkili yorumlar
              .Include(u => u.Entries) // Entryler ile ilişkili beğeniler
              .ThenInclude(e => e.Likes)
              .Include(u => u.Entries) // Entryler ile ilişkili beğenmeler
              .ThenInclude(e => e.Dislikes)
              .Include(u => u.Basliklar) // Kullanıcıya ait başlıklar
              .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task DeleteUserWithRelatedData(int userId)
        {
            try
            {
                // Kullanıcıyı ve ilişkili verilerini getir
                var userToDelete = await _context.Users
                   .Include(u => u.Entries) // Kullanıcıya ait entryler
                .ThenInclude(e => e.Comments) // Entryler ile ilişkili yorumlar
                .Include(u => u.Entries) // Entryler ile ilişkili beğeniler
                .ThenInclude(e => e.Likes)
                .Include(u => u.Entries) // Entryler ile ilişkili beğenmeler
                .ThenInclude(e => e.Dislikes)
                .Include(u => u.Basliklar) // Kullanıcıya ait başlıklar
                .FirstOrDefaultAsync(u => u.Id == userId);

                if (userToDelete == null)
                    throw new InvalidOperationException($"User with id {userId} not found.");

                // İlişkili verileri sil
                _context.Comments.RemoveRange(userToDelete.Entries.SelectMany(e => e.Comments));
                _context.Likes.RemoveRange(userToDelete.Entries.SelectMany(e => e.Likes));
                _context.Dislikes.RemoveRange(userToDelete.Entries.SelectMany(e => e.Dislikes));
                _context.Entryler.RemoveRange(userToDelete.Entries);
                _context.Basliklar.RemoveRange(userToDelete.Basliklar);

                // Kullanıcıyı sil
                _context.Users.Remove(userToDelete);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    // Foreign key constraint violation (referans kısıtlaması ihlali) hatası
                    throw new InvalidOperationException($"An error occurred while deleting the user with id {userId}: The user is referenced by other entities.");
                }
                else
                {
                    throw new InvalidOperationException($"An error occurred while deleting the user with id {userId}: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred while deleting the user with id {userId}: {ex.Message}");
            }
        }
        }
       

    }

