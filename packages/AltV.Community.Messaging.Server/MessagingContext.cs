using AltV.Community.Messaging.Server.Abstractions;
using AltV.Net.Elements.Entities;

namespace AltV.Community.Messaging.Server;

public sealed class MessagingContext<TPlayer>(TPlayer player, long messageId, string eventName)
    : IMessagingContext<TPlayer>
    where TPlayer : IPlayer
{
    private int responded;

    public TPlayer Player => player;

    public void Respond(object? value = null)
    {
        if (Interlocked.CompareExchange(ref responded, 1, 0) == 1)
        {
            return;
        }
        player.Emit(eventName, messageId, value);
    }
}
