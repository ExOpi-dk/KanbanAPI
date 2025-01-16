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

        public async Task<User?> GetUserById(int id)
        {
            User? user = await userRepository.GetUserById(id);

            return user;
        }

        public async Task<User?> CreateUser(User user)
        {
            bool success = await userRepository.PostUser(user);

            return success ? user : null;
        }

        public async Task<User?> UpdateUser(User user)
        {
            bool success = await userRepository.UpdateUser(user);

            return success ? user : null;
        }
    }
}
