using ChecklistsManagement.Domain.IRepository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChecklistsManagement.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {

        private readonly IMongoCollection<TEntity> _collection;

        public Repository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<TEntity>> GetPagedDataAsync(int page = 1, int pageSize = 10)
        {
            return await _collection.Find(_ => true)
                    .Skip((page - 1) * pageSize)
                    .Limit(pageSize).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _collection.Find(Builders<TEntity>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(TKey id, TEntity entity)
        {
           await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), entity);
        }

        public async Task DeleteAsync(TKey id)
        {
            await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }
    }
}
