using AltV.Community.Messaging.Client.Abstractions;

namespace AltV.Community.Messaging.Client;

public sealed class MessagingContextFactory : IMessagingContextFactory
{
    public IMessagingContext CreateMessagingContext(string eventName, long messageId)
    {
        return new MessagingContext(eventName, messageId);
    }
}
