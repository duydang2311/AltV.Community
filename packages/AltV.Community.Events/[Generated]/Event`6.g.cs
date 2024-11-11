namespace AltV.Community.Events;

public class Event<T1, T2, T3, T4, T5, T6>(IEventInvoker invoker) : IEvent<T1, T2, T3, T4, T5, T6>
{
    private readonly HashSet<Delegate> delegates = [];

    public bool AddHandler(Action<T1, T2, T3, T4, T5, T6> handler)
    {
        return delegates.Add(handler);
    }

    public bool AddHandler(Func<T1, T2, T3, T4, T5, T6, Task> handler)
    {
        return delegates.Add(handler);
    }

    public bool RemoveHandler(Action<T1, T2, T3, T4, T5, T6> handler)
    {
        return delegates.Remove(handler);
    }

    public bool RemoveHandler(Func<T1, T2, T3, T4, T5, T6, Task> handler)
    {
        return delegates.Remove(handler);
    }

    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        invoker.Invoke(delegates, arg1, arg2, arg3, arg4, arg5, arg6);
    }

    public Task InvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        return invoker.InvokeAsync(delegates, arg1, arg2, arg3, arg4, arg5, arg6);
    }

    public Task InvokeAsyncSerial(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        return invoker.InvokeAsyncSerial(delegates, arg1, arg2, arg3, arg4, arg5, arg6);
    }
}
