namespace CharacterService.WebApi.Middleware.Responses
{
    public class ErrorResponse
    {
        public string Error { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public string? TraceId { get; init; }
    }
}
