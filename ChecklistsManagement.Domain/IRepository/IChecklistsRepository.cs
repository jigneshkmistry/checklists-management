using ChecklistsManagement.Domain.IRepository;
using MongoDB.Bson;

namespace ChecklistsManagement.Domain
{
    public interface IChecklistsRepository : IRepository<Checklists, ObjectId>
    {
    }
}
