using Kanban.Models;

namespace Kanban.Repositories
{
    public interface IBoardRepository
    {
        Task<bool> DeleteBoard(Board board);
        Task<List<Board>> GetAllBoards();
        Task<Board?> GetBoardById(int id);
        Task<bool> PostBoard(Board board);
        Task<bool> UpdateBoard(Board updatedBoard);
    }
}