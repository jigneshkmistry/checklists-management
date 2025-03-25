using ChecklistsManagement.Domain.IRepository;
using MongoDB.Bson;

namespace ChecklistsManagement.Domain
{
    /// <summary>
    /// Repository interface for managing checklist entities in the database.
    /// Inherits common CRUD operations from the base IRepository interface.
    /// </summary>
    public interface IChecklistsRepository : IRepository<Checklists, ObjectId>
    {
    }
}
