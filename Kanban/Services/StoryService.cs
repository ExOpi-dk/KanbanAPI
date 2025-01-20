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

        public async Task<Story?> GetStoryById(int id)
        {
            Story? story = await storyRepository.GetStoryById(id);

            return story;
        }

        public async Task<Story?> CreateStory(Story story)
        {
            bool success = await storyRepository.CreateStory(story);

            return success ? story : null;
        }

        public async Task<Story?> UpdateStory(Story story)
        {
            bool success = await storyRepository.UpdateStory(story);

            if (success) {
                return await storyRepository.GetStoryById(story.Id);
            }

            return null;
        }
    }
}
