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
        [Consumes("application/json")]
        [HttpPost(Name = "PostUser")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            User? newUser = await userService.CreateUser(user);

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [HttpPut(Name = "UpsertUser")]
        public async Task<IActionResult> UpsertUser([FromBody] User requestUser)
        {
            User? existingUser = await userService.GetUserById(requestUser.Id);

            if (existingUser != null)
            {
                User? updatedUser = await userService.UpdateUser(requestUser);
                if (updatedUser != null)
                {
                    return Ok(updatedUser);
                }
                return BadRequest();
            }

            User? createdUser = await userService.CreateUser(requestUser);
            if (createdUser != null)
            {
                return CreatedAtAction(nameof(UpsertUser), new { id = createdUser.Id }, createdUser);
            }
            return BadRequest();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            bool? result = await userService.DeleteUser(id);

            if (result != null)
            {
                return (bool)result ? NoContent() : BadRequest();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
