namespace CharacterService.Domain.Exceptions
{
    public class DomainException:Exception
    {
        public string ErrorCode { get; }

        protected DomainException(string msg, string errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
