using AltV.Net.Elements.Entities;

namespace AltV.Community.Messaging.Server.Abstractions;

public interface IMessagingContext<TPlayer>
    where TPlayer : IPlayer
{
    TPlayer Player { get; }

    void Respond(object? value = null);
}
