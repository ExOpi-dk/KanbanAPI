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
            return await s_context.Boards.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Board>> GetAllBoards()
        {
            return await s_context.Boards.AsNoTracking().ToListAsync();
        }

        public async Task<bool> PostBoard(Board board)
        {
            board.Id = default;

            await s_context.Boards.AddAsync(board);
            return await s_context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBoard(Board updatedBoard)
        {
            s_context.Boards.Attach(updatedBoard);
            s_context.Entry(updatedBoard).State = EntityState.Modified;
            return await s_context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBoard(Board board)
        {
            s_context.Boards.Remove(board);
            return await s_context.SaveChangesAsync() > 0;
        }
    }
}
