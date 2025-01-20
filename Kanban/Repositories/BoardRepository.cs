using Kanban.Contexts;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private static readonly KanbanContext s_context = new();

        public async Task<Board?> GetBoardById(int id)
        {
            return await s_context.Boards.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Board>> GetAllBoards()
        {
            return await s_context.Boards.AsNoTracking().ToListAsync();
        }

        public async Task<bool> CreateBoard(Board board)
        {
            board.Id = default;

            await s_context.Boards.AddAsync(board);
            int result = await s_context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateBoard(Board updatedBoard)
        {
            var existingBoard = await s_context.Boards.FindAsync(updatedBoard.Id);
            if (existingBoard != null)
            {
                s_context.Entry(existingBoard).CurrentValues.SetValues(updatedBoard);
                return await s_context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteBoard(Board board)
        {
            s_context.Boards.Remove(board);
            return await s_context.SaveChangesAsync() > 0;
        }
    }
}
