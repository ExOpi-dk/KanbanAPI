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
            bool success = await userRepository.CreateUser(user);

            return success ? user : null;
        }

        public async Task<User?> UpdateUser(User user)
        {
            bool success = await userRepository.UpdateUser(user);

            return success ? user : null;
        }

        public async Task<bool?> AnonymizeUser(int id)
        {
            User? user = await userRepository.GetUserById(id);

            if (user != null)
            {
                user.Name = string.Empty;
                bool success = await userRepository.UpdateUser(user);
                return success;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> DeleteUser(int id)
        {
            User? user = await userRepository.GetUserById(id);

            if (user != null)
            {
                bool success = await userRepository.DeleteUser(user);
                return success;
            }
            else
            {
                return null;
            }
        }
    }
}
