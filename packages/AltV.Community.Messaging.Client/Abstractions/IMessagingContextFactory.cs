namespace AltV.Community.Messaging.Client.Abstractions;

public interface IMessagingContextFactory
{
    IMessagingContext CreateMessagingContext(string eventName);
}
