using Bookify.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger = logger;

    private readonly Action<ILogger, string, Exception?> _executingCommand =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, "ExecutingCommand"),
            "Executing command {Command}");

    private readonly Action<ILogger, string, Exception?> _commandExecuted =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(2, "CommandExecuted"),
            "Command {Command} executed successfully");

    private readonly Action<ILogger, string, Exception?> _commandFailed =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(3, "CommandFailed"),
            "Command {Command} processing failed");

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);
        ArgumentNullException.ThrowIfNull(request);

        string name = request.GetType().Name;
        try
        {
            _executingCommand(_logger, name, null);
            TResponse? result = await next(cancellationToken).ConfigureAwait(false);
            _commandExecuted(_logger, name, null);
            return result;
        }
        catch (Exception exception)
        {
            _commandFailed(_logger, name, exception);
            throw;
        }
    }
}
