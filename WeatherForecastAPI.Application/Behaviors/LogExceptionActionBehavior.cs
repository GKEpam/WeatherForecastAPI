using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace WeatherForecastAPI.Application.Behaviors;

public class LogExceptionActionBehavior<TRequest, TException>(ILogger<TRequest> logger) : IRequestExceptionAction<TRequest, TException>
    where TRequest : notnull
    where TException : Exception
{
    public async Task Execute(TRequest request, TException exception, CancellationToken cancellationToken)
    {
        string tRequestName = typeof(TRequest).Name;
        logger.LogError(exception, "An exception occurred for request {tRequestName}", tRequestName);
        await Task.CompletedTask;
    }
}
