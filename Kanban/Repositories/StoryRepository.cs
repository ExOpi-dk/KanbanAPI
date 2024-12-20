using Kanban.Contexts;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private static readonly KanbanContext s_context = new();
        private static readonly Lock s_lock = new();

        public async Task<Story?> GetStoryById(int id)
        {
            Story? story = await s_context.FindAsync<Story>(id);

            return story;
        }

        public async Task<List<Story>> GetAllStories()
        {
            List<Story> stories = await s_context.Stories.ToListAsync();

            return stories;
        }

        public async Task<bool> PostStory(Story story)
        {
            await s_context.Stories.AddAsync(story);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateStory(Story oldStory, Story newStory)
        {
            lock (s_lock)
            {
                s_context.Stories.Entry(oldStory).CurrentValues.SetValues(newStory);
            }
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteStory(Story story)
        {
            lock (s_lock)
            {
                s_context.Stories.Remove(story);
            }
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }
    }
}
