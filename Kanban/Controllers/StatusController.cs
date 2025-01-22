using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Status")]
    public class StatusController(IService<Status> statusService) : DtoController<Status>(statusService)
    {
        [EndpointSummary("Get all statuses")]
        [EndpointDescription("Returns every status")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        public override async Task<IActionResult> Post([FromBody] Status status)
        {
            return await base.Post(status);
        }

        public override async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Status> patchDoc)
        {
            return await base.Patch(id, patchDoc);
        }

        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
