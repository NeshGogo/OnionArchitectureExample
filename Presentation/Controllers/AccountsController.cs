using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/owners/{ownerId:guid}/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AccountsController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAccounts(Guid ownerId, CancellationToken cancellationToken)
        {
            var accountsDtos = await _serviceManager.AccountService.GetAllByOwnerIdAsync(ownerId, cancellationToken);
            return Ok(accountsDtos);
        }

        [HttpGet("{accountId:guid}")]
        public async Task<IActionResult> GetAccountById(Guid ownerId, Guid accountId, CancellationToken cancellationToken)
        {
            var accountDto = await _serviceManager.AccountService.GetByIdAsync(ownerId, accountId, cancellationToken);
            return Ok(accountDto);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAccount(Guid ownerId, [FromBody] AccountForCreationDto accountForCreationDto, CancellationToken cancellationToken)
        {
            var accountDto = await _serviceManager.AccountService.CreateAsync(ownerId, accountForCreationDto, cancellationToken);
            return CreatedAtAction(nameof(GetAccountById), new { ownerId = accountDto.OwnerId, accountId = accountDto.Id }, accountDto);
        }


        [HttpDelete("{accountId:guid}")]
        public async Task<IActionResult> DeleteOwner(Guid ownerId, Guid accountId, CancellationToken cancellationToken)
        {
            await _serviceManager.AccountService.DeleteAsync(ownerId, accountId, cancellationToken);
            return NoContent();
        }
    }
}
