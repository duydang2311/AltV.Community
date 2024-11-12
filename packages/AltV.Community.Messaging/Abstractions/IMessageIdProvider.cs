namespace AltV.Community.Messaging.Abstractions;

public interface IMessageIdProvider
{
    long GetNext();
}
