using System.Collections.Concurrent;
using AltV.Community.Messaging.Abstractions;
using AltV.Community.Messaging.Client.Abstractions;
using AltV.Net;
using AltV.Net.Client;

namespace AltV.Community.Messaging.Client;

public class Messenger(
    IMessagingContextFactory messagingContextFactory,
    IMessageIdProvider messageIdProvider
) : IMessenger
{
    private readonly ConcurrentDictionary<long, StateBag> messageBags = [];
    private readonly ConcurrentDictionary<string, Function> answerHandlers = [];

    public void Publish(string eventName, object?[]? args = null)
    {
        Alt.EmitServer(eventName, BuildArgs(messageIdProvider.GetNext(), args));
    }

    public Task<object?> SendAsync(string eventName, object?[]? args = null)
    {
        return SendAsync<object?>(eventName, args);
    }

    public Task<T> SendAsync<T>(string eventName, object?[]? args = null)
    {
        var bag = SendAsyncInternal<T>(eventName, args);
        return MapInternal<T>(bag.TaskCompletionSource.Task);
    }

    public Action On(string eventName, Action<IMessagingContext> handler)
    {
        return OnInternal(
            eventName,
            Alt.OnServer(
                eventName,
                (long messageId) =>
                {
                    handler(messagingContextFactory.CreateMessagingContext(eventName, messageId));
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
                (long messageId) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId)
                        )
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
                (long messageId, T1 arg1) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName, messageId),
                        arg1
                    );
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
                (long messageId, T1 arg1) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId),
                            arg1
                        )
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
                (long messageId, T1 arg1, T2 arg2) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName, messageId),
                        arg1,
                        arg2
                    );
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
                (long messageId, T1 arg1, T2 arg2) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId),
                            arg1,
                            arg2
                        )
                    );
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) =>
                {
                    handler(
                        messagingContextFactory.CreateMessagingContext(eventName, messageId),
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
                (long messageId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7) =>
                {
                    SafeFireAndForget(
                        handler(
                            messagingContextFactory.CreateMessagingContext(eventName, messageId),
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

    private StateBag SendAsyncInternal<T>(string eventName, object?[]? args)
    {
        var messageId = messageIdProvider.GetNext();
        if (!answerHandlers.ContainsKey(eventName))
        {
            answerHandlers[eventName] = Alt.OnServer<long, T>(eventName, (messageId, answer) => AnswerInternal(messageId, answer));
        }
        var tcs = new TaskCompletionSource<object?>();
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var reg = cts.Token.Register(() =>
        {
            tcs.TrySetCanceled();
        });
        var bag = new StateBag(tcs, cts, reg);
        _ = tcs.Task.ContinueWith(
            (task, state) =>
            {
                if (state is not long messageId || !messageBags.TryRemove(messageId, out var bag))
                {
                    return;
                }
                bag.Dispose();
            },
            messageId,
            CancellationToken.None,
            TaskContinuationOptions.NotOnRanToCompletion,
            TaskScheduler.Default
        );
        messageBags[messageId] = bag;
        Alt.EmitServer(eventName, BuildArgs(messageId, args));
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

    private static object?[] BuildArgs(long messageId, object?[]? args = null)
    {
        if (args is null)
        {
            return [messageId];
        }

        // c# client sandbox forbids spread syntax
        var arr = new object?[args.Length + 1];
        arr[0] = messageId;
        args.CopyTo(arr, 1);
        return arr;
    }

    private void AnswerInternal(long messageId, object? answer)
    {
        if (!messageBags.TryRemove(messageId, out var bag))
        {
            return;
        }

        bag.TaskCompletionSource.TrySetResult(answer);
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
