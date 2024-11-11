namespace AltV.Community.Events;

public class Event<T1, T2>(IEventInvoker invoker) : IEvent<T1, T2>
{
    private readonly HashSet<Delegate> delegates = [];

    public bool AddHandler(Action<T1, T2> handler)
    {
        return delegates.Add(handler);
    }

    public bool AddHandler(Func<T1, T2, Task> handler)
    {
        return delegates.Add(handler);
    }

    public bool RemoveHandler(Action<T1, T2> handler)
    {
        return delegates.Remove(handler);
    }

    public bool RemoveHandler(Func<T1, T2, Task> handler)
    {
        return delegates.Remove(handler);
    }

    public void Invoke(T1 arg1, T2 arg2)
    {
        invoker.Invoke(delegates, arg1, arg2);
    }

    public Task InvokeAsync(T1 arg1, T2 arg2)
    {
        return invoker.InvokeAsync(delegates, arg1, arg2);
    }

    public Task InvokeAsyncSerial(T1 arg1, T2 arg2)
    {
        return invoker.InvokeAsyncSerial(delegates, arg1, arg2);
    }
}
