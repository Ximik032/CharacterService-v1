using CharacterService.Domain.Exceptions;
using CharacterService.WebApi.Middleware.Responses;
using System.Diagnostics;

namespace CharacterService.WebApi.Middleware
{
    public class DomainExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DomainExceptionMiddleware> _logger;

        public DomainExceptionMiddleware(RequestDelegate next, ILogger<DomainExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning("Domain error:{ErrorCode} - {Message}", ex.ErrorCode, ex.Message);

                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    ValidationException => StatusCodes.Status400BadRequest,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    BusinessRuleException => StatusCodes.Status422UnprocessableEntity,
                    _ => StatusCodes.Status400BadRequest
                };

                context.Response.ContentType = "application/json";

                var response = new ErrorResponse
                {
                    Error = ex.ErrorCode,
                    Message = ex.Message,
                    TraceId = Activity.Current?.Id ?? context.TraceIdentifier
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new ErrorResponse
                {
                    Error = "INTERNAL_ERROR",
                    Message = $"An unexpected error occurred",
                    TraceId = Activity.Current?.Id ?? context.TraceIdentifier
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
