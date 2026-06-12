namespace CharacterService.Domain.Exceptions
{
    public class NotFoundException:DomainException
    {
        public NotFoundException(string entityName, object id)
            : base("{entityName} with id '{id}' was not found", "NOT_FOUND")
        {

        }
    }
}
