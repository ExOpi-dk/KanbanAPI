using Kanban.Enums;
using Kanban.Models;

namespace Kanban.Repositories
{
    public interface IRepository<T> where T : Dto
    {
        Task<bool> Create(T dto);
        Task<bool> Delete(T dto);
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<OperationResult> Update(int id, Action<T?> updateAction);
    }
}