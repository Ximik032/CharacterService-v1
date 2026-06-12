using CharacterService.Application.DTOs.Character.Requests;
using CharacterService.Infrastructure.Extensions;
using FluentValidation;

namespace CharacterService.Application.Validators
{
    public class CreateCharacterRequestValidator:AbstractValidator<CreateCharacterRequest>
    {

        public CreateCharacterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(255);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.SystemPrompt)
                .NotEmpty()
                .MaximumLength(1000);

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
