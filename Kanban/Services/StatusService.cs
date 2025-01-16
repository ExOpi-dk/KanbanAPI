﻿using Kanban.Models;
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

        public async Task<Status?> PostStatus(Status status)
        {
            bool success = await statusRepository.PostStatus(status);

            return success ? status : null;
        }
    }
}