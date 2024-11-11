namespace AltV.Community.Events;

public interface IEventInvoker
{
    void Invoke(IEnumerable<Delegate> @delegate, params object?[]? args);
    Task InvokeAsync(IEnumerable<Delegate> @delegate, params object?[]? args);
    Task InvokeAsyncSerial(IEnumerable<Delegate> @delegate, params object?[]? args);
}
