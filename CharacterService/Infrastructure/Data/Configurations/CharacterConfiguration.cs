using CharacterService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CharacterService.Infrastructure.Data.Configurations
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("characters");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(x => x.SystemPrompt)
                .HasColumnName("system_prompt")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.Background)
                .HasColumnName("background")
                .HasColumnType("jsonb");

            builder.Property(x => x.Traits)
                .HasColumnName("traits")
                .HasColumnType("jsonb");

            builder.Property(x => x.Quirks)
                .HasColumnName("quirks")
                .HasColumnType("jsonb");

            builder.Property(x => x.Skills)
                .HasColumnName("skills")
                .HasColumnType("jsonb");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.HasIndex(x => x.UserId);
        }
    }
}
