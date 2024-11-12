using AltV.Community.Messaging.Client.Abstractions;

namespace AltV.Community.Messaging.Client;

public sealed class ResponselessMessagingContext : IMessagingContext
{
    private ResponselessMessagingContext() { }

    public static readonly ResponselessMessagingContext Instance = new();

    public void Respond(object? value = null) { }
}
