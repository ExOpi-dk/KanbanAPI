using Kanban.Models;
using Kanban.Repositories;

namespace Kanban.Services
{
    public class StoryService(IStoryRepository storyRepository) : IStoryService
    {
        public async Task<List<Story>> GetStories()
        {
            List<Story> stories = await storyRepository.GetAllStories();

            return stories;
        }

        public async Task<Story?> PostStory(Story story)
        {
            bool success = await storyRepository.PostStory(story);

            return success ? story : null;
        }
    }
}
