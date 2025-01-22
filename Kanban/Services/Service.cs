using Kanban.Enums;
using Kanban.Models;
using Kanban.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Services
{
    public class Service<T>(IRepository<T> repository) : IService<T> where T : Dto
    {
        public async Task<List<T>> GetAll()
        {
            List<T> dtos = await repository.GetAll();

            return dtos;
        }

        public async Task<T?> GetById(int id)
        {
            T? story = await repository.GetById(id);

            return story;
        }

        public async Task<T?> Create(T dto)
        {
            bool success = await repository.Create(dto);

            return success ? dto : null;
        }

        public async Task<OperationResult> Update(int id, Action<T?> updateAction)
        {
            return await repository.Update(id, updateAction);
        }

        public async Task<OperationResult> Delete(int id)
        {
            T? dto = await repository.GetById(id);

            if (dto != null)
            {
                bool success = await repository.Delete(dto);
                return success ? OperationResult.Success : OperationResult.Error;
            }

            return OperationResult.NotFound;
        }
    }
}
