namespace IKP.Infrastructure.Services.Redis.Interfaces
{
    /// <summary>
    /// Represents an acquired Redis distributed lock.
    /// </summary>
    public interface IRedisDistributedLockHandle : IAsyncDisposable
    {
    }
}
