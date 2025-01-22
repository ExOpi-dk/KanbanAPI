using Kanban.Contexts;
using Kanban.Enums;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories
{
    public class Repository<T> : IRepository<T> where T : Dto
    {
        private static readonly KanbanContext s_context = new();

        public async Task<T?> GetById(int id)
        {
            T? dto = await s_context.FindAsync<T>(id);

            return dto;
        }

        public async Task<List<T>> GetAll()
        {
            List<T> dtos = await s_context.Set<T>().AsNoTracking().ToListAsync();

            return dtos;
        }

        public async Task<bool> Create(T dto)
        {
            dto.Id = default;

            await s_context.AddAsync(dto);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Update()
        {
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Delete(T dto)
        {
            s_context.Remove(dto);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }
    }
}
