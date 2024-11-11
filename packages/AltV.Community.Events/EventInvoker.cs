namespace AltV.Community.Events;

public class EventInvoker : IEventInvoker
{
    public void Invoke(IEnumerable<Delegate> delegates, params object?[]? args)
    {
        foreach (var @delegate in delegates)
        {
            @delegate.Method.Invoke(@delegate.Target, args);
        }
    }

    public Task InvokeAsync(IEnumerable<Delegate> delegates, params object?[]? args)
    {
        var tasks = new LinkedList<Task>();
        foreach (var @delegate in delegates)
        {
            var ret = @delegate.Method.Invoke(@delegate.Target, args);
            if (ret is Task task)
            {
                tasks.AddLast(task);
            }
        }
        return Task.WhenAll(tasks);
    }

    public async Task InvokeAsyncSerial(IEnumerable<Delegate> delegates, params object?[]? args)
    {
        foreach (var @delegate in delegates)
        {
            var ret = @delegate.Method.Invoke(@delegate.Target, args);
            if (ret is Task task)
            {
                await task.ConfigureAwait(false);
            }
        }
    }
}
