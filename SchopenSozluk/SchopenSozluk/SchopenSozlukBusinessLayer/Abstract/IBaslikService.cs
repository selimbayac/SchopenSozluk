using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukBusinessLayer.Abstract
{
    public interface IBaslikService
    {
        Task<List<Baslik>> GetAllAsync();
        Task<Baslik> GetByIdAsync(int id);
        Task AddAsync(Baslik baslik);
        Task UpdateAsync(Baslik baslik);
        Task DeleteAsync(Baslik baslik);
     //   Task GetByIdAsync(string name);
        Task<Baslik> GetByNameAsync(string name);

        Task<IEnumerable<Baslik>> GetBasliklarByUserIdAsync(int userId);

        Task DeleteBaslikAsync(int baslikId);
        Task<Baslik> GetBaslikByIdAsync(int baslikId);
    }
}
