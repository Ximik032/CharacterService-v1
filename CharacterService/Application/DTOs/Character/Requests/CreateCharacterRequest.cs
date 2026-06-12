using System.Text.Json;
using System.Text.Json.Serialization;

namespace CharacterService.Application.DTOs.Character.Requests
{
    public record CreateCharacterRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; init; } = string.Empty;

        [JsonPropertyName("systemPrompt")]
        public string SystemPrompt { get; init; } = string.Empty;

        [JsonPropertyName("background")]
        public JsonElement? Background { get; init; }

        [JsonPropertyName("traits")]
        public JsonElement? Traits { get; init; }

        [JsonPropertyName("quirks")]
        public JsonElement? Quirks { get; init; }

        [JsonPropertyName("skills")]
        public JsonElement? Skills { get; init; }

    }
}
