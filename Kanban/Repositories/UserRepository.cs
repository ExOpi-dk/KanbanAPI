using Kanban.Contexts;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly KanbanContext s_context = new();

        public async Task<User?> GetUserById(int id)
        {
            User? user = await s_context.FindAsync<User>(id);

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await s_context.Users.ToListAsync();

            return users;
        }

        public async Task<bool> PostUser(User user)
        {
            user.Id = default;

            await s_context.Users.AddAsync(user);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateUser(User oldUser, User newUser)
        {
            s_context.Users.Entry(oldUser).CurrentValues.SetValues(newUser);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteUser(User user)
        {
            s_context.Users.Remove(user);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }
    }
}
