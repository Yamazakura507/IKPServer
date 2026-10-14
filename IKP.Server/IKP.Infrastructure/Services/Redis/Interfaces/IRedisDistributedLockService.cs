namespace IKP.Infrastructure.Services.Redis.Interfaces
{
    /// <summary>
    /// Provides distributed Redis locks used only for coordination.
    /// </summary>
    public interface IRedisDistributedLockService
    {
        /// <summary>
        /// Attempts to acquire a distributed lock.
        /// </summary>
        /// <param name="key">Lock key.</param>
        /// <param name="expiry">Lock lifetime.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A lock handle or null when another worker owns the lock.</returns>
        ValueTask<IRedisDistributedLockHandle?> TryAcquireAsync(string key, TimeSpan expiry, CancellationToken cancellationToken = default);
    }
}
