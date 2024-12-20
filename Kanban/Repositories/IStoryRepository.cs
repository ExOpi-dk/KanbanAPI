using Kanban.Models;

namespace Kanban.Repositories
{
    public interface IStoryRepository
    {
        Task<Story?> GetStoryById(int id);
        Task<List<Story>> GetAllStories();
        Task<bool> PostStory(Story story);
        Task<bool> UpdateStory(Story oldStory, Story newStory);
        Task<bool> DeleteStory(Story story);
    }
}
