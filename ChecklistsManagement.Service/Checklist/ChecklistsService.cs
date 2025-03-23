using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Service
{
    public class ChecklistsService : IChecklistsService
    {

        #region PRIVATE MEMBERS

        private readonly IChecklistsRepository _checklistsRepository;

        #endregion

        #region CONSTRUCTOR

        public ChecklistsService(IChecklistsRepository checklistsRepository) 
        {
            _checklistsRepository = checklistsRepository;
        }

        #endregion

        #region PUBLIC MEMBERS   

        public List<ChecklistsDTO> GetChecklists()
        {
            List<ChecklistsDTO> retVal = new List<ChecklistsDTO>();
            List<Checklists> checklists = _checklistsRepository.GetChecklists();

            checklists.ForEach(checklist =>
            {
                retVal.Add(new ChecklistsDTO() { Description = checklist.Description, Title = checklist.Title, Id = checklist.Id });
            });

            return retVal;
        }

        #endregion

        #region VIRTUAL METHODS


        #endregion

    }
}
