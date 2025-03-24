using System.ComponentModel.DataAnnotations;

namespace ChecklistsManagement.DTO
{
    public class ChecklistsForCreationDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        public string? Description { get; set; }

    }
}
