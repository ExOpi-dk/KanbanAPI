using Kanban.Enums;
using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController(IService<Status> statusService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Produces<List<Status>>]
        [HttpGet(Name = "GetStatus")]
        public async Task<IActionResult> GetStatus()
        {
            List<Status> statuses = await statusService.GetAll();

            if (statuses.Count > 0)
            {
                return Ok(statuses);
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost(Name = "PostStatus")]
        public async Task<IActionResult> PostStatus([FromBody] Status status)
        {
            Status? newStatus = await statusService.Create(status);

            if (newStatus != null)
            {
                return CreatedAtAction(nameof(PostStatus),
                    new { id = newStatus.Id }, newStatus);
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
        [HttpPatch("{id}", Name = "PatchStatus")]
        public async Task<IActionResult> PatchStatus(int id, [FromBody] JsonPatchDocument<Status> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            OperationResult result = await statusService.Update(id, status =>
            {
                if (status != null)
                {
                    patchDoc.ApplyTo(status);
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
        [HttpDelete(Name = "DeleteStatus")]
        public async Task<IActionResult> DeleteStatus([FromQuery] int id)
        {
            OperationResult result = await statusService.Delete(id);

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
