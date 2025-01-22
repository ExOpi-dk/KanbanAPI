using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("User")]
    public class UserController(IService<User> userService) : DtoController<User>(userService)
    {
        [EndpointSummary("Get all users")]
        [EndpointDescription("Returns every user")]
        public override async Task<IActionResult> Get()
        {
            return await base.Get();
        }

        public override async Task<IActionResult> Post([FromBody] User user)
        {
            return await base.Post(user);
        }

        public override async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<User> patchDoc)
        {
            return await base.Patch(id, patchDoc);
        }

        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
