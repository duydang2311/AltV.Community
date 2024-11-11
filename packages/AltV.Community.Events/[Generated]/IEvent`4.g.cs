namespace AltV.Community.Events;

public interface IEvent<T1, T2, T3, T4>
{
    bool AddHandler(Action<T1, T2, T3, T4> handler);
    bool AddHandler(Func<T1, T2, T3, T4, Task> handler);
    bool RemoveHandler(Action<T1, T2, T3, T4> handler);
    bool RemoveHandler(Func<T1, T2, T3, T4, Task> handler);
    void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    Task InvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    Task InvokeAsyncSerial(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}
