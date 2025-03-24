using ChecklistsManagement.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChecklistsManagement.Repository
{
    public class ChecklistsRepository : Repository<Checklists, ObjectId>, IChecklistsRepository
    {

        #region PRIVATE VARIABLE
        
        private readonly ILogger<ChecklistsRepository> _logger;
        private readonly IMongoCollection<Checklists> _collection;

        #endregion

        #region CONSTRUCTOR

        public ChecklistsRepository(ILogger<ChecklistsRepository> logger,
            IMongoDatabase database) : base(database, "Checklists")
        {
            _collection = database.GetCollection<Checklists>("Checklists");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region PUBLIC 

        #endregion

    }
}
