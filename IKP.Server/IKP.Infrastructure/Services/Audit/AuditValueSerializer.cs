using IKP.Infrastructure.Services.Audit.Interfaces;
using System.Text.Json;

namespace IKP.Infrastructure.Services.Audit
{
    public class AuditValueSerializer : IAuditValueSerializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = false
        };

        public string? Serialize(object? value, Type type)
        {
            if (value is null) return null;

            return JsonSerializer.Serialize(value, type, Options);
        }

        public object? Deserialize(string? json, Type type)
        {
            if (string.IsNullOrWhiteSpace(json)) return null;

            return JsonSerializer.Deserialize(json, type, Options);
        }
    }
}
