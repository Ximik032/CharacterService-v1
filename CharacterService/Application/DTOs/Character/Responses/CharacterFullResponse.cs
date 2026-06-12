using System.Text.Json;
using System.Text.Json.Serialization;

namespace CharacterService.Application.DTOs.Character.Responses
{
    public record CharacterFullResponse:CharacterResponse
    {
        [JsonPropertyName("background")]
        public JsonElement Background { get; set; }

        [JsonPropertyName("traits")]
        public JsonElement Traits { get; set; }
        
        [JsonPropertyName("quirks")]
        public JsonElement Quirks { get; set; }
        
        [JsonPropertyName("skills")]
        public JsonElement Skills { get; set; }
    }
}
