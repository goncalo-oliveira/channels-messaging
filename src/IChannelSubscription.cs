namespace Faactory.Channels.Messaging;

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
