using IKP.Infrastructure.Services.Redis.Interfaces;

namespace IKP.Infrastructure.Services.Redis
{
    /// <summary>
    /// Represents a fail-open coordination handle when Redis is unavailable.
    /// </summary>
    public class RedisUnavailableLockHandle : IRedisDistributedLockHandle
    {
        /// <inheritdoc />
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
