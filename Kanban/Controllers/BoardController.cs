using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardController(IService<Board> boardService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Produces<List<Board>>]
        [HttpGet(Name = "GetBoard")]
        public async Task<IActionResult> GetBoard()
        {
            List<Board> boards = await boardService.GetAll();

            if (boards.Count > 0)
            {
                return Ok(boards);
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost(Name = "PostBoard")]
        public async Task<IActionResult> PostBoard([FromBody] Board board)
        {
            Board? newBoard = await boardService.Create(board);

            if (newBoard != null)
            {
                return CreatedAtAction(nameof(PostBoard),
                    new { id = newBoard.Id }, newBoard);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
