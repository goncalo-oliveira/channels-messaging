namespace Faactory.Channels.Messaging;

/// <summary>
/// Defines the interface for a channel subscriber.
/// </summary>
public interface IChannelSubscriber
{
    /// <summary>
    /// Subscribes to channel messages.
    /// </summary>
    /// <returns>An <see cref="IChannelSubscription"/> that can be used to read messages.</returns>
    IChannelSubscription Subscribe();
}

/// <summary>
/// Represents a subscription to channel messages that can be read asynchronously.
/// </summary>
public interface IChannelSubscription : IAsyncDisposable
{
    /// <summary>
    /// Reads all channel messages asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous stream of <see cref="ChannelMessage"/> instances.</returns>
    IAsyncEnumerable<ChannelMessage> ReadAllAsync( CancellationToken cancellationToken = default );
}
