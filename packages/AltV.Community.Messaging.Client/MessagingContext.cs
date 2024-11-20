using AltV.Community.Messaging.Client.Abstractions;
using AltV.Net.Client;

namespace AltV.Community.Messaging.Client;

public class MessagingContext(string eventName, long messageId) : IMessagingContext
{
    private int responded;

    public void Respond(object?[]? args)
    {
        if (Interlocked.CompareExchange(ref responded, 1, 0) == 1)
        {
            return;
        }

        if (args is null)
        {
            Alt.EmitServer(eventName, messageId, null);
            return;
        }

        var extended = new object?[args.Length + 1];
        extended[0] = messageId;
        args.CopyTo(extended, 1);
        Alt.EmitServer(eventName, extended);
    }
}
