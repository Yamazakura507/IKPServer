namespace IKP.Infrastructure.Services.Audit.Interfaces
{
    public interface IAuditValueSerializer
    {
        string? Serialize(object? value, Type type);

        object? Deserialize(string? json, Type type);
    }
}
