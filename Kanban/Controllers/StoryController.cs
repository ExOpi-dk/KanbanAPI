using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Story")]
    public class StoryController(IService<Story> storyService) : DtoController<Story>(storyService)
    {
        [EndpointSummary("Get all user stories")]
        [EndpointDescription("Returns every user story")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        public override async Task<IActionResult> Post([FromBody] Story story)
        {
            return await base.Post(story);
        }

        public override async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Story> patchDoc)
        {
            return await base.Patch(id, patchDoc);
        }

        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
