using CharacterService.Domain.Abstractions;
using CharacterService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CharacterService.Infrastructure.Data
{
    public class CharacterDbContext:DbContext
    {
        public DbSet<Character> Characters {  get; set; }

        public CharacterDbContext(DbContextOptions<CharacterDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CharacterDbContext).Assembly);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                }

                if(entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt=now;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);

        }
    }
}
