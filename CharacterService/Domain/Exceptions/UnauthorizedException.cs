namespace CharacterService.Domain.Exceptions
{
    public class UnauthorizedException:DomainException
    {
        public UnauthorizedException(string message = "Access denied")
            : base(message, "UNAUTHORIZED")
        {

        }
    }
}
