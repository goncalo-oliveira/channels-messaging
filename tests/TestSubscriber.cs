using System.Threading.Channels;
using Faactory.Channels.Messaging;

namespace tests;

internal sealed class TestSubscriber : IChannelSubscriber
{
    private readonly Channel<ChannelMessage> channel = Channel.CreateUnbounded<ChannelMessage>();

    public IChannelSubscription Subscribe()
        => new Subscription( channel );

    public ValueTask PublishAsync( ChannelMessage message )
        => channel.Writer.WriteAsync( message );

    private sealed class Subscription( Channel<ChannelMessage> channel ) : IChannelSubscription
    {
        public ValueTask DisposeAsync()
            => ValueTask.CompletedTask;

        public IAsyncEnumerable<ChannelMessage> ReadAllAsync( CancellationToken cancellationToken = default )
            => channel.Reader.ReadAllAsync( cancellationToken );
    }
}
