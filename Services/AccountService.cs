using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;
using Shared;

namespace Services
{
    internal sealed class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repositoryManager;
        public AccountService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager; 
       
        public async Task<AccountDto> CreateAsync(Guid ownerId, AccountForCreationDto accountForCreationDto, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);

            if (owner is null)
            {
                throw new OwnerNotFoundException(ownerId);
            }

            var account = accountForCreationDto.Adapt<Account>();
            
            account.OwnerId = ownerId;
            
            _repositoryManager.AccountRepository.Insert(account);
            
            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return account.Adapt<AccountDto>();
        }

        public async Task DeleteAsync(Guid ownerId, Guid accountId, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);

            if (owner is null)
            {
                throw new OwnerNotFoundException(ownerId);
            }

            var account = await _repositoryManager.AccountRepository.GetByIdAsync(accountId, cancellationToken);

            if (account is null)
            {
                throw new AccountNotFoundException(accountId);
            }

            if (account.OwnerId != owner.Id)
            {
                throw new AccountDoesNotBelongToOwnerException(ownerId, accountId);
            }

            _repositoryManager.AccountRepository.Remove(account);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<AccountDto>> GetAllByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            var accounts = await _repositoryManager.AccountRepository.GetAllByOwnerIdAsync(ownerId, cancellationToken);

            var accountDtos = accounts.Adapt<IEnumerable<AccountDto>>();

            return accountDtos;
        }

        public async Task<AccountDto> GetByIdAsync(Guid ownerId, Guid accountId, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);

            if (owner is null)
            {
                throw new OwnerNotFoundException(ownerId);
            }

            var account = await _repositoryManager.AccountRepository.GetByIdAsync(accountId, cancellationToken);

            if (account is null)
            {
                throw new AccountNotFoundException(accountId);
            }

            if (account.OwnerId != owner.Id)
            {
                throw new AccountDoesNotBelongToOwnerException(ownerId, accountId);
            }

            var accountDto = account.Adapt<AccountDto>();
            return accountDto;
        }
    }
}
