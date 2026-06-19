namespace Faactory.Channels.Messaging;

/// <summary>
/// Defines a handler for channel messages.
/// </summary>
public interface IChannelMessageHandler
{
    /// <summary>
    /// Gets the type of message that this handler can process.
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Handles the specified channel message.
    /// </summary>
    /// <param name="channel">The channel associated with the message.</param>
    /// <param name="message">The channel message to be handled.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task HandleAsync( IChannel channel, ChannelMessage message, CancellationToken cancellationToken = default );
}
