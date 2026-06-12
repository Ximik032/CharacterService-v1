using FluentValidation;
using System.Text.Json;

namespace CharacterService.Infrastructure.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T,JsonElement?> MustBeValidObject<T>(
            this IRuleBuilder<T,JsonElement?> ruleBuilder
            )
        {
            return ruleBuilder
            .Must(x =>
                !x.HasValue ||
                x.Value.ValueKind == JsonValueKind.Object)
            .WithMessage("Must be valid JSON object");
        }
    }
}
