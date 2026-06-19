
using Faactory.Channels.Messaging;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0130
namespace Faactory.Channels;
#pragma warning restore IDE0130

/// <summary>
/// Provides extension methods for adding messaging support to the channel builder.
/// </summary>
public static class ChannelBuilderMessagingExtensions
{
    /// <summary>
    /// Adds messaging support to the channel builder.
    /// </summary>
    /// <param name="builder">The channel builder.</param>
    /// <param name="configure">An optional action to configure the messaging builder.</param>
    /// <returns>The channel builder.</returns>
    public static IChannelBuilder AddMessaging( this IChannelBuilder builder, Action<IChannelMessagingBuilder>? configure = null )
    {
        builder.AddChannelService<ChannelMessageDispatcher>();

        if ( configure != null )
        {
            var messagingBuilder = new ChannelMessagingBuilder( builder );

            configure( messagingBuilder );
        }

        return builder;
    }

    private sealed class ChannelMessagingBuilder( IChannelBuilder channelBuilder ) : IChannelMessagingBuilder
    {
        public IServiceCollection Services => channelBuilder.Services;

        public IChannelMessagingBuilder AddMessageHandler<THandler>() where THandler : class, IChannelMessageHandler
        {
            Services.AddSingleton<IChannelMessageHandler, THandler>();

            return this;
        }
    }
}

/// <summary>
/// Defines a builder for configuring channel messaging support.
/// </summary>
public interface IChannelMessagingBuilder
{
    /// <summary>
    /// Gets the service collection.
    /// </summary>
    IServiceCollection Services { get; }

    /// <summary>
    /// Adds a message handler to the channel messaging builder.
    /// </summary>
    /// <typeparam name="THandler">The message handler type.</typeparam>
    /// <returns>The channel messaging builder.</returns>
    IChannelMessagingBuilder AddMessageHandler<THandler>() where THandler : class, IChannelMessageHandler;
}
