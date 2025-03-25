using ChecklistsManagement.Domain;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChecklistsManagement.Repository
{
    /// <summary>
    /// Repository class for managing checklist-related database operations.
    /// </summary>
    public class ChecklistsRepository : Repository<Checklists, ObjectId>, IChecklistsRepository
    {
        #region PRIVATE VARIABLE

        /// <summary>
        /// Logger instance for logging repository operations.
        /// </summary>
        private readonly ILogger<ChecklistsRepository> _logger;

        /// <summary>
        /// MongoDB collection for storing checklists.
        /// </summary>
        private readonly IMongoCollection<Checklists> _collection;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistsRepository"/> class.
        /// </summary>
        /// <param name="logger">Logger instance for logging repository activities.</param>
        /// <param name="database">MongoDB database instance.</param>
        public ChecklistsRepository(ILogger<ChecklistsRepository> logger,
            IMongoDatabase database) : base(database, "Checklists")
        {
            // Initialize the collection reference
            _collection = database.GetCollection<Checklists>("Checklists");

            // Ensure logger is not null
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region PUBLIC 

        #endregion
    }
}
