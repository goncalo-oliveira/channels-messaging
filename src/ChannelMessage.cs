using System.Text.Json;

namespace Faactory.Channels.Messaging;

/// <summary>
/// Represents a message sent to/received from a channel.
/// </summary>
/// <param name="ChannelId">The channel identifier.</param>
/// <param name="Type">The message type.</param>
/// <param name="Payload">The message payload.</param>
/// <param name="CorrelationId">The optional correlation identifier.</param>
public record ChannelMessage(
    string ChannelId,
    string Type,
    JsonElement Payload,
    string? CorrelationId = null
)
{
    /// <summary>
    /// Gets the message identifier.
    /// </summary>
    public string MessageId { get; init; } = Guid.NewGuid().ToString( "n" );
}
