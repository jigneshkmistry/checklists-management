using AutoMapper;
using ChecklistsManagement.Domain.IRepository;
using ChecklistsManagement.Util;
using Microsoft.Extensions.Logging;

namespace ChecklistsManagement.Service
{
    public class ServiceBase<TEntity, TKey> : IServiceBase<TEntity, TKey> where TEntity : class
    {
        #region PRIVATE MEMBERS

        private readonly IRepository<TEntity, TKey> _repository;
        protected readonly ILogger<ServiceBase<TEntity, TKey>> _logger;
        protected readonly IMapper _mapper;

        #endregion

        #region CONSTRUCTOR

        public ServiceBase(IRepository<TEntity, TKey> repository,
            ILogger<ServiceBase<TEntity, TKey>> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        public async Task<List<DTO>> GetAllAsync<DTO>()
        {
            List<TEntity> entities = await _repository.GetAllAsync();
            return _mapper.Map<List<DTO>>(entities);
        }

        public async Task<List<DTO>> GetPagedDataAsync<DTO>(int page = 1, int pageSize = 10)
        {
            List<TEntity> entities = await _repository.GetPagedDataAsync(page, pageSize);
            return _mapper.Map<List<DTO>>(entities);
        }

        public async Task<DTO> GetByIdAsync<DTO>(TKey id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);

            if (entity != null)
            {
                return _mapper.Map<DTO>(entity);
                
            }
            else
            {
                throw new CustomException(404, $"Records with Id {id} not found");
            }
        }

        public async Task<TReturnDto> AddAsync<TReturnDto, TCreationDto>(TCreationDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity);

            return _mapper.Map<TReturnDto>(entity);
        }

        public async Task UpdateAsync<DTO>(TKey id, DTO dto)
        {
            TEntity entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new CustomException(404, $"Records with Id {id} not found");
            }

            _mapper.Map(dto, entity);

            this.DoPostProcessing(entity);

            await _repository.UpdateAsync(id,entity);
        }

        public async Task DeleteAsync(TKey id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new CustomException(404, $"Records with Id {id} not found");
            }

            await _repository.DeleteAsync(id);
        }

        public virtual void DoPostProcessing(TEntity entity)
        {
            //DO NOTHING
        }
    }
}
