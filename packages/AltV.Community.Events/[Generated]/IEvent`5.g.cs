namespace AltV.Community.Events;

public interface IEvent<T1, T2, T3, T4, T5>
{
    bool AddHandler(Action<T1, T2, T3, T4, T5> handler);
    bool AddHandler(Func<T1, T2, T3, T4, T5, Task> handler);
    bool RemoveHandler(Action<T1, T2, T3, T4, T5> handler);
    bool RemoveHandler(Func<T1, T2, T3, T4, T5, Task> handler);
    void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    Task InvokeAsync(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    Task InvokeAsyncSerial(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}
