# Channels Messaging

Messaging support for Channels.

This package provides a simple publish/subscribe abstraction that allows messages to be exchanged between channel instances and external components.

> [!NOTE] This project is currently incubated and is not yet considered production-ready. The API may change in future releases.

## Features

- Publish messages to channels
- Subscribe to channel messages
- Channel-scoped message dispatching
- Request/response workflows through message correlation
- Transport-agnostic abstractions

## Getting Started

Enable messaging on a channel configuration.

```csharp
services.AddChannels( channel =>
{
    channel.AddMessaging( messaging =>
    {
        messaging.AddMessageHandler<MyMessageHandler>();
    } );
} );
```

Implement a message handler.

```csharp
public class MyMessageHandler : IChannelMessageHandler
{
    public string Type => "sample.message";

    public Task HandleAsync(
        IChannel channel,
        ChannelMessage message,
        CancellationToken cancellationToken = default )
    {
        // handle message

        return Task.CompletedTask;
    }
}
```

Publish a message.

```csharp
await channelPublisher.PublishAsync(
    new ChannelMessage(
        channelId,
        "sample.message",
        JsonSerializer.SerializeToElement( new
        {
            Value = 123
        } )
    )
);
```

## Message Correlation

Each `ChannelMessage` contains a unique `MessageId` and an optional `CorrelationId`.

These can be used to implement request/response workflows and correlate responses to previously published messages.

## Transports

This package only defines the messaging abstractions and channel-side dispatching mechanisms.

Transport implementations are not included and must be provided separately. You can implement your own transport or use existing ones that integrate with this messaging system.
