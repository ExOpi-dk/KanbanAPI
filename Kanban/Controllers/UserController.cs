using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;
using Kanban.Enums;

namespace Kanban.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IService<User> userService) : ControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Produces<List<User>>]
        [HttpGet(Name = "GetUser")]
        public async Task<IActionResult> GetUser()
        {
            List<User> users = await userService.GetAll();

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
            User? newUser = await userService.Create(user);

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

        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Consumes("application/json")]
        //[HttpPut(Name = "UpsertUser")]
        //public async Task<IActionResult> UpsertUser([FromBody] User requestUser)
        //{
        //    User? existingUser = await userService.GetById(requestUser.Id);

        //    if (existingUser != null)
        //    {
        //        User? updatedUser = await userService.Update(requestUser);
        //        if (updatedUser != null)
        //        {
        //            return Ok(updatedUser);
        //        }
        //        return BadRequest();
        //    }

        //    User? createdUser = await userService.Create(requestUser);
        //    if (createdUser != null)
        //    {
        //        return CreatedAtAction(nameof(UpsertUser), new { id = createdUser.Id }, createdUser);
        //    }
        //    return BadRequest();
        //}

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete(Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            OperationResult result = await userService.Delete(id);

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
