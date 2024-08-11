namespace KJWT.SharedKernel.Caching;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T>> factory,
        TimeSpan? expiration = null,
        CancellationToken cancelationToken = default);

    void Remove(
        string key,
        CancellationToken cancellationToken);
}
