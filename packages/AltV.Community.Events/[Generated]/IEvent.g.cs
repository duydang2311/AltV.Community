namespace AltV.Community.Events;

public interface IEvent
{
    bool AddHandler(Action handler);
    bool AddHandler(Func<Task> handler);
    bool RemoveHandler(Action handler);
    bool RemoveHandler(Func<Task> handler);
    void Invoke();
    Task InvokeAsync();
    Task InvokeAsyncSerial();
}
