using ChecklistsManagement.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace ChecklistsManagement.Repository
{
    public class ChecklistsRepository : IChecklistsRepository
    {

        #region PRIVATE VARIABLE
        
        private readonly ILogger<ChecklistsRepository> _logger;
        private readonly IMongoCollection<Checklists> _collection;

        #endregion

        #region CONSTRUCTOR

        public ChecklistsRepository(ILogger<ChecklistsRepository> logger,
            IMongoDatabase database) 
        {
            _collection = database.GetCollection<Checklists>("Checklists");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region PUBLIC 

        public async Task<List<Checklists>> GetChecklists()
        {
            _logger.LogInformation("ChecklistsRepository.GetChecklists called:");
            return await _collection.Find(_ => true).ToListAsync();
        }

        #endregion

    }
}
