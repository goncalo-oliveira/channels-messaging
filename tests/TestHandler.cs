using Faactory.Channels;
using Faactory.Channels.Messaging;

namespace tests;

internal sealed class TestHandler( string type ) : IChannelMessageHandler
{
    private readonly TaskCompletionSource completion = new();

    public string Type => type;
    public int Count { get; private set; }

    public Task WaitAsync() => completion.Task;

    public async Task<bool> WaitAsync( TimeSpan timeout )
        => await Task.WhenAny( completion.Task, Task.Delay( timeout ) ) == completion.Task;

    public Task HandleAsync( IChannel channel, ChannelMessage message, CancellationToken cancellationToken )
    {
        Count++;

        completion.TrySetResult();

        return Task.CompletedTask;
    }
}
