using System.Collections.Concurrent;
using AltV.Community.Messaging.Abstractions;
using AltV.Community.Messaging.Client.Abstractions;
using AltV.Net;
using AltV.Net.Client;

namespace AltV.Community.Messaging.Client;

public sealed class Messenger(IMessagingContextFactory messagingContextFactory) : IMessenger
{
    private readonly ConcurrentDictionary<string, StateBag> messageTasks = [];

    private readonly ConcurrentDictionary<string, Function> handlerTasks = [];

    public void Publish(string eventName, object?[]? args = null)
    {
        Alt.EmitServer(eventName, args ?? []);
    }

    public Task<object?> SendAsync(string eventName, object?[]? args = null)
    {
        return SendAsync<object?>(eventName, args);
    }

    public Task<T> SendAsync<T>(string eventName, object?[]? args = null)
    {
        var bag = SendAsyncInternal(eventName, args);
        return MapInternal<T>(bag.TaskCompletionSource.Task);
    }

    public bool Answer(string eventName, object? answer)
    {
        if (!messageTasks.TryRemove(eventName, out var bag))
        {
            return false;
        }

        return bag.TaskCompletionSource.TrySetResult(answer);
    }

    private StateBag SendAsyncInternal(string eventName, object?[]? args)
    {
        if (!messageTasks.TryGetValue(eventName, out var bag))
        {
            if (!handlerTasks.ContainsKey(eventName))
            {
                handlerTasks[eventName] = Alt.OnServer<object?>(
                    eventName,
                    (answer) =>
                    {
                        Answer(eventName, answer);
                    }
                );
            }

            var tcs = new TaskCompletionSource<object?>();
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var reg = cts.Token.Register(() =>
            {
                tcs.TrySetCanceled();
            });
            bag = new StateBag(tcs, cts, reg);
            tcs.Task.ContinueWith(
                (task, state) =>
                {
                    if (state is not StateBag stateBag)
                    {
                        return;
                    }
                    stateBag.Dispose();
                },
                bag
            );
            messageTasks[eventName] = bag;
            Alt.EmitServer(eventName, args);
        }
        return bag;
    }

    private static async Task<T> MapInternal<T>(Task<object?> task)
    {
        var ret = await task.ConfigureAwait(false);
        if (ret is T t)
        {
            return t;
        }
        throw new TypeMismatchException();
    }

    private static Action OnInternal(string eventName, Function function)
    {
        return () =>
        {
            Alt.OffClient(eventName, function);
        };
    }

    public Action On(string eventName, Action<IMessagingContext> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                () =>
                {
                    handler(messagingContextFactory.CreateMessagingContext(eventName));
                }
            )
        );
    }

    public Action On(string eventName, Func<IMessagingContext, Task> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                () =>
                {
                    SafeFireAndForget(
                        handler(messagingContextFactory.CreateMessagingContext(eventName))
                    );
                }
            )
        );
    }

    public Action On<T1>(string eventName, Action<IMessagingContext, T1> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1) =>
                {
                    handler(messagingContextFactory.CreateMessagingContext(eventName), arg1);
                }
            )
        );
    }

    public Action On<T1>(string eventName, Func<IMessagingContext, T1, Task> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1) =>
                {
                    SafeFireAndForget(
                        handler(messagingContextFactory.CreateMessagingContext(eventName), arg1)
                    );
                }
            )
        );
    }

    public Action On<T1, T2>(string eventName, Action<IMessagingContext, T1, T2> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2) =>
                {
                    handler(messagingContextFactory.CreateMessagingContext(eventName), arg1, arg2);
                }
            )
        );
    }

    public Action On<T1, T2>(string eventName, Func<IMessagingContext, T1, T2, Task> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName),
                            arg1,
                            arg2
                        )
                    );
                    ;
                    ;
                    ;
                }
            )
        );
    }

    public Action On<T1, T2, T3>(string eventName, Action<IMessagingContext, T1, T2, T3> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName),
                        arg1,
                        arg2,
                        arg3
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3>(
        string eventName,
        Func<IMessagingContext, T1, T2, T3, Task> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName),
                            arg1,
                            arg2,
                            arg3
                        )
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4>(
        string eventName,
        Action<IMessagingContext, T1, T2, T3, T4> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName),
                        arg1,
                        arg2,
                        arg3,
                        arg4
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4>(
        string eventName,
        Func<IMessagingContext, T1, T2, T3, T4, Task> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName),
                            arg1,
                            arg2,
                            arg3,
                            arg4
                        )
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4, T5>(
        string eventName,
        Action<IMessagingContext, T1, T2, T3, T4, T5> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName),
                        arg1,
                        arg2,
                        arg3,
                        arg4,
                        arg5
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4, T5>(
        string eventName,
        Func<IMessagingContext, T1, T2, T3, T4, T5, Task> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName),
                            arg1,
                            arg2,
                            arg3,
                            arg4,
                            arg5
                        )
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4, T5, T6>(
        string eventName,
        Action<IMessagingContext, T1, T2, T3, T4, T5, T6> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName),
                        arg1,
                        arg2,
                        arg3,
                        arg4,
                        arg5,
                        arg6
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4, T5, T6>(
        string eventName,
        Func<IMessagingContext, T1, T2, T3, T4, T5, T6, Task> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName),
                            arg1,
                            arg2,
                            arg3,
                            arg4,
                            arg5,
                            arg6
                        )
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4, T5, T6, T7>(
        string eventName,
        Action<IMessagingContext, T1, T2, T3, T4, T5, T6, T7> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName),
                        arg1,
                        arg2,
                        arg3,
                        arg4,
                        arg5,
                        arg6,
                        arg7
                    );
                }
            )
        );
    }

    public Action On<T1, T2, T3, T4, T5, T6, T7>(
        string eventName,
        Func<IMessagingContext, T1, T2, T3, T4, T5, T6, T7, Task> handler
    )
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName),
                            arg1,
                            arg2,
                            arg3,
                            arg4,
                            arg5,
                            arg6,
                            arg7
                        )
                    );
                }
            )
        );
    }

    private static async void SafeFireAndForget(Task task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Alt.LogError(e.ToString());
        }
    }
}
