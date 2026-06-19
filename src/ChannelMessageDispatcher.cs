using Microsoft.Extensions.Logging;

namespace Faactory.Channels.Messaging;

/// <summary>
/// Dispatches messages from a channel to the appropriate message handler.
/// </summary>
internal sealed class ChannelMessageDispatcher(
    ILoggerFactory loggerFactory,
    IChannelSubscriber channelSubscriber,
    IEnumerable<IChannelMessageHandler> handlers
)
: ChannelService
{
    private readonly ILogger logger = loggerFactory.CreateLogger<ChannelMessageDispatcher>();
    private readonly Dictionary<string, IChannelMessageHandler> handlers
        = handlers.ToDictionary( h => h.Type, StringComparer.OrdinalIgnoreCase );

    protected override async Task ExecuteAsync( CancellationToken stoppingToken )
    {
        await using var subscription = channelSubscriber.Subscribe();

        try
        {
            await foreach ( var message in subscription.ReadAllAsync( stoppingToken ) )
            {
                // not for us, ignore
                if ( message.ChannelId != Channel.Id )
                {
                    continue;
                }

                if ( !handlers.TryGetValue( message.Type, out var handler ) )
                {
                    logger.LogDebug(
                        "No handler found for message type {MessageType}",
                        message.Type
                    );

                    continue;
                }

                try
                {
                    await handler.HandleAsync(
                        Channel,
                        message,
                        stoppingToken
                    );
                }
                catch ( Exception ex ) when ( ex is not OperationCanceledException )
                {
                    logger.LogError(
                        ex,
                        "Error handling message {MessageType} for channel {ChannelId}",
                        message.Type,
                        message.ChannelId
                    );
                }
            }
        }
        catch ( OperationCanceledException )
        {
            // ignore
        }
    }
}
