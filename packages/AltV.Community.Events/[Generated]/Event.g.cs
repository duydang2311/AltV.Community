namespace AltV.Community.Events;

public class Event(IEventInvoker invoker) : IEvent
{
    private readonly HashSet<Delegate> delegates = [];

    public bool AddHandler(Action handler)
    {
        return delegates.Add(handler);
    }

    public bool AddHandler(Func<Task> handler)
    {
        return delegates.Add(handler);
    }

    public bool RemoveHandler(Action handler)
    {
        return delegates.Remove(handler);
    }

    public bool RemoveHandler(Func<Task> handler)
    {
        return delegates.Remove(handler);
    }

    public void Invoke()
    {
        invoker.Invoke(delegates);
    }

    public Task InvokeAsync()
    {
        return invoker.InvokeAsync(delegates);
    }

    public Task InvokeAsyncSerial()
    {
        return invoker.InvokeAsyncSerial(delegates);
    }
}
