using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Domain.IRepository
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();

        Task<List<TEntity>> GetPagedDataAsync(int page = 1, int pageSize = 10);

        Task<TEntity> GetByIdAsync(TKey id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TKey id, TEntity entity);

        Task DeleteAsync(TKey id);
    }
}
