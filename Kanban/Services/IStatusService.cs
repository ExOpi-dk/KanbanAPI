using Kanban.Models;

namespace Kanban.Services
{
    public interface IStatusService
    {
        Task<List<Status>> GetStatuses();
        Task<Status?> GetStatusById(int id);
        Task<Status?> CreateStatus(Status status);
        Task<Status?> UpdateStatus(Status status);
    }
}