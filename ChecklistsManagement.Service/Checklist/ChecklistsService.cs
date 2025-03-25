using AutoMapper;
using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using ChecklistsManagement.DTO.Checklists;
using ChecklistsManagement.Util;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace ChecklistsManagement.Service
{
    public class ChecklistsService : ServiceBase<Checklists, ObjectId>, IChecklistsService
    {

        #region PRIVATE MEMBERS

        private readonly IChecklistsRepository _checklistsRepository;
     
        #endregion

        #region CONSTRUCTOR

        public ChecklistsService(ILogger<ChecklistsService> logger,
            IMapper mapper,
            IChecklistsRepository checklistsRepository) : base(checklistsRepository, logger, mapper)
        {
            _checklistsRepository = checklistsRepository;
        }

        #endregion

        #region PUBLIC MEMBERS   

        public async Task<ChecklistItemDTO> AddChecklistItem(ObjectId id,ChecklistItemCreationDTO item)
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
                    throw new CustomException(404, $"Checklistitem with id {itemId} not found");
                }
                checklist.UpdatedAt = DateTime.Now;
                await _checklistsRepository.UpdateAsync(id, checklist);
            }
            else
            {
                throw new CustomException(404, $"Checklist with id {id} not found");
            }
        }

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

        public override void DoPostProcessing(Checklists entity)
        {
            entity.UpdatedAt = DateTime.Now;
        }

        #endregion

    }
}
