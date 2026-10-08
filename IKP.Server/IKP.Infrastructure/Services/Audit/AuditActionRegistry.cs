using IKP.Infrastructure.Services.Audit.Interfaces;
using Npgsql;
using System.Collections.Concurrent;

namespace IKP.Infrastructure.Services.Audit
{
    public class AuditActionRegistry : IAuditActionRegistry
    {
        private readonly NpgsqlDataSource dataSource;

        private readonly ConcurrentDictionary<string, Guid> cache =
            new(StringComparer.OrdinalIgnoreCase);

        public AuditActionRegistry(NpgsqlDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public Guid GetId(string code) => cache.TryGetValue(code, out Guid id) ? id : GetIdAsync(code).GetAwaiter().GetResult();

        public async ValueTask<Guid> GetIdAsync(string code, CancellationToken cancellationToken = default)
        {
            if (cache.TryGetValue(code, out Guid cachedId)) return cachedId;

            const string sql = """
                SELECT "Id"
                FROM "app_logs"."AuditActions"
                WHERE "Code" = $1
                  AND "IsActive" = TRUE
                LIMIT 1;
                """;

            await using NpgsqlCommand command = dataSource.CreateCommand(sql);

            command.Parameters.AddWithValue(code);

            object? result = await command.ExecuteScalarAsync(cancellationToken);

            if (result is not Guid id) throw new InvalidOperationException($"Audit action '{code}' was not found or is inactive.");

            cache[code] = id;

            return id;
        }
    }
}
