using Kanban.Models;

namespace Kanban.Services
{
    public interface IStoryService
    {
        Task<List<Story>> GetStories();
        Task<Story?> GetStoryById(int id);
        Task<Story?> CreateStory(Story story);
        Task<Story?> UpdateStory(Story story);
    }
}
