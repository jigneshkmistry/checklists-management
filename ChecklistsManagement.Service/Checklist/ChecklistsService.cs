using AutoMapper;
using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using ChecklistsManagement.DTO.Checklists;
using ChecklistsManagement.Util;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace ChecklistsManagement.Service
{
    /// <summary>
    /// Service class for managing checklists and their items.
    /// </summary>
    public class ChecklistsService : ServiceBase<Checklists, ObjectId>, IChecklistsService
    {
        #region PRIVATE MEMBERS

        private readonly IChecklistsRepository _checklistsRepository;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistsService"/> class.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="mapper">AutoMapper instance.</param>
        /// <param name="checklistsRepository">Checklist repository instance.</param>
        public ChecklistsService(ILogger<ChecklistsService> logger,
            IMapper mapper,
            IChecklistsRepository checklistsRepository) : base(checklistsRepository, logger, mapper)
        {
            _checklistsRepository = checklistsRepository;
        }

        #endregion

        #region PUBLIC MEMBERS   

        /// <summary>
        /// Adds an item to the specified checklist.
        /// </summary>
        /// <param name="id">Checklist ID.</param>
        /// <param name="item">Checklist item creation DTO.</param>
        /// <returns>Returns the added checklist item.</returns>
        /// <exception cref="CustomException">Throws if the checklist is not found.</exception>
        public async Task<ChecklistItemDTO> AddChecklistItem(ObjectId id, ChecklistItemCreationDTO item)
        {
            var checklist = await _checklistsRepository.GetByIdAsync(id);

            if (checklist != null)
            {
                ChecklistItem entity = _mapper.Map<ChecklistItem>(item);
                checklist.Items.Add(entity);
                await _checklistsRepository.UpdateAsync(id, checklist);

                return _mapper.Map<ChecklistItemDTO>(entity);
            }
            else
            {
                throw new CustomException(404, "Checklist with id not found");
            }
        }

        /// <summary>
        /// Deletes a checklist item from a specified checklist.
        /// </summary>
        /// <param name="id">Checklist ID.</param>
        /// <param name="itemId">Checklist item ID.</param>
        /// <exception cref="CustomException">Throws if the checklist is not found.</exception>
        public async Task DeleteChecklistItemAsync(ObjectId id, ObjectId itemId)
        {
            var checklist = await _checklistsRepository.GetByIdAsync(id);

            if (checklist != null)
            {
                checklist.Items.RemoveAll(i => i.Id == itemId.ToString());
                await _checklistsRepository.UpdateAsync(id, checklist);
            }
            else
            {
                throw new CustomException(404, "Checklist with id not found");
            }
        }

        /// <summary>
        /// Updates an existing checklist item.
        /// </summary>
        /// <param name="id">Checklist ID.</param>
        /// <param name="itemId">Checklist item ID.</param>
        /// <param name="checklistItemForUpdateDTO">DTO containing updated values.</param>
        /// <exception cref="CustomException">Throws if the checklist or checklist item is not found.</exception>
        public async Task UpdateChecklitem(ObjectId id, ObjectId itemId, ChecklistItemForUpdateDTO checklistItemForUpdateDTO)
        {
            var checklist = await _checklistsRepository.GetByIdAsync(id);

            if (checklist != null)
            {
                var checklistItem = checklist.Items.Find(i => i.Id == itemId.ToString());
                if (checklistItem != null)
                {
                    _mapper.Map(checklistItemForUpdateDTO, checklistItem);
                }
                else
                {
                    throw new CustomException(404, $"Checklist item with id {itemId} not found");
                }
                checklist.UpdatedAt = DateTime.Now;
                await _checklistsRepository.UpdateAsync(id, checklist);
            }
            else
            {
                throw new CustomException(404, $"Checklist with id {id} not found");
            }
        }

        /// <summary>
        /// Publishes or unpublishes a checklist.
        /// </summary>
        /// <param name="id">Checklist ID.</param>
        /// <param name="publish">Boolean flag to publish (true) or unpublish (false).</param>
        /// <exception cref="CustomException">Throws if the checklist is not found.</exception>
        public async Task PublishChecklistAsync(ObjectId id, bool publish)
        {
            var checklist = await _checklistsRepository.GetByIdAsync(id);

            if (checklist != null)
            {
                checklist.Status = publish ? ChecklistStatus.Published : ChecklistStatus.Unpublished;
                checklist.UpdatedAt = DateTime.Now;
                await _checklistsRepository.UpdateAsync(id, checklist);
            }
            else
            {
                throw new CustomException(404, "Checklist with id not found");
            }
        }

        #endregion

        #region VIRTUAL METHODS

        /// <summary>
        /// Performs post-processing actions after checklist updates.
        /// </summary>
        /// <param name="entity">Checklist entity.</param>
        public override void DoPostProcessing(Checklists entity)
        {
            entity.UpdatedAt = DateTime.Now;
        }

        #endregion
    }
}
