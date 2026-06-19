using System.Text.Json;
using Faactory.Channels;
using Faactory.Channels.Messaging;
using Microsoft.Extensions.Logging.Abstractions;

namespace tests;

public class DispatcherTests
{
    [Fact]
    public async Task Dispatches_Matching_Message()
    {
        var handler = new TestHandler("test");
        var subscriber = new TestSubscriber();

        using var dispatcher = CreateDispatcher(subscriber, handler );
        var channel = NullChannel.Instance;

        await dispatcher.StartAsync( channel, CancellationToken.None );

        await subscriber.PublishAsync(
            new ChannelMessage(
                channel.Id,
                "test",
                JsonSerializer.SerializeToElement( new { } )
            )
        );

        await handler.WaitAsync();

        Assert.Equal( 1, handler.Count );
    }

    [Fact]
    public async Task Ignores_Messages_For_Other_Channels()
    {
        var handler = new TestHandler("test");
        var subscriber = new TestSubscriber();

        using var dispatcher = CreateDispatcher(subscriber, handler );
        var channel = NullChannel.Instance;

        await dispatcher.StartAsync( channel, CancellationToken.None );

        await subscriber.PublishAsync(
            new ChannelMessage(
                "other",
                "test",
                JsonSerializer.SerializeToElement( new { } )
            )
        );

        Assert.False( await handler.WaitAsync( TimeSpan.FromMilliseconds( 100 ) ) );
        Assert.Equal( 0, handler.Count );
    }

    [Fact]
    public async Task Ignores_Unknown_Message_Types()
    {
        var handler = new TestHandler("test");
        var subscriber = new TestSubscriber();

        using var dispatcher = CreateDispatcher(subscriber, handler );
        var channel = NullChannel.Instance;

        await dispatcher.StartAsync( channel, CancellationToken.None );

        await subscriber.PublishAsync(
            new ChannelMessage(
                channel.Id,
                "unknown",
                JsonSerializer.SerializeToElement( new { } )
            )
        );

        Assert.False( await handler.WaitAsync( TimeSpan.FromMilliseconds( 100 ) ) );
        Assert.Equal( 0, handler.Count );
    }

    private static ChannelMessageDispatcher CreateDispatcher(
        IChannelSubscriber subscriber,
        IChannelMessageHandler handler )
    {
        var dispatcher = new ChannelMessageDispatcher(
            NullLoggerFactory.Instance,
            subscriber,
            [handler]
        );

        return dispatcher;
    }
}
