## AltV.Community.Events

[![NuGet badge](https://img.shields.io/nuget/v/AltV.Community.Events?color=blue&cacheSeconds=3600)](https://www.nuget.org/packages/AltV.Community.Events/)

### Quickstart

This package helps you quickly create event as a class with provided base methods for managing event handlers.

#### Installation

```bash
dotnet add package AltV.Community.Events
```

#### Usage with Dependency Injection

1. Create your event

```csharp
using AltV.Net;
using AltV.Community.Events;

// .net 8 primary constructor syntax
public sealed class PlayerLoggedInEvent(IEventInvoker invoker) : Event<IPlayer>(invoker) { }

// with more parameters
public sealed class PlayerLoggedInEvent(IEventInvoker invoker) : Event<IPlayer, int, string, float, double>(invoker) { }
```

2. Register necessary types to the service collection

```csharp
// your resource startup

serviceCollection.AddSingleton<IEventInvoker, EventInvoker>(); // register the default event invoker
serviceCollection.AddSingleton<PlayerLoggedInEvent>(); // register the event
```

3. Use the event by adding/removing handlers or invoking handlers.

```csharp
// somewhere in your Script

public sealed class SpawnPlayerScript(PlayerLoggedInEvent playerLoggedInEvent)
{
    playerLoggedInEvent.AddHandler((player) =>
    {
        Console.WriteLine($"{player.Name} has logged in successfully!");
        SpawnPlayer(player);
    });
    playerLoggedInEvent.AddHandler((player) =>
    {
        Console.WriteLine("I'm another event handler");
    });
}

public sealed class PlayerConnectScript(PlayerLoggedInEvent playerLoggedInEvent)
{
    Alt.OnPlayerConnect += (player) =>
    {
        var success = AskPlayerToLogin(player);
        if (success)
        {
            playerLoggedInEvent.Invoke(player); // or InvokeAsync, InvokeAsyncSerial
        }
    };
}
```
