namespace ChecklistsManagement.Domain
{
    public interface IChecklistsRepository
    {
        Task<List<Checklists>> GetChecklists();
    }
}
