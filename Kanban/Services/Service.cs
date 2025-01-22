using Kanban.Enums;
using Kanban.Models;
using Kanban.Repositories;

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

        public async Task<T?> Update(T dto)
        {
            bool success = await repository.Update(dto);

            if (success)
            {
                return await repository.GetById(dto.Id);
            }

            updateAction(dto);

            return await repository.Update(dto) ? OperationResult.Success : OperationResult.Error;
        }

        public async Task<DeleteResult> Delete(int id)
        {
            T? dto = await repository.GetById(id);

            if (dto != null)
            {
                bool success = await repository.Delete(dto);
                return success ? DeleteResult.Success : DeleteResult.Error;
            }

            return DeleteResult.NotFound;
        }
    }
}
