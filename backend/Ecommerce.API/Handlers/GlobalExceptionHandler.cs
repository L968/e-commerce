using Ecommerce.Domain.Errors;
using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace Ecommerce.API.Handlers;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        string traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        ProblemDetails problemDetails = CreateProblemDetails(exception);

        LogException(exception, problemDetails.Status!.Value, traceId);

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static ProblemDetails CreateProblemDetails(Exception exception)
    {
        if (exception is DomainException domainException)
        {
            if (domainException.IsNotFound)
            {
                return new ProblemDetails
                {
                    Type = "https://tools.ietfl.org/html/rfc9110#section-15.5.5",
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                };
            }

            return new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = string.Join("; ", domainException.Errors)
            };
        }

        return new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error"
        };
    }

    private void LogException(Exception exception, int statusCode, string traceId)
    {
        string baseMessage = "Status Code: {StatusCode}, TraceId: {TraceId}, Message: {Message}";

        switch (statusCode)
        {
            case StatusCodes.Status404NotFound:
                _logger.LogWarning(baseMessage + ", Resource not found", statusCode, traceId, exception.Message);
                break;
            case StatusCodes.Status400BadRequest:
                _logger.LogWarning(baseMessage + ", Domain warning occurred", statusCode, traceId, exception.Message);
                break;
            default:
                _logger.LogError(
                    exception: exception,
                    message: baseMessage + ", Error processing request on machine {MachineName}",
                    statusCode,
                    traceId,
                    exception.Message,
                    Environment.MachineName
                );
                break;
        }
    }

}
