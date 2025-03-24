using System.ComponentModel.DataAnnotations;

namespace ChecklistsManagement.DTO
{
    public class ChecklistItemForUpdateDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
