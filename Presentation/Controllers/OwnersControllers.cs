using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersControllers : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public OwnersControllers(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetOwners(CancellationToken cancellationToken)
        {
            var ownerDtos = await _serviceManager.OwnerService.GetAllAsync(cancellationToken);
            return Ok(ownerDtos);
        }

        [HttpGet("{ownerId:guid}")]
        public async Task<IActionResult> GetOwnerById(Guid ownerId, CancellationToken cancellationToken)
        {
            var ownerDto = await _serviceManager.OwnerService.GetByIdAsync(ownerId, cancellationToken);
            return Ok(ownerDto);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOwner([FromBody]OwnerForCreationDto ownerForCreationDto, CancellationToken cancellationToken)
        {
            var ownerDto = await _serviceManager.OwnerService.CreateAsync(ownerForCreationDto, cancellationToken);
            return CreatedAtAction(nameof(GetOwnerById), new { ownerId = ownerDto.Id}, ownerDto);
        }

        [HttpPut("{ownerId:guid}")]
        public async Task<IActionResult> UpdateOwner(Guid ownerId, [FromBody] OwnerForUpdateDto ownerForUpdateDto, CancellationToken cancellationToken)
        {
            await _serviceManager.OwnerService.UpdateAsync(ownerId, ownerForUpdateDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{ownerId:guid}")]
        public async Task<IActionResult> DeleteOwner(Guid ownerId, CancellationToken cancellationToken)
        {
            await _serviceManager.OwnerService.DeleteAsync(ownerId, cancellationToken);
            return NoContent();
        }
    }
}
