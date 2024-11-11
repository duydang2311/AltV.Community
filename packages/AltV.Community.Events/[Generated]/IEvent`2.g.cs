namespace AltV.Community.Events;

public interface IEvent<T1, T2>
{
    bool AddHandler(Action<T1, T2> handler);
    bool AddHandler(Func<T1, T2, Task> handler);
    bool RemoveHandler(Action<T1, T2> handler);
    bool RemoveHandler(Func<T1, T2, Task> handler);
    void Invoke(T1 arg1, T2 arg2);
    Task InvokeAsync(T1 arg1, T2 arg2);
    Task InvokeAsyncSerial(T1 arg1, T2 arg2);
}
