using CharacterService.Application.DTOs.Character.Requests;
using CharacterService.Infrastructure.Extensions;
using FluentValidation;

namespace CharacterService.Application.Validators
{
    public class UpdateCharacterRequestValidator:AbstractValidator<UpdateCharacterRequest>
    {
        public UpdateCharacterRequestValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(255)
                .When(x => x.Name!=null);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .When(x => x.Description!=null);

            RuleFor(x => x.SystemPrompt)
                .MaximumLength(1000)
                .When(x => x.SystemPrompt!=null);

            RuleFor(x => x.Background)
                .MustBeValidObject();

            RuleFor(x => x.Traits)
                .MustBeValidObject();

            RuleFor(x => x.Quirks)
                .MustBeValidObject();

            RuleFor(x => x.Skills)
                .MustBeValidObject();
        }
    }
}
