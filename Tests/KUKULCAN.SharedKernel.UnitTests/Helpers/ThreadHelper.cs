namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides helper methods for multithreaded unit tests.
/// </summary>
public sealed class ThreadHelper
{
    /// <summary>
    /// Executes the specified action in parallel.
    /// </summary>
    public void RunParallel(int threadCount, Action action)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(threadCount);
        ArgumentNullException.ThrowIfNull(action);

        Parallel.For(0, threadCount, _ => action());
    }

    /// <summary>
    /// Executes the specified action asynchronously using multiple tasks.
    /// </summary>
    public async Task RunParallelAsync(int taskCount, Func<Task> action)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taskCount);
        ArgumentNullException.ThrowIfNull(action);

        Task[] tasks =
        [
            .. Enumerable
                .Range(0, taskCount)
                .Select(_ => action())
        ];

        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Executes the specified function concurrently and returns every result.
    /// </summary>
    public IReadOnlyList<T> RunParallel<T>(int taskCount, Func<T> function)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taskCount);
        ArgumentNullException.ThrowIfNull(function);

        T[] results = new T[taskCount];

        Parallel.For(0, taskCount, i => results[i] = function());

        return results;
    }

    /// <summary>
    /// Executes the specified asynchronous function concurrently.
    /// </summary>
    public async Task<IReadOnlyList<T>> RunParallelAsync<T>(int taskCount, Func<Task<T>> function)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(taskCount);
        ArgumentNullException.ThrowIfNull(function);

        Task<T>[] tasks =
        [
            .. Enumerable
                .Range(0, taskCount)
                .Select(_ => function())
        ];

        return await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Repeats the specified action.
    /// </summary>
    public void Repeat(int iterations, Action action)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(iterations);
        ArgumentNullException.ThrowIfNull(action);

        for (int i = 0; i < iterations; i++)
        {
            action();
        }
    }

    /// <summary>
    /// Sleeps the current thread.
    /// </summary>
    public void Sleep(TimeSpan delay)
    {
        Thread.Sleep(delay);
    }

    /// <summary>
    /// Waits asynchronously.
    /// </summary>
    public Task Delay(TimeSpan delay)
    {
        return Task.Delay(delay);
    }
}
