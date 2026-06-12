namespace CharacterService.Domain.Exceptions
{
    public class ValidationException:DomainException
    {
        public ValidationException(string message)
            : base(message, "VALIDATION_ERROR")
        {

        }
    }
}
