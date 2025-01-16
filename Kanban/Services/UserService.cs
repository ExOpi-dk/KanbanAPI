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

        public async Task<(User? updatedUser, bool existed)> UpdateUser(User newUser)
        {
            User? existingUser = await userRepository.GetUserById(newUser.Id);
            bool existed = existingUser != null;
            bool success = await userRepository.UpdateUser(newUser);

            return (success ? newUser : null, existed);
        }
    }
}
