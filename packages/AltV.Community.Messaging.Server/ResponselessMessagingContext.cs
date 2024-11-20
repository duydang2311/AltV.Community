using AltV.Community.Messaging.Server.Abstractions;
using AltV.Net.Elements.Entities;

namespace AltV.Community.Messaging.Server;

public sealed class ResponselessMessagingContext<TPlayer>(TPlayer player)
    : IMessagingContext<TPlayer>
    where TPlayer : IPlayer
{
    public TPlayer Player => player;

    public void Respond(object?[]? args = null) { }
}
