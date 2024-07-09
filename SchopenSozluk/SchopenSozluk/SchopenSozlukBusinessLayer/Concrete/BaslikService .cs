using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukEntityLayer.Concrete;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SchopenSozlukBusinessLayer.Concrete
{
    public class BaslikService : IBaslikService
    {
        private readonly IGenericRepository<Baslik> _repository;
        private readonly IBaslikRepository _baslikRepository;

        public BaslikService(IGenericRepository<Baslik> repository, IBaslikRepository baslikRepository)
        {
            _repository = repository;
            _baslikRepository = baslikRepository;
        }

        public async Task<List<Baslik>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Baslik baslik)
        {
            await _repository.InsertAsync(baslik);
        }

        public async Task UpdateAsync(Baslik baslik)
        {
            await _repository.UpdateAsync(baslik);
        }

        public async Task DeleteAsync(Baslik baslik)
        {
            await _repository.DeleteAsync(baslik);
        }
        public async Task<Baslik> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);//dg
        }

        public async Task<Baslik> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<Baslik> GetBaslikByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Baslik>> GetBasliklarByUserIdAsync(int userId)
        {
            return await _baslikRepository.GetBasliklarByUserIdAsync(userId);
        }
        public async Task DeleteBaslikAsync(int baslikId)
        {
            await _baslikRepository.DeleteBaslikAsync(baslikId);
        }

        public async Task<Baslik> GetBaslikByIdAsyncc(int baslikId)
        {
            return await _baslikRepository.GetBaslikByIdAsync(baslikId);
        }
    }
}
