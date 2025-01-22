using Kanban.Models;
using Kanban.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Kanban.Enums;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

namespace Kanban.Controllers
{
    public abstract class DtoController<T>(IService<T> service) : ControllerBase where T : Dto
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            List<T> dtos = await service.GetAll();

            if (dtos.Count > 0)
            {
                return Ok(dtos);
            }

            return NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [HttpPost]
        public virtual async Task<IActionResult> Post(T dto)
        {
            T? newDto = await service.Create(dto);

            if (newDto != null)
            {
                return CreatedAtAction(nameof(Post),
                    new { id = newDto.Id }, newDto);
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
        [HttpPatch("{id}")]
        public virtual async Task<IActionResult> Patch(int id, JsonPatchDocument<T> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            OperationResult result;
            try
            {
                result = await service.Update(id, dto =>
                {
                    if (dto != null)
                    {
                        patchDoc.ApplyTo(dto);
                    }
                });
            }
            catch (JsonPatchException)
            {
                return BadRequest();
            }

            return result switch
            {
                OperationResult.Success => NoContent(),
                OperationResult.Error => BadRequest(),
                OperationResult.NotFound => NotFound(),
                _ => BadRequest()
            };
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            OperationResult result = await service.Delete(id);

            return result switch
            {
                OperationResult.Success => NoContent(),
                OperationResult.Error => BadRequest(),
                OperationResult.NotFound => NotFound(),
                _ => BadRequest()
            };
        }
    }
}
