using Kanban.Contexts;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private static readonly KanbanContext s_context = new();
        private static readonly Lock s_lock = new();

        public async Task<Status?> GetStatusById(int id)
        {
            Status? status = await s_context.FindAsync<Status>(id);

            return status;
        }

        public async Task<List<Status>> GetAllStatuses()
        {
            List<Status> statuses = await s_context.Statuses.ToListAsync();

            return statuses;
        }

        public async Task<bool> PostStatus(Status status)
        {
            status.Id = default;

            await s_context.Statuses.AddAsync(status);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateStatus(Status oldStatus, Status newStatus)
        {
            lock (s_lock)
            {
                s_context.Statuses.Entry(oldStatus).CurrentValues.SetValues(newStatus);
            }
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteStatus(Status status)
        {
            lock (s_lock)
            {
                s_context.Statuses.Remove(status);
            }
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }
    }
}
