using Kanban.Models;
using Kanban.Repositories;

namespace Kanban.Services
{
    public class BoardService(IBoardRepository boardRepository) : IBoardService
    {
        public async Task<List<Board>> GetBoards()
        {
            List<Board> boards = await boardRepository.GetAllBoards();

            return boards;
        }

        public async Task<Board?> PostBoard(Board board)
        {
            bool success = await boardRepository.CreateBoard(board);

            return success ? board : null;
        }
    }
}
