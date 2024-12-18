using AltV.Net.Elements.Entities;

namespace AltV.Community.Messaging.Server.Abstractions;

public interface IMessagingContextFactory
{
    IMessagingContext<TPlayer> CreateMessagingContext<TPlayer>(
        TPlayer player,
        long messageId,
        string eventName
    )
        where TPlayer : IPlayer;
}
