using IKP.Application.Diagnostics.Interfaces;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IKP.Infrastructure.Services.Diagnostics
{
    /// <summary>
    /// Provides Entity Framework persistence for diagnostics.
    /// </summary>
    public class DiagnosticRepository : IDiagnosticRepository
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private AppDbContext? dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticRepository"/> class.
        /// </summary>
        /// <param name="dbContextFactory">Application database context factory.</param>
        public DiagnosticRepository(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc />
        public async ValueTask<ErrorGroup?> FindErrorGroupAsync(string fingerprint, int fingerprintVersion, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);

            return await context.ErrorGroups.FirstOrDefaultAsync(x => x.Fingerprint == fingerprint && x.FingerprintVersion == fingerprintVersion, cancellationToken);
        }

        /// <inheritdoc />
        public async ValueTask<Guid> GetDefinitionIdAsync(string groupCode, string definitionCode, CancellationToken cancellationToken = default)
        {
            AppDbContext context = await GetDbContextAsync(cancellationToken);

            Guid? definitionId = await context.DiagnosticDefinitions
                .Where(x => x.Code == definitionCode && x.IsActive && x.Group.Code == groupCode && x.Group.IsActive)
                .Select(x => (Guid?)x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return definitionId ?? throw new InvalidOperationException($"Diagnostic definition '{definitionCode}' was not found in group '{groupCode}'.");
        }

        /// <inheritdoc />
        public void AddErrorGroup(ErrorGroup errorGroup)
        {
            AppDbContext context = GetRequiredDbContext();

            context.ErrorGroups.Add(errorGroup);
        }

        /// <inheritdoc />
        public void AddErrorOccurrence(ErrorOccurrence errorOccurrence)
        {
            AppDbContext context = GetRequiredDbContext();

            context.ErrorOccurrences.Add(errorOccurrence);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AppDbContext context = GetRequiredDbContext();

            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Gets or creates the database context used by this repository.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The database context.</returns>
        private async ValueTask<AppDbContext> GetDbContextAsync(CancellationToken cancellationToken)
        {
            if (dbContext is not null) return dbContext;

            dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            return dbContext;
        }

        /// <summary>
        /// Gets the initialized database context.
        /// </summary>
        /// <returns>The database context.</returns>
        private AppDbContext GetRequiredDbContext()
        {
            return dbContext ?? throw new InvalidOperationException("The diagnostic repository has not initialized its database context.");
        }
    }
}
