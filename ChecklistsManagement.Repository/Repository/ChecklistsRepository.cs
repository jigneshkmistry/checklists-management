using ChecklistsManagement.Domain;
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

        #endregion

        #region CONSTRUCTOR

        public ChecklistsRepository() { }

        #endregion

        #region PUBLIC 

        public List<Checklists> GetChecklists()
        {
            return new List<Checklists>() {
                new Checklists() { Description = "Desc 1", Title = "Daily Checklist", Id = "1" },
                new Checklists() { Description = "Desc 2", Title = "Weekli Checklist", Id = "2" }
            };
        }

        #endregion

    }
}
