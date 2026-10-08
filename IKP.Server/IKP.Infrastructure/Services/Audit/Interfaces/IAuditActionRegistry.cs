namespace IKP.Infrastructure.Services.Audit.Interfaces
{
    public interface IAuditActionRegistry
    {
        Guid GetId(string code);

        ValueTask<Guid> GetIdAsync(string code, CancellationToken cancellationToken = default);
    }
}
