using Kanban.Models;

namespace Kanban.Services
{
    public interface IStatusService
    {
        Task<List<Status>> GetStatuses();
        Task<Status?> PostStatus(Status status);
    }
}