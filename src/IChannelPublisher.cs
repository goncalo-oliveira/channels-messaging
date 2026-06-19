namespace Faactory.Channels.Messaging;

/// <summary>
/// Defines the interface for a channel publisher.
/// </summary>
public interface IChannelPublisher
{
    /// <summary>
    /// Publishes a message.
    /// </summary>
    /// <param name="message">The message to publish.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ID of the published message.</returns>
    Task<string> PublishAsync( ChannelMessage message, CancellationToken cancellationToken = default );
}
