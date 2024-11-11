namespace AltV.Community.Events;

public class Event<T1>(IEventInvoker invoker) : IEvent<T1>
{
    private readonly HashSet<Delegate> delegates = [];

    public bool AddHandler(Action<T1> handler)
    {
        return delegates.Add(handler);
    }

    public bool AddHandler(Func<T1, Task> handler)
    {
        return delegates.Add(handler);
    }

    public bool RemoveHandler(Action<T1> handler)
    {
        return delegates.Remove(handler);
    }

    public bool RemoveHandler(Func<T1, Task> handler)
    {
        return delegates.Remove(handler);
    }

    public void Invoke(T1 arg1)
    {
        invoker.Invoke(delegates, arg1);
    }

    public Task InvokeAsync(T1 arg1)
    {
        return invoker.InvokeAsync(delegates, arg1);
    }

    public Task InvokeAsyncSerial(T1 arg1)
    {
        return invoker.InvokeAsyncSerial(delegates, arg1);
    }
}
