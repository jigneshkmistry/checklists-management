namespace ChecklistsManagement.Service
{
    /// <summary>
    /// Generic service interface defining common CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity">The entity type this service operates on.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public interface IServiceBase<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities and maps them to the specified DTO type.
        /// </summary>
        /// <typeparam name="DTO">The DTO type to map the entities to.</typeparam>
        /// <returns>A list of DTOs representing all entities.</returns>
        Task<List<DTO>> GetAllAsync<DTO>();

        /// <summary>
        /// Retrieves a paginated list of entities and maps them to the specified DTO type.
        /// </summary>
        /// <typeparam name="DTO">The DTO type to map the entities to.</typeparam>
        /// <param name="page">The page number (default is 1).</param>
        /// <param name="pageSize">The number of records per page (default is 10).</param>
        /// <returns>A paginated list of DTOs.</returns>
        Task<List<DTO>> GetPagedDataAsync<DTO>(int page = 1, int pageSize = 10);

        /// <summary>
        /// Retrieves an entity by its unique identifier and maps it to the specified DTO type.
        /// </summary>
        /// <typeparam name="DTO">The DTO type to map the entity to.</typeparam>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity mapped to a DTO.</returns>
        Task<DTO> GetByIdAsync<DTO>(TKey id);

        /// <summary>
        /// Adds a new entity to the database and returns the newly created entity mapped to a DTO.
        /// </summary>
        /// <typeparam name="TReturnDto">The DTO type returned after creation.</typeparam>
        /// <typeparam name="TCreationDto">The DTO type containing creation data.</typeparam>
        /// <param name="entity">The DTO containing the data for the new entity.</param>
        /// <returns>The created entity mapped to a DTO.</returns>
        Task<TReturnDto> AddAsync<TReturnDto, TCreationDto>(TCreationDto entity);

        /// <summary>
        /// Updates an existing entity using data from a DTO.
        /// </summary>
        /// <typeparam name="DTO">The DTO type containing update data.</typeparam>
        /// <param name="id">The unique identifier of the entity to update.</param>
        /// <param name="dto">The DTO containing the updated values.</param>
        Task UpdateAsync<DTO>(TKey id, DTO dto);

        /// <summary>
        /// Deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        Task DeleteAsync(TKey id);
    }
}
