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
        Task<bool> Update();
    }
}