using Kanban.Contexts;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private static readonly KanbanContext s_context = new();

        public async Task<Status?> GetStatusById(int id)
        {
            Status? status = await s_context.Statuses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            return status;
        }

        public async Task<List<Status>> GetAllStatuses()
        {
            List<Status> statuses = await s_context.Statuses.AsNoTracking().ToListAsync();

            return statuses;
        }

        public async Task<bool> CreateStatus(Status status)
        {
            status.Id = default;

            await s_context.Statuses.AddAsync(status);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateStatus(Status updatedStatus)
        {
            var existingStatus = await s_context.Statuses.FindAsync(updatedStatus.Id);
            if (existingStatus != null)
            {
                s_context.Entry(existingStatus).CurrentValues.SetValues(updatedStatus);
                return await s_context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteStatus(Status status)
        {
            s_context.Statuses.Remove(status);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }
    }
}
