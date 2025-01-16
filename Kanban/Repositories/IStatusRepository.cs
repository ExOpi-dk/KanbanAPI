using Kanban.Models;

namespace Kanban.Repositories
{
    public interface IStatusRepository
    {
        Task<bool> DeleteStatus(Status status);
        Task<List<Status>> GetAllStatuses();
        Task<Status?> GetStatusById(int id);
        Task<bool> CreateStatus(Status status);
        Task<bool> UpdateStatus(Status updatedStatus);
    }
}