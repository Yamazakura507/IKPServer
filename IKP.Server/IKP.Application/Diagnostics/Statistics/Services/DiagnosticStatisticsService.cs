using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Statistics.Interfaces;

namespace IKP.Application.Diagnostics.Statistics.Services
{
    /// <summary>
    /// Executes durable diagnostic statistics recalculations.
    /// </summary>
    public class DiagnosticStatisticsService : IDiagnosticStatisticsService
    {
        private readonly IDiagnosticRepository diagnosticRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticStatisticsService"/> class.
        /// </summary>
        /// <param name="diagnosticRepository">Diagnostic repository.</param>
        public DiagnosticStatisticsService(IDiagnosticRepository diagnosticRepository)
        {
            this.diagnosticRepository = diagnosticRepository;
        }

        /// <inheritdoc />
        public async ValueTask RecalculateAsync(Guid errorGroupId, DateTime calculatedAt, CancellationToken cancellationToken = default)
        {
            await diagnosticRepository.RecalculateStatisticsAsync(errorGroupId, calculatedAt, cancellationToken);
        }
    }
}
