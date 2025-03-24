using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using ChecklistsManagement.DTO.Checklists;
using MongoDB.Bson;

namespace ChecklistsManagement.Service
{
    public interface IChecklistsService : IServiceBase<Checklists, ObjectId>
    {
        
        Task PublishChecklistAsync(ObjectId id, bool publish);

        Task<ChecklistItemDTO> AddChecklistItem(ObjectId id, ChecklistItemCreationDTO item);

        Task DeleteChecklistItemAsync(ObjectId id, ObjectId itemId);
    }
}
