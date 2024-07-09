using Microsoft.EntityFrameworkCore;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Concrete;
using SchopenSozlukEntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                // Hata detaylarını konsola yaz
                Console.WriteLine("GetAllAsync metodu çalışırken bir hata oluştu: " + ex.Message);

                // Hatanın tekrar fırlatılması, hatayı üst katmana aktarır
                throw;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task InsertAsync(T t)
        {
            await _context.Set<T>().AddAsync(t);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T t)
        {
            _context.Set<T>().Update(t);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T t)
        {
            _context.Set<T>().Remove(t);
            await _context.SaveChangesAsync();
        }

        public async Task<Baslik> GetByNameAsync(string name)
        {
            return await _context.Basliklar.FirstOrDefaultAsync(b => b.Name == name);
        }

      
    }
}
