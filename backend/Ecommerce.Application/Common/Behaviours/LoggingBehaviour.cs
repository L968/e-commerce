using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(
    ILogger<TRequest> logger,
    ICurrentUserService currentUserService
    ) : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger = logger;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        int userId = _currentUserService.UserId;

        _logger.LogInformation("Ecommerce Request: {Name} {@UserId} {@Request}",
            requestName, userId, request);

        return Task.CompletedTask;
    }
}
