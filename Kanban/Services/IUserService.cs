using Kanban.Models;

namespace Kanban.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUserById(int id);
        Task<User?> PostUser(User user);
        Task<User?> UpdateUser(User user);
    }
}