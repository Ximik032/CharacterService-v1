namespace CharacterService.Domain.Exceptions
{
    public class BusinessRuleException:DomainException
    {
        public BusinessRuleException(string message)
            : base(message, "BUSINESS_RULE_VIOLATION")
        {

        }
    }
}
