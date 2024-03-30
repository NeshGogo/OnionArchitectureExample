using Shared;

namespace Services.Abstractions
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
        Task<AccountDto> GetByIdAsync(Guid ownerId, Guid accountId, CancellationToken cancellationToken = default);
        Task<AccountDto> CreateAsync(Guid ownerId, AccountForCreationDto accountForCreationDto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid ownerId, Guid accountId, CancellationToken cancellationToken = default);
    }
}
