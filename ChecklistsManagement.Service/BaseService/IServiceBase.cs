using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Service
{
    public interface IServiceBase<TEntity, TKey> where TEntity : class
    {
        Task<List<DTO>> GetAllAsync<DTO>();

        Task<List<DTO>> GetPagedDataAsync<DTO>(int page = 1, int pageSize = 10);

        Task<DTO> GetByIdAsync<DTO>(TKey id);

        Task<TReturnDto> AddAsync<TReturnDto, TCreationDto>(TCreationDto entity);

        Task UpdateAsync<DTO>(TKey id, DTO dto);

        Task DeleteAsync(TKey id);

    }
}
