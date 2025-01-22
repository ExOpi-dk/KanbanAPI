using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Board")]
    public class BoardController(IService<Board> boardService) : DtoController<Board>(boardService)
    {
        [EndpointSummary("Get all boards")]
        [EndpointDescription("Returns every user story board")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        public override async Task<IActionResult> Post([FromBody] Board board)
        {
            return await base.Post(board);
        }

        public override async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Board> patchDoc)
        {
            return await base.Patch(id, patchDoc);
        }

        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
