using Kanban.Models;

namespace Kanban.Services
{
    public interface IStoryService
    {
        Task<List<Story>> GetStories();
        Task<Story?> PostStory(Story story);
    }
}
