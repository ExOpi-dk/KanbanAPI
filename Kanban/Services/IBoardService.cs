using Kanban.Models;

namespace Kanban.Services
{
    public interface IBoardService
    {
        Task<List<Board>> GetBoards();
        Task<Board?> CreateBoard(Board board);
    }
}