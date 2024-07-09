using SchopenSozlukBusinessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Abstract;
using SchopenSozlukDataAccessLayer.Repositories;
using SchopenSozlukDataAccessLayer.Repository;
using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukBusinessLayer.Concrete
{
    public class EntryManager : IEntryService
    {
        private readonly IGenericRepository<Entry> _repository;
        private readonly IUserRepository _userdal;
        private readonly IEntryRepository _entryDal;
        private readonly ILikeRepository _likeRepository;
        private readonly IDislikeRepository _dislikeRepository;


        public EntryManager(IGenericRepository<Entry> repository, IEntryRepository entryDal, IUserRepository userdal , ILikeRepository likeRepository, IDislikeRepository dislikeRepository)
        {
            _repository = repository;
            _entryDal = entryDal;
            _userdal = userdal;
            _likeRepository = likeRepository;
            _dislikeRepository = dislikeRepository;
        }

        public async Task<List<Entry>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Entry> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Entry entry)
        {
            await _repository.InsertAsync(entry);
        }

        public async Task UpdateAsync(Entry entry)
        {
            await _repository.UpdateAsync(entry);
        }

        public async Task DeleteAsync(Entry entry)
        {
            await _repository.DeleteAsync(entry);
        }
        public async Task<int> GetLikeCountAsync(int entryId)
        {
            return await _entryDal.GetLikeCountAsync(entryId);
        }

        public async Task<int> GetDislikeCountAsync(int entryId)
        {
            return await _entryDal.GetDislikeCountAsync(entryId);
        }

        public async Task LikeEntryAsync(int entryId, int userId)
        {
            await _likeRepository.LikeEntryAsync(userId, entryId);
        }

        public async Task DislikeEntryAsync(int entryId, int userId)
        {
            await _dislikeRepository.DislikeEntryAsync(userId, entryId);
        }

        public async Task<List<Entry>> GetEntriesByBaslikIdAsync(int baslikId)
        {
            return await _entryDal.GetEntriesByBaslikIdAsync(baslikId);
        }

        public async Task<string> GetUserNameByUserIdAsync(int userId)
        {
            var user = await _userdal.GetByIdAsync(userId);
            return user != null ? user.UserName : "Bilinmeyen Kullanıcı";
        }
        public async Task<Entry> GetEntryWithCommentsAsync(int entryId)
        {
            return await _entryDal.GetEntryWithCommentsAsync(entryId);
        }
        public async Task<List<Comment>> GetCommentsByEntryIdAsync(int entryId)
        {
            return await _entryDal.GetCommentsByEntryIdAsync(entryId);
        }

        public async Task<List<Comment>> GetCommentsByEntryIdsAsync(List<int> entryIds)
        {
            return await _entryDal.GetCommentsByEntryIdsAsync(entryIds);
        }
        public async Task<Dictionary<int, string>> GetUserNamesByUserIdsAsync(List<int> userIds)
        {
            return await _entryDal.GetUserNamesByUserIdsAsync(userIds);
        }

        public async Task<IEnumerable<Entry>> GetEntriesByUserIdAsync(int userId)
        {
            return await _entryDal.GetEntriesByUserIdAsync(userId);
        }


        public async Task<Entry> GetEntryByIdAsync(int entryId)
        {
            return await _entryDal.GetEntryByIdAsync(entryId);
        }

        public async Task DeleteEntryWithRelatedDataAsync(int entryId)
        {
            await _entryDal.DeleteEntryWithRelatedDataAsync(entryId);
        }
    }
}