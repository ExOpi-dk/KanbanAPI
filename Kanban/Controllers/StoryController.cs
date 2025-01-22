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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [Consumes("application/json-patch+json")]
        [HttpPatch("{id}", Name = "PatchStory")]
        public async Task<IActionResult> PatchStory(int id, [FromBody] JsonPatchDocument<Story> patchDoc)
        {
            if (patchDoc == null)
            {
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
