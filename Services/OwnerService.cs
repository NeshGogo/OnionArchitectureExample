﻿using Services.Abstractions;
using Domain.Repositories;
using Shared;
using Domain.Exceptions;
using Mapster;
using Domain.Entities;

namespace Services
{
    internal sealed class OwnerService : IOwnerService
    {
        private readonly IRepositoryManager _repositoryManager;
        
        public OwnerService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
        
        public async Task<OwnerDto> CreateAsync(OwnerForCreationDto ownerForCreationDto, CancellationToken cancellationToken = default)
        {
            var owner = ownerForCreationDto.Adapt<Owner>();

            _repositoryManager.OwnerRepository.Insert(owner);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            return owner.Adapt<OwnerDto>();
        }

        public async Task DeleteAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);
            
            if (owner is null)
            {
                throw new OwnerNotFoundException(ownerId);
            }

            _repositoryManager.OwnerRepository.Remove(owner);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<OwnerDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var owners = await _repositoryManager.OwnerRepository.GetAllAsync(cancellationToken);
            var ownersDtos = owners.Adapt<IEnumerable<OwnerDto>>();
            return ownersDtos;
        }

        public async Task<OwnerDto> GetByIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);
            
            if(owner is null)
            {
                throw new OwnerNotFoundException(ownerId);
            }
            
            var ownerDto = owner.Adapt<OwnerDto>();
            return ownerDto;
        }

        public async Task UpdateAsync(Guid ownerId, OwnerForUpdateDto ownerForUpdateDto, CancellationToken cancellationToken = default)
        {
            var owner = await _repositoryManager.OwnerRepository.GetByIdAsync(ownerId, cancellationToken);

            if (owner is null)
            {
                throw new OwnerNotFoundException(ownerId);
            }

            owner.Name = ownerForUpdateDto.Name;
            owner.DateOfBirth = ownerForUpdateDto.DateOfBirth;
            owner.Address = ownerForUpdateDto.Address;

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
