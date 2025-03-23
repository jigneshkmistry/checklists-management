using ChecklistsManagement.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Repository
{
    public class ChecklistsRepository : IChecklistsRepository
    {

        #region PRIVATE VARIABLE
        
        private readonly ILogger<ChecklistsRepository> _logger;

        #endregion

        #region CONSTRUCTOR

        public ChecklistsRepository(ILogger<ChecklistsRepository> logger) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region PUBLIC 

        public List<Checklists> GetChecklists()
        {
            _logger.LogInformation("ChecklistsRepository.GetChecklists called:");

            return new List<Checklists>() {
                new Checklists() { Description = "Desc 1", Title = "Daily Checklist", Id = "1" },
                new Checklists() { Description = "Desc 2", Title = "Weekli Checklist", Id = "2" }
            };
        }

        #endregion

    }
}
