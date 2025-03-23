using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.Domain
{
    public class Checklists
    {
        public string Id { get; set; } = "";

        public string Title { get; set; } = "0";

        public string? Description { get; set; }
    }
}
