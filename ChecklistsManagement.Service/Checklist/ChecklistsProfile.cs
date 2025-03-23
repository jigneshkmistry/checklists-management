using AutoMapper;
using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;

namespace ChecklistsManagement.Service
{
    public class ChecklistsProfile : Profile
    {
        #region CONSTRUCTOR

        public ChecklistsProfile()
        {
            CreateMap<Checklists, ChecklistsDTO>();
        }

        #endregion
    }
}
