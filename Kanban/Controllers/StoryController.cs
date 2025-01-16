using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController(IStoryService storyService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Produces<List<Story>>]
        [HttpGet(Name = "GetStory")]
        public async Task<IActionResult> GetStory()
        {
            List<Story> stories = await storyService.GetStories();

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
            Story? newStory = await storyService.CreateStory(story);

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
            Story? existingStory = await storyService.GetStoryById(requestStory.Id);

            if (existingStory != null)
            {
                Story? updatedStory = await storyService.UpdateStory(requestStory);
                if (updatedStory != null)
                {
                    return Ok(updatedStory);
                }
                return BadRequest();
            }

            Story? createdStory = await storyService.CreateStory(requestStory);
            if (createdStory != null)
            {
                return CreatedAtAction(nameof(UpsertStory), new { id = createdStory.Id }, createdStory);
            }
            return BadRequest();
        }
    }
}
