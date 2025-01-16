using Kanban.Models;

namespace Kanban.Repositories
{
    public interface IUserRepository
    {
        Task<bool> DeleteUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<bool> PostUser(User user);
        Task<bool> UpdateUser(User user);
    }
}