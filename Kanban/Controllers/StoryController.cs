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
    public class StoryController(IService<Story> storyService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Produces<List<Story>>]
        [HttpGet(Name = "GetStory")]
        public async Task<IActionResult> GetStory()
        {
            List<Story> stories = await storyService.GetAll();

            if (stories.Count > 0)
            {
                return Ok(stories);
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost(Name = "PostStory")]
        public async Task<IActionResult> PostStory([FromBody] Story story)
        {
            Story? newStory = await storyService.Create(story);

            if (newStory != null)
            {
                return CreatedAtAction(nameof(PostStory),
                    new { id = newStory.Id }, newStory);
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
        [HttpPut(Name = "UpsertStory")]
        public async Task<IActionResult> UpsertStory([FromBody] Story requestStory)
        {
            Story? existingStory = await storyService.GetById(requestStory.Id);

            if (existingStory != null)
            {
                Story? updatedStory = await storyService.Update(requestStory);
                if (updatedStory != null)
                {
                    return Ok(updatedStory);
                }
                return BadRequest();
            }

            OperationResult result = await storyService.Update(id, story =>
            {
                if (story != null)
                {
                    try
                    {
                        patchDoc.ApplyTo(story);
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
