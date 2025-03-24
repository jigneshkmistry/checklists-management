using ChecklistsManagement.DTO.Checklists;

namespace ChecklistsManagement.DTO
{
    public class ChecklistDetailsDTO
    {
        public string Id { get; set; } = "";

        public string Title { get; set; } = "";

        public string? Description { get; set; }

        public List<ChecklistItemDTO> Items { get; set; } = new();

        public int Status { get; set; } = 0;

    }
}
