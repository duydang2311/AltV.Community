namespace AltV.Community.Messaging.Abstractions;

internal class StateBag(
    TaskCompletionSource<object?> tcs,
    CancellationTokenSource cts,
    CancellationTokenRegistration ctr
)
{
    public readonly TaskCompletionSource<object?> TaskCompletionSource = tcs;
    public readonly CancellationTokenSource CancellationTokenSource = cts;
    public readonly CancellationTokenRegistration CancellationTokenRegistration = ctr;

    public void Dispose()
    {
        CancellationTokenRegistration.Dispose();
        CancellationTokenSource.Dispose();
    }
}
