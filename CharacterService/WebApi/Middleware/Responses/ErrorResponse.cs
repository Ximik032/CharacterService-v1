namespace CharacterService.WebApi.Middleware.Responses
{
    public class ErrorResponse
    {
        public string Error { get; init; }
        public string Message { get; init; }
        public string? TraceId { get; init; }
    }
}
