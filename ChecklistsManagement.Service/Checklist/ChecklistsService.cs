using ChecklistsManagement.Domain;
using ChecklistsManagement.DTO;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ChecklistsService> _logger;

        #endregion

        #region CONSTRUCTOR

        public ChecklistsService(ILogger<ChecklistsService> logger, 
            IChecklistsRepository checklistsRepository) 
        {
            _checklistsRepository = checklistsRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region PUBLIC MEMBERS   

        public List<ChecklistsDTO> GetChecklists()
        {
            _logger.LogInformation("ChecklistsService.GetChecklists called:");

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
