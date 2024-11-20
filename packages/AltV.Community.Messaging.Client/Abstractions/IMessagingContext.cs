namespace AltV.Community.Messaging.Client.Abstractions;

public interface IMessagingContext
{
    void Respond(object?[]? args = null);
}
