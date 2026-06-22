namespace Faactory.Channels.Messaging;

/// <summary>
/// Extension methods for <see cref="IChannelSubscription"/>.
/// </summary>
public static class ChannelSubscriptionExtensions
{
    /// <summary>
    /// Reads the first response message with the specified correlation ID from the subscription.
    /// </summary>
    /// <param name="subscription">The channel subscription to read from.</param>
    /// <param name="correlationId">The correlation ID to match the response message.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the response message.</param>
    /// <returns>The first response message with the specified correlation ID, or <c>null</c> if no such message is received before the subscription is completed or the cancellation token is triggered.</returns>
    public static async Task<ChannelMessage?> ReadResponseAsync(
        this IChannelSubscription subscription,
        string correlationId,
        CancellationToken cancellationToken = default
    )
    {
        await foreach ( var message in subscription.ReadAllAsync( cancellationToken ) )
        {
            if ( message.CorrelationId == correlationId )
            {
                return message;
            }
        }

        return null;
    }
}
