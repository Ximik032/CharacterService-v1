using CharacterService.Domain.Entities;

namespace CharacterService.Application.Interfaces
{
    public interface ICharacterRepository
    {
        Task<Character?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        Task<Character?> GetTrackedByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Character>> GetByUserIdAsync(Guid userId,int page,int pageSize,CancellationToken cancellationToken);
        Task<IReadOnlyList<Character>> GetAllAsync(int page, int pageSize,CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken);
        Task<int> CountByUserIdAsync(Guid userId, CancellationToken cancellationToken);


        Task<Character> AddAsync(Character createCharacter , CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<int> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    }
}
