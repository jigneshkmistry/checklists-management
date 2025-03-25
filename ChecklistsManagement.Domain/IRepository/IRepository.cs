namespace ChecklistsManagement.Domain.IRepository
{
    /// <summary>
    /// Generic repository interface defining common database operations.
    /// </summary>
    /// <typeparam name="TEntity">The entity type this repository manages.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities from the database.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Retrieves a paginated list of entities.
        /// </summary>
        /// <param name="page">The page number (default is 1).</param>
        /// <param name="pageSize">The number of records per page (default is 10).</param>
        /// <returns>A paginated list of entities.</returns>
        Task<List<TEntity>> GetPagedDataAsync(int page = 1, int pageSize = 10);

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<TEntity> GetByIdAsync(TKey id);

        /// <summary>
        /// Adds a new entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        Task UpdateAsync(TKey id, TEntity entity);

        /// <summary>
        /// Deletes an entity from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        Task DeleteAsync(TKey id);
    }
}
