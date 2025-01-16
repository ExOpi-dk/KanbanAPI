using Kanban.Contexts;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories
{
    public class StoryRepository : IStoryRepository
    {
        private static readonly KanbanContext s_context = new();

        public async Task<Story?> GetStoryById(int id)
        {
            Story? story = await s_context.Stories.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            return story;
        }

        public async Task<List<Story>> GetAllStories()
        {
            List<Story> stories = await s_context.Stories.AsNoTracking().ToListAsync();

            return stories;
        }

        public async Task<bool> PostStory(Story story)
        {
            story.Id = default;

            await s_context.Stories.AddAsync(story);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateStory(Story updatedStory)
        {
            s_context.Stories.Attach(updatedStory);
            s_context.Entry(updatedStory).State = EntityState.Modified;
            return await s_context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStory(Story story)
        {
            s_context.Stories.Remove(story);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }
    }
}
