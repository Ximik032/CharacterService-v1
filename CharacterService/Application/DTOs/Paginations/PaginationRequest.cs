using CharacterService.Application.DTOs.Character.Responses;
using System.Text.Json.Serialization;

namespace CharacterService.Application.DTOs.Paginations
{
    public record PaginationRequest
    {
        [JsonPropertyName("page")]
        public int Page { get; init; } = 1;

        [JsonPropertyName("pageSize")]
        public int PageSize { get; init; } = 20;

    }
}
