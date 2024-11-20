using AltV.Community.Messaging.Server.Abstractions;
using AltV.Net.Elements.Entities;

namespace AltV.Community.Messaging.Server;

public class MessagingContext<TPlayer>(TPlayer player, long messageId, string eventName)
    : IMessagingContext<TPlayer>
    where TPlayer : IPlayer
{
    private int responded;

    public TPlayer Player => player;

    public void Respond(object?[]? args = null)
    {
        if (Interlocked.CompareExchange(ref responded, 1, 0) == 1)
        {
            return;
        }

        if (args is null)
        {
            player.Emit(eventName, messageId, null);
            return;
        }

        player.Emit(eventName, [messageId, .. args]);
    }
}
