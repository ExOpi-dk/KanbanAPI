using Kanban.Models;
using Kanban.Repositories;

namespace Kanban.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        public async Task<List<User>> GetUsers()
        {
            List<User> users = await userRepository.GetAllUsers();

            return users;
        }

        public async Task<User?> PostUser(User user)
        {
            bool success = await userRepository.PostUser(user);

            return success ? user : null;
        }
    }
}
