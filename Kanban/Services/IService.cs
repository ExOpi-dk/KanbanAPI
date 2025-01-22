using Kanban.Enums;
using Kanban.Models;

namespace Kanban.Services
{
    public interface IService<T> where T : Dto
    {
        Task<T?> Create(T dto);
        Task<DeleteResult> Delete(int id);
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task<T?> Update(T dto);
    }
}