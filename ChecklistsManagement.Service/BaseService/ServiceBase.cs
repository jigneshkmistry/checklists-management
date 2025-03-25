using AutoMapper;
using ChecklistsManagement.Domain.IRepository;
using ChecklistsManagement.Util;
using Microsoft.Extensions.Logging;

namespace ChecklistsManagement.Service
{
    /// <summary>
    /// Base service class providing common CRUD operations for entities.
    /// </summary>
    public class ServiceBase<TEntity, TKey> : IServiceBase<TEntity, TKey> where TEntity : class
    {
        #region PRIVATE MEMBERS

        private readonly IRepository<TEntity, TKey> _repository;
        protected readonly ILogger<ServiceBase<TEntity, TKey>> _logger;
        protected readonly IMapper _mapper;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBase{TEntity, TKey}"/> class.
        /// </summary>
        public ServiceBase(IRepository<TEntity, TKey> repository,
            ILogger<ServiceBase<TEntity, TKey>> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// Retrieves all records and maps them to the specified DTO type.
        /// </summary>
        public async Task<List<DTO>> GetAllAsync<DTO>()
        {
            List<TEntity> entities = await _repository.GetAllAsync();
            return _mapper.Map<List<DTO>>(entities);
        }

        /// <summary>
        /// Retrieves paged records and maps them to the specified DTO type.
        /// </summary>
        public async Task<List<DTO>> GetPagedDataAsync<DTO>(int page = 1, int pageSize = 10)
        {
            List<TEntity> entities = await _repository.GetPagedDataAsync(page, pageSize);
            return _mapper.Map<List<DTO>>(entities);
        }

        /// <summary>
        /// Retrieves a record by ID and maps it to the specified DTO type.
        /// Throws a custom exception if the record is not found.
        /// </summary>
        public async Task<DTO> GetByIdAsync<DTO>(TKey id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);

            if (entity != null)
            {
                return _mapper.Map<DTO>(entity);
            }
            else
            {
                throw new CustomException(404, $"Record with Id {id} not found");
            }
        }

        /// <summary>
        /// Adds a new record using the specified DTO and returns the created entity mapped to a return DTO.
        /// </summary>
        public async Task<TReturnDto> AddAsync<TReturnDto, TCreationDto>(TCreationDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity);

            return _mapper.Map<TReturnDto>(entity);
        }

        /// <summary>
        /// Updates an existing record by ID using the specified DTO.
        /// Throws a custom exception if the record is not found.
        /// </summary>
        public async Task UpdateAsync<DTO>(TKey id, DTO dto)
        {
            TEntity entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new CustomException(404, $"Record with Id {id} not found");
            }

            _mapper.Map(dto, entity);

            this.DoPostProcessing(entity);

            await _repository.UpdateAsync(id, entity);
        }

        /// <summary>
        /// Deletes a record by ID.
        /// Throws a custom exception if the record is not found.
        /// </summary>
        public async Task DeleteAsync(TKey id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new CustomException(404, $"Record with Id {id} not found");
            }

            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Performs any additional processing after updating an entity. Can be overridden by derived classes.
        /// </summary>
        public virtual void DoPostProcessing(TEntity entity)
        {
            // Default implementation does nothing. Derived classes can override this method.
        }
    }
}
