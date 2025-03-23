using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Domain
{
    public interface IChecklistsRepository
    {
        List<Checklists> GetChecklists();
    }
}
