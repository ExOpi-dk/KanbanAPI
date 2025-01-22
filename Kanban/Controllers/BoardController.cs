using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Kanban.Enums;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

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
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
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
                    try
                    {
                        patchDoc.ApplyTo(board);
                    }
                    catch (JsonPatchException)
                    {
                        result = OperationResult.Error;
                    }
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
    }
}
