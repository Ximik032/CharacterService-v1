using CharacterService.Application.DTOs.Character.Requests;
using CharacterService.Application.DTOs.Character.Responses;
using CharacterService.Application.DTOs.Paginations;

namespace CharacterService.Application.Services
{
    public interface ICharacterService
    {
        Task<CharacterResponse> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        Task<PaginatedResponse<CharacterResponse>> GetAllAsync(PaginationRequest pagination, CancellationToken cancellationToken);
        Task<PaginatedResponse<CharacterResponse>> GetAllByUserIdAsync(Guid userId, PaginationRequest pagination, CancellationToken cancellationToken);
        

        Task<CharacterResponse> CreateAsync(Guid userId,CreateCharacterRequest createCharacterRequest, CancellationToken cancellationToken);
        Task<CharacterResponse> UpdateAsync(Guid id, UpdateCharacterRequest updateCharacterRequest, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<int> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    }
}
