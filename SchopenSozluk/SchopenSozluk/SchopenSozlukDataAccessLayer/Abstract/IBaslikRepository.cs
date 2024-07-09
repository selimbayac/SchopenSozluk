using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Abstract
{
    public interface IBaslikRepository :IGenericDal<Baslik>
    {
        Task<List<Baslik>> GetListAsync();
        Task<Baslik> GetByIdAsync(int id);
        Task<IEnumerable<Baslik>> GetBasliklarByUserIdAsync(int userId);
        Task DeleteBaslikAsync(int baslikId);
        Task<Baslik> GetBaslikByIdAsync(int baslikId);
    }
}
