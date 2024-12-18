using AltV.Community.Messaging.Server.Abstractions;
using AltV.Net.Elements.Entities;

namespace AltV.Community.Messaging.Server;

public sealed class MessagingContextFactory : IMessagingContextFactory
{
    public IMessagingContext<TPlayer> CreateMessagingContext<TPlayer>(
        TPlayer player,
        long messageId,
        string eventName
    )
        where TPlayer : IPlayer
    {
        return messageId == 0
            ? new ResponselessMessagingContext<TPlayer>(player)
            : new MessagingContext<TPlayer>(player, messageId, eventName);
    }
}
