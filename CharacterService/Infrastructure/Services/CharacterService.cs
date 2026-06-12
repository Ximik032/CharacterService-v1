using CharacterService.Application.DTOs.Character.Requests;
using CharacterService.Application.DTOs.Character.Responses;
using CharacterService.Application.DTOs.Paginations;
using CharacterService.Application.Interfaces;
using CharacterService.Application.Mappers;
using CharacterService.Application.Services;
using CharacterService.Domain.Entities;
using CharacterService.Domain.Exceptions;
using System.Text.Json;

namespace CharacterService.Infrastructure.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _repository;
        private readonly ILogger<CharacterService> _logger;

        public CharacterService(ICharacterRepository repository, ILogger<CharacterService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CharacterResponse> CreateAsync(Guid userId, CreateCharacterRequest createCharacterRequest, CancellationToken cancellationToken)
        {
            var result = await _repository.AddAsync(CharacterMapper.ToEntity(userId, createCharacterRequest), cancellationToken);
            _logger.LogInformation("Character with id {id} added", result.Id);
            return CharacterMapper.ToResponse(result);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteAsync(
                id,
                cancellationToken);

            if (!deleted)
            {
                _logger.LogWarning(
                    "Character with id {id} not found",
                    id);

                throw new NotFoundException(
                    "Character with id {id} not found", id);
            }

            _logger.LogInformation(
                "Character with id {id} deleted",
                id);
        }

        public async Task<int> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var deletedRows = await _repository.DeleteByUserIdAsync(userId, cancellationToken);
            _logger.LogInformation("Deleted rows {deletedRows} by user id {id}", deletedRows, userId);
            return deletedRows;
        }

        public async Task<PaginatedResponse<CharacterResponse>> GetAllAsync(PaginationRequest pagination, CancellationToken cancellationToken)
        {
            var characters = await _repository.GetAllAsync(pagination.Page, pagination.PageSize, cancellationToken);
            var totalCount = await _repository.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pagination.PageSize);

            _logger.LogInformation("Characters page{page}, pageSize {pageSize}, totalCount {totalCount}, totalPages {totalPages}", pagination.Page, pagination.PageSize, totalCount, totalPages);

            return PaginationMapper.ToResponse(characters, pagination.Page, pagination.PageSize, totalCount, totalPages);
        }

        public async Task<PaginatedResponse<CharacterResponse>> GetAllByUserIdAsync(Guid userId, PaginationRequest pagination, CancellationToken cancellationToken)
        {
            var characters = await _repository.GetByUserIdAsync(userId, pagination.Page, pagination.PageSize, cancellationToken);
            var userTotalCount = await _repository.CountByUserIdAsync(userId, cancellationToken);
            var totalPages = (int)Math.Ceiling(userTotalCount / (double)pagination.PageSize);

            _logger.LogInformation("Characters page{page}, pageSize {pageSize}, totalCount {totalCount}, totalPages {totalPages}", pagination.Page, pagination.PageSize, userTotalCount, totalPages);

            return PaginationMapper.ToResponse(characters, pagination.Page, pagination.PageSize, userTotalCount, totalPages);

        }

        public async Task<CharacterResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var character = await GetCharacterByIdCore(id,cancellationToken);

            _logger.LogInformation(
                "Found character with id {id}",
                id);

            return CharacterMapper.ToResponse(character);

        }

        public async Task<CharacterResponse> UpdateAsync(Guid id, UpdateCharacterRequest updateCharacterRequest, CancellationToken cancellationToken)
        {
            var character = await GetTrackedCharacterByIdAsync(id, cancellationToken);
            if (updateCharacterRequest.Name is not null)
            {
                character.Name = updateCharacterRequest.Name;
            }
            if (updateCharacterRequest.Description is not null)
            {
                character.Description = updateCharacterRequest.Description;
            }
            if(updateCharacterRequest.SystemPrompt is not null)
            {
                character.SystemPrompt = updateCharacterRequest.SystemPrompt;
            }

            if (updateCharacterRequest.Background is not null)
            {
                character.Background = updateCharacterRequest.Background;
            }
            if(updateCharacterRequest.Traits is not null)
            {
                character.Traits = updateCharacterRequest.Traits;
            }
            if (updateCharacterRequest.Quirks is not null)
            {
                character.Quirks = updateCharacterRequest.Quirks;
            }
            if(updateCharacterRequest.Skills is not null)
            {
                character.Skills = updateCharacterRequest.Skills;
            }

            await _repository.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Character with id: {id}  updated",id);

            return CharacterMapper.ToResponse(character);
        }


        private async Task<Character> GetCharacterByIdCore(
            Guid id,
            CancellationToken cancellationToken
            )
        {
            var character = await _repository.GetByIdAsync(id,cancellationToken);

            if (character == null)
            {
                _logger.LogWarning(
                    "Character with id {id} not found",
                    id);

                throw new NotFoundException(
                    "Character with id {id} not found", id);
            }

            return character;
        }

        private async  Task<Character> GetTrackedCharacterByIdAsync(Guid id,CancellationToken cancellationToken)
        {
            var character = await _repository.GetTrackedByIdAsync(id, cancellationToken);

            if (character == null)
            {
                _logger.LogWarning(
                    "Character with id {id} not found",
                    id);

                throw new NotFoundException(
                    "Character with id {id} not found", id);
            }

            return character;
        }
    }
}
