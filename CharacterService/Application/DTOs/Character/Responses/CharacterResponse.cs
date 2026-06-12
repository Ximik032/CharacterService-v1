using System.Text.Json.Serialization;

namespace CharacterService.Application.DTOs.Character.Responses
{
    public record CharacterResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("userId")]
        public Guid UserId { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; init; }


        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; init; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; init; } 
    }
}
