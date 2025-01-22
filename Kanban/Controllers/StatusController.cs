using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [HttpPut(Name = "UpsertStatus")]
        public async Task<IActionResult> UpsertStatus([FromBody] Status requestStatus)
        {
            Status? existingStatus = await statusService.GetById(requestStatus.Id);

            if (existingStatus != null)
            {
                Status? updatedStory = await statusService.Update(requestStatus);
                if (updatedStory != null)
                {
                    return Ok(updatedStory);
                }
                return BadRequest();
            }

            OperationResult result = await statusService.Update(id, status =>
            {
                if (status != null)
                {
                    try
                    {
                        patchDoc.ApplyTo(status);
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
