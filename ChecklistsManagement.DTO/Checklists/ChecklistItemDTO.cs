using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChecklistsManagement.DTO.Checklists
{
    public class ChecklistItemDTO
    {
        public string Id { get; set; } = "";
     
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
