namespace Faactory.Channels.Messaging;

/// <summary>
/// Defines the interface for a channel subscriber.
/// </summary>
public interface IChannelSubscriber
{
    /// <summary>
    /// Subscribes to channel messages.
    /// </summary>
    /// <param name="channelId">The identifier of the channel to subscribe to. If null, subscribes to all messages.</param>
    /// <returns>An <see cref="IChannelSubscription"/> that can be used to read messages.</returns>
    Task<IChannelSubscription> SubscribeAsync( string? channelId );
}
