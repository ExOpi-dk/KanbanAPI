using Kanban.Enums;
using Kanban.Models;

namespace Kanban.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUserById(int id);
        Task<User?> CreateUser(User user);
        Task<User?> UpdateUser(User user);
        Task<DeleteResult> DeleteUser(int id);
    }
}