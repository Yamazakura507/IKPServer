namespace IKP.Infrastructure.Services.Redis
{
    /// <summary>
    /// Configures the Redis coordination connection.
    /// </summary>
    public class RedisOptions
    {
        /// <summary>Gets or sets the Redis connection string.</summary>
        public string ConnectionString { get; set; } = "localhost:6379,abortConnect=false";
    }
}
