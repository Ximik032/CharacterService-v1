using CharacterService.Application.DTOs.Character.Requests;
using CharacterService.Application.DTOs.Character.Responses;
using CharacterService.Domain.Entities;

namespace CharacterService.Application.Mappers
{
    public static class CharacterMapper
    {
        public static CharacterResponse ToResponse(Character character)
        {
            var characterResponse = new CharacterResponse
            {
                Id = character.Id,
                Name = character.Name,
                Description = character.Description,
                CreatedAt = character.CreatedAt,
                UpdatedAt = character.UpdatedAt,
                UserId = character.UserId
            };
            return characterResponse;
        }


        public static Character ToEntity(Guid userID,CreateCharacterRequest characterRequest)
        {
            return new Character
            {
                Id = Guid.NewGuid(),
                Name = characterRequest.Name,
                Description = characterRequest.Description,
                Background = characterRequest.Background,
                Quirks = characterRequest.Quirks,
                Skills = characterRequest.Skills,
                Traits = characterRequest.Traits,
                SystemPrompt = characterRequest.SystemPrompt,
                UserId = userID,
            };
        }
    }
}
