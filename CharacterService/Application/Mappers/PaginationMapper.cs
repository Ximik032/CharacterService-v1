using CharacterService.Application.DTOs.Character.Responses;
using CharacterService.Application.DTOs.Paginations;
using CharacterService.Domain.Entities;

namespace CharacterService.Application.Mappers
{
    public static class PaginationMapper
    {

        public static PaginatedResponse<CharacterResponse> ToResponse(
            IReadOnlyList<Character> characters,
            int page,
            int pageSize,
            int totalCount,
            int totalpages)
        {
            return new PaginatedResponse<CharacterResponse>
            {
                Items = characters.Select(CharacterMapper.ToResponse).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalpages,

            };
        } 
    }
}
