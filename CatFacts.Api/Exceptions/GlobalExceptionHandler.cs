using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CatFacts.Api.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken = default)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");

        var problemDetails = exception switch
        {
            HttpRequestException => new ProblemDetails
            {
                Status = StatusCodes.Status502BadGateway,
                Title = "External API Error",
                Detail = "An error occurred while communicating with an external API."
            },

            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred."
            }
        };

        await context.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}
