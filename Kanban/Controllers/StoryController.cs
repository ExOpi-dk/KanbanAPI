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
        [HttpPost(Name = "PostStory")]
        public async Task<IActionResult> PostStory([FromBody] Story story)
        {
            Story? newStory = await storyService.PostStory(story);

            if (newStory != null)
            {
                return CreatedAtAction(nameof(PostStory),
                    new { id = newStory.StoryId }, newStory);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
