using IKP.Infrastructure.Services.Redis.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Security.Cryptography;

namespace IKP.Infrastructure.Services.Redis
{
    /// <summary>
    /// Implements distributed locking through Redis SET NX semantics exposed by StackExchange.Redis.
    /// </summary>
    public class RedisDistributedLockService : IRedisDistributedLockService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> connection;
        private readonly ILogger<RedisDistributedLockService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisDistributedLockService"/> class.
        /// </summary>
        /// <param name="options">Redis configuration.</param>
        /// <param name="logger">Logger.</param>
        public RedisDistributedLockService(IOptions<RedisOptions> options, ILogger<RedisDistributedLockService> logger)
        {
            this.logger = logger;

            connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options.Value.ConnectionString));
        }

        /// <inheritdoc />
        public async ValueTask<IRedisDistributedLockHandle?> TryAcquireAsync(string key, TimeSpan expiry, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(16));

            try
            {
                IDatabase database = connection.Value.GetDatabase();
                bool acquired = await database.LockTakeAsync(key, token, expiry);

                return acquired ? new RedisDistributedLockHandle(database, key, token) : null;
            }
            catch (Exception exception)
            {
                logger.LogWarning(exception, "Redis distributed lock could not be acquired for key {Key}. PostgreSQL remains the source of truth; processing continues without the Redis lock.", key);
                
                return new RedisUnavailableLockHandle();
            }
        }

        /// <summary>
        /// Releases the Redis connection when it has been initialized.
        /// </summary>
        public void Dispose()
        {
            if (connection.IsValueCreated) connection.Value.Dispose();
        }
    }
}
