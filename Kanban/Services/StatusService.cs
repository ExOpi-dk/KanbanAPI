using Kanban.Models;
using Kanban.Repositories;

namespace Kanban.Services
{
    public class StatusService(IStatusRepository statusRepository) : IStatusService
    {
        public async Task<List<Status>> GetStatuses()
        {
            List<Status> statuses = await statusRepository.GetAllStatuses();

            return statuses;
        }

        public async Task<Status?> CreateStatus(Status status)
        {
            bool success = await statusRepository.CreateStatus(status);

            return success ? status : null;
        }
    }
}
