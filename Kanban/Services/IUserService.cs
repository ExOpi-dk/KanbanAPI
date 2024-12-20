using Kanban.Models;

namespace Kanban.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User?> PostUser(User user);
    }
}
