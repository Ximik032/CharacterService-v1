using CharacterService.Application.Interfaces;
using CharacterService.Domain.Entities;
using CharacterService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CharacterService.Infrastructure.Repositories
{
    public class CharacterRepository:ICharacterRepository
    {
        private readonly CharacterDbContext _context;

        public CharacterRepository(CharacterDbContext context)
        {
            _context = context;
        }

        //commands
        public async Task<Character> AddAsync(Character createCharacter, CancellationToken cancellationToken)
        {
            var result = await _context.Characters
                .AddAsync(createCharacter,cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var deletedRows = await _context.Characters
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            return deletedRows > 0;
        }

        public async Task<int> DeleteByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var deletedRows = await _context.Characters
                .Where(x => x.UserId == userId)
                .ExecuteDeleteAsync(cancellationToken);

            return deletedRows;
        }


        //get with as no tracking
        public async Task<IReadOnlyList<Character>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var results = await _context.Characters
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return results;
        }

        public async Task<Character?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.Characters
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id== id, cancellationToken);

            return result;
        }

        public async Task<Character?> GetTrackedByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var character = await _context.Characters
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return character;
        }

        public async Task<IReadOnlyList<Character>> GetByUserIdAsync(Guid userId,int page,int pageSize, CancellationToken cancellationToken)
        {
            var results = await _context.Characters
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x=>x.CreatedAt)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return results;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Characters.CountAsync(cancellationToken);
        }
        public async Task<int> CountByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Characters.Where(x=>x.UserId==userId).CountAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
