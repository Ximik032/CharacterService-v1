using CharacterService.Application.DTOs.Character.Requests;
using CharacterService.Application.DTOs.Character.Responses;
using CharacterService.Application.DTOs.Paginations;
using CharacterService.Application.Mappers;
using CharacterService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CharacterService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CharacterResponse>> GetById(Guid id,CancellationToken cancellationToken)
        {
            var result = await _characterService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<CharacterResponse>>> GetAll([FromQuery] PaginationRequest request, CancellationToken cancellationToken)
        {
            var paginationResponse = await _characterService.GetAllAsync(request,cancellationToken);

            return Ok(paginationResponse);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<PaginatedResponse<CharacterResponse>>> GetAllByUserId(Guid userId, [FromQuery]  PaginationRequest request,CancellationToken cancellationToken)
        {
            var response = await _characterService.GetAllByUserIdAsync(userId, request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            await _characterService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpDelete("user/{userId:guid}")]
        public async Task<ActionResult> DeleteByUserId(Guid userId,CancellationToken cancellationToken)
        {
            await _characterService.DeleteByUserIdAsync(userId, cancellationToken);
            return NoContent();
        }

        [HttpPost("{userId:guid}")]
        public async Task<ActionResult<CharacterResponse>> Create(Guid userId, [FromBody]CreateCharacterRequest request,CancellationToken cancellationToken)
        {
            var result = await _characterService.CreateAsync(userId,request, cancellationToken);

            return CreatedAtAction(nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<CharacterResponse>> Update(Guid id, [FromBody]UpdateCharacterRequest request,CancellationToken cancellationToken)
        {
            var result = await _characterService.UpdateAsync(id, request, cancellationToken);

            return Ok(result);
        }
        


    }
}
