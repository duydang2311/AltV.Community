using AltV.Community.Messaging.Client.Abstractions;
using AltV.Net.Client;

namespace AltV.Community.Messaging.Client;

public sealed class MessagingContext(string eventName, long messageId) : IMessagingContext
{
    private int responded;

    public void Respond(object? value = null)
    {
        if (Interlocked.CompareExchange(ref responded, 1, 0) == 1)
        {
            return;
        }
        Alt.EmitServer(eventName, messageId, value);
    }
}
