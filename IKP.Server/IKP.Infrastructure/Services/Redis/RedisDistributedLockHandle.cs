using IKP.Infrastructure.Services.Redis.Interfaces;
using StackExchange.Redis;

namespace IKP.Infrastructure.Services.Redis
{
    /// <summary>
    /// Represents an acquired Redis lock and releases it using the ownership token.
    /// </summary>
    public class RedisDistributedLockHandle : IRedisDistributedLockHandle
    {
        private readonly IDatabase database;
        private readonly string key;
        private readonly string token;
        private int disposed;

        /// <summary>
        /// Initializes a new lock handle.
        /// </summary>
        public RedisDistributedLockHandle(IDatabase database, string key, string token)
        {
            this.database = database;
            this.key = key;
            this.token = token;
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0) return;

            await database.LockReleaseAsync(key, token);
        }
    }
}
