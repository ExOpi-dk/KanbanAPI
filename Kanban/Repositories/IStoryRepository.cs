using Kanban.Models;

namespace Kanban.Repositories
{
    public interface IStoryRepository
    {
        Task<bool> DeleteStory(Story story);
        Task<List<Story>> GetAllStories();
        Task<Story?> GetStoryById(int id);
        Task<bool> PostStory(Story story);
        Task<bool> UpdateStory(Story updatedStory);
    }
}