using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecklistsManagement.DTO
{
    public class ChecklistsForUpdateDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

    }
}
