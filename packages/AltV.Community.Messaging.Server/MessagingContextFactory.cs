using AltV.Community.Messaging.Server.Abstractions;
using AltV.Net.Elements.Entities;

namespace AltV.Community.Messaging.Server;

public sealed class MessagingContextFactory : IMessagingContextFactory
{
    public IMessagingContext<TPlayer> CreateMessagingContext<TPlayer>(
        TPlayer player,
        string eventName
    )
        where TPlayer : IPlayer
    {
        return new MessagingContext<TPlayer>(player, eventName);
    }
}
