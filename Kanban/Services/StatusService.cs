using Kanban.Models;
using Kanban.Repositories;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kanban.Services
{
    public class StatusService(IStatusRepository statusRepository) : IStatusService
    {
        public async Task<List<Status>> GetStatuses()
        {
            List<Status> statuses = await statusRepository.GetAllStatuses();

            return statuses;
        }

        public async Task<Status?> GetStatusById(int id)
        {
            Status? status = await statusRepository.GetStatusById(id);
            return status;
        }

        public async Task<Status?> CreateStatus(Status status)
        {
            bool success = await statusRepository.CreateStatus(status);

            return success ? status : null;
        }

        public async Task<Status?> UpdateStatus(Status status)
        {
            bool success = await statusRepository.UpdateStatus(status);

            if (success)
            {
                return await statusRepository.GetStatusById(status.Id);
            }

            return null;
        }
    }
}
