using CharacterService.Domain.Abstractions;
using System.Text.Json;

namespace CharacterService.Domain.Entities
{
    public class Character:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string SystemPrompt { get; set; } = string.Empty;

        //Json
        public JsonElement? Background { get; set; }
        public JsonElement? Traits { get; set; }
        public JsonElement? Quirks { get; set; }
        public JsonElement? Skills { get; set; }

    }
}
