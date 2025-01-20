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
            User? user = await s_context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            List<User> users = await s_context.Users.AsNoTracking().ToListAsync();

            return users;
        }

        public async Task<bool> CreateUser(User user)
        {
            await s_context.Users.AddAsync(user);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            s_context.Users.Attach(user);
            s_context.Entry(user).State = EntityState.Modified;

            return await s_context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUser(User user)
        {
            s_context.Users.Remove(user);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }
    }
}
