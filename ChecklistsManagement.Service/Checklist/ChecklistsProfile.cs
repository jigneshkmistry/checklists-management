using AutoMapper;
using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using ChecklistsManagement.DTO.Checklists;

namespace ChecklistsManagement.Service
{
    /// <summary>
    /// AutoMapper profile for mapping between domain models and DTOs related to checklists.
    /// </summary>
    public class ChecklistsProfile : Profile
    {
        #region CONSTRUCTOR

        /// <summary>
        /// Initializes mapping configurations for checklist-related entities and DTOs.
        /// </summary>
        public ChecklistsProfile()
        {
            // Mapping between Checklist entity and DTOs
            CreateMap<Checklists, ChecklistsDTO>();  // Maps Checklist entity to ChecklistDTO
            CreateMap<ChecklistsForCreationDTO, Checklists>();  // Maps DTO for creating a checklist to entity
            CreateMap<ChecklistsForUpdateDTO, Checklists>();  // Maps DTO for updating a checklist to entity

            // Mapping between ChecklistItem entity and DTOs
            CreateMap<ChecklistItem, ChecklistItemDTO>();  // Maps ChecklistItem entity to ChecklistItemDTO
            CreateMap<ChecklistItemCreationDTO, ChecklistItem>();  // Maps DTO for creating a checklist item to entity
            CreateMap<ChecklistItemForUpdateDTO, ChecklistItem>();  // Maps DTO for updating a checklist item to entity

            // Additional mapping for checklist details
            CreateMap<Checklists, ChecklistDetailsDTO>();  // Maps Checklist entity to ChecklistDetailsDTO
        }

        #endregion
    }
}