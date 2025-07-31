using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("➡️ Starting request: {RequestName} | Data: {Serialize}", requestName, JsonSerializer.Serialize(request));

        var stopwatch = Stopwatch.StartNew();
        var response = await next(cancellationToken);
        stopwatch.Stop();

        logger.LogInformation($"✅ Finished request: {requestName} in {stopwatch.ElapsedMilliseconds}ms");

        if (stopwatch.ElapsedMilliseconds > 30000)
        {
            logger.LogWarning("⚠️ {RequestName} took longer than expected.", requestName);
        }
        return response;
    }
}
