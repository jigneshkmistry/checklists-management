using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using ChecklistsManagement.DTO.Checklists;
using MongoDB.Bson;

namespace ChecklistsManagement.Service
{
    /// <summary>
    /// Interface for checklist service, defining operations related to checklists and checklist items.
    /// Inherits from the base service interface.
    /// </summary>
    public interface IChecklistsService : IServiceBase<Checklists, ObjectId>
    {
        /// <summary>
        /// Publishes or unpublishes a checklist based on the provided status.
        /// </summary>
        /// <param name="id">The unique identifier of the checklist.</param>
        /// <param name="publish">A boolean value indicating whether to publish (true) or unpublish (false) the checklist.</param>
        Task PublishChecklistAsync(ObjectId id, bool publish);

        /// <summary>
        /// Adds a new checklist item to a specified checklist.
        /// </summary>
        /// <param name="id">The unique identifier of the checklist.</param>
        /// <param name="item">The checklist item creation data transfer object.</param>
        /// <returns>The created checklist item as a DTO.</returns>
        Task<ChecklistItemDTO> AddChecklistItem(ObjectId id, ChecklistItemCreationDTO item);

        /// <summary>
        /// Updates an existing checklist item within a checklist.
        /// </summary>
        /// <param name="id">The unique identifier of the checklist.</param>
        /// <param name="itemId">The unique identifier of the checklist item to update.</param>
        /// <param name="checklistItemForUpdateDTO">The DTO containing updated values for the checklist item.</param>
        Task UpdateChecklitem(ObjectId id, ObjectId itemId, ChecklistItemForUpdateDTO checklistItemForUpdateDTO);

        /// <summary>
        /// Deletes a checklist item from a specified checklist.
        /// </summary>
        /// <param name="id">The unique identifier of the checklist.</param>
        /// <param name="itemId">The unique identifier of the checklist item to delete.</param>
        Task DeleteChecklistItemAsync(ObjectId id, ObjectId itemId);
    }
}
