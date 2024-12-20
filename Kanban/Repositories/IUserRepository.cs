using Kanban.Models;

namespace Kanban.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<bool> PostUser(User user);
        Task<bool> UpdateUser(User oldUser, User newUser);
        Task<bool> DeleteUser(User user);
    }
}
