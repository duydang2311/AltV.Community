namespace AltV.Community.Events;

public interface IEvent<T1>
{
    bool AddHandler(Action<T1> handler);
    bool AddHandler(Func<T1, Task> handler);
    bool RemoveHandler(Action<T1> handler);
    bool RemoveHandler(Func<T1, Task> handler);
    void Invoke(T1 arg1);
    Task InvokeAsync(T1 arg1);
    Task InvokeAsyncSerial(T1 arg1);
}
