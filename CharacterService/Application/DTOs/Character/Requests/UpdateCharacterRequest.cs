using System.Text.Json;
using System.Text.Json.Serialization;

namespace CharacterService.Application.DTOs.Character.Requests
{
    public record UpdateCharacterRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("systemPrompt")]
        public string? SystemPrompt { get; init; }

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
