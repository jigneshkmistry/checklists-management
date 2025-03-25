using ChecklistsManagement.Domain.IRepository;
using MongoDB.Driver;

namespace ChecklistsManagement.Repository
{
    /// <summary>
    /// Generic repository class for MongoDB operations.
    /// </summary>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// MongoDB collection for the specified entity type.
        /// </summary>
        private readonly IMongoCollection<TEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="database">MongoDB database instance.</param>
        /// <param name="collectionName">The name of the MongoDB collection.</param>
        public Repository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        /// <summary>
        /// Retrieves all entities from the collection.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Retrieves paginated data from the collection.
        /// </summary>
        /// <param name="page">The page number (default is 1).</param>
        /// <param name="pageSize">The number of records per page (default is 10).</param>
        /// <returns>A list of paginated entities.</returns>
        public async Task<List<TEntity>> GetPagedDataAsync(int page = 1, int pageSize = 10)
        {
            return await _collection.Find(_ => true)
                    .Skip((page - 1) * pageSize)
                    .Limit(pageSize).ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _collection.Find(Builders<TEntity>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds a new entity to the collection.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public async Task AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Updates an existing entity in the collection.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="entity">The updated entity data.</param>
        public async Task UpdateAsync(TKey id, TEntity entity)
        {
            await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", id), entity);
        }

        /// <summary>
        /// Deletes an entity from the collection by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        public async Task DeleteAsync(TKey id)
        {
            await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }
    }
}
