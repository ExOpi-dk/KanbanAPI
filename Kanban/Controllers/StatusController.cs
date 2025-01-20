using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController(IStatusService statusService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Produces<List<Status>>]
        [HttpGet(Name = "GetStatus")]
        public async Task<IActionResult> GetStatus()
        {
            List<Status> statuses = await statusService.GetStatuses();

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
            Status? newStatus = await statusService.CreateStatus(status);

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
            Status? existingStatus = await statusService.GetStatusById(requestStatus.Id);

            if (existingStatus != null)
            {
                Status? updatedStory = await statusService.UpdateStatus(requestStatus);
                if (updatedStory != null)
                {
                    return Ok(updatedStory);
                }
                return BadRequest();
            }

            Status? createdStory = await statusService.CreateStatus(requestStatus);
            if (createdStory != null)
            {
                return CreatedAtAction(nameof(UpsertStatus), new { id = createdStory.Id }, createdStory);
            }
            return BadRequest();
        }
    }
}
