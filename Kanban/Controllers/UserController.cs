using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Produces<List<User>>]
        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            List<User> users = await userService.GetUsers();

            if (users.Count > 0)
            {
                return Ok(users);
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes("application/json")]
        [HttpPost(Name = "PostUser")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            User? newUser = await userService.PostUser(user);

            if (newUser != null)
            {
                return CreatedAtAction(nameof(PostUser),
                    new { id = newUser.Id }, newUser);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
