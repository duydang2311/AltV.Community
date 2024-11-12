using AltV.Community.Messaging.Abstractions;

namespace AltV.Community.Messaging;

public sealed class MessageIdProvider : IMessageIdProvider
{
    private long id;

    public long GetNext()
    {
        return Interlocked.CompareExchange(ref id, 1, long.MaxValue) == long.MaxValue
            ? 1
            : Interlocked.Increment(ref id);
    }
}
