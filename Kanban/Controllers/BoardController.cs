using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Kanban.Enums;

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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json-patch+json")]
        [HttpPatch("{id}", Name = "PatchBoard")]
        public async Task<IActionResult> PatchBoard(int id, [FromBody] JsonPatchDocument<Board> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            OperationResult result = await boardService.Update(id, board =>
            {
                if (board != null)
                {
                    patchDoc.ApplyTo(board);
                }
            });

            switch (result)
            {
                case OperationResult.Success:
                    return NoContent();
                case OperationResult.Error:
                    return BadRequest();
                case OperationResult.NotFound:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(Name = "DeletBoard")]
        public async Task<IActionResult> DeleteBoard([FromQuery] int id)
        {
            OperationResult result = await boardService.Delete(id);

            switch (result)
            {
                case OperationResult.Success:
                    return NoContent();
                case OperationResult.Error:
                    return BadRequest();
                case OperationResult.NotFound:
                    return NotFound();
                default:
                    return BadRequest();
            }
        }
    }
}
