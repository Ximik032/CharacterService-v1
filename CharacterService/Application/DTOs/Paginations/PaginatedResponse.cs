using CharacterService.Application.DTOs.Character.Responses;
using System.Text.Json.Serialization;

namespace CharacterService.Application.DTOs.Paginations
{
    public record PaginatedResponse<T>
    {
        [JsonPropertyName("items")]
        public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();

        [JsonPropertyName("page")]
        public int Page { get; init; } = 1;

        [JsonPropertyName("pageSize")]
        public int PageSize { get; init; } = 20;

        [JsonPropertyName("totalCount")]
        public int TotalCount { get; init; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; init; }
    }
}
