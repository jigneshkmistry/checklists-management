using System.ComponentModel.DataAnnotations;

namespace ChecklistsManagement.DTO
{
    public class ChecklistItemCreationDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
