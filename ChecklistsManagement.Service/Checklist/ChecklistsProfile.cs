using AutoMapper;
using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using ChecklistsManagement.DTO.Checklists;

namespace ChecklistsManagement.Service
{
    public class ChecklistsProfile : Profile
    {
        #region CONSTRUCTOR

        public ChecklistsProfile()
        {
            CreateMap<Checklists, ChecklistsDTO>();
            CreateMap<ChecklistsForCreationDTO, Checklists>();
            CreateMap<ChecklistsForUpdateDTO, Checklists>();

            CreateMap<ChecklistItem, ChecklistItemDTO>();
            CreateMap<ChecklistItemCreationDTO, ChecklistItem>();

            CreateMap<Checklists, ChecklistDetailsDTO>();  
        }

        #endregion
    }
}
