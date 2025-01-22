using Kanban.Enums;
using Kanban.Models;

namespace Kanban.Services
{
    public interface IService<T> where T : Dto
    {
        Task<T?> Create(T dto);
        Task<OperationResult> Delete(int id);
        Task<List<T>> GetAll();
        Task<OperationResult> Update(int id, Action<T?> updateAction);
        Task<T?> GetById(int id);
    }
}