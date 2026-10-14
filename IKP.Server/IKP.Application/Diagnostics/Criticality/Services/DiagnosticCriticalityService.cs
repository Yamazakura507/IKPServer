using IKP.Application.Diagnostics.Criticality.Interfaces;
using IKP.Application.Diagnostics.Criticality.Models;
using IKP.Application.Diagnostics.Interfaces;

namespace IKP.Application.Diagnostics.Criticality.Services
{
    /// <summary>
    /// Coordinates diagnostic criticality calculation and persistence.
    /// </summary>
    public class DiagnosticCriticalityService : IDiagnosticCriticalityService
    {
        private readonly IDiagnosticRepository diagnosticRepository;
        private readonly DiagnosticCriticalityCalculator calculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticCriticalityService"/> class.
        /// </summary>
        /// <param name="diagnosticRepository">Diagnostic repository.</param>
        /// <param name="calculator">Criticality calculator.</param>
        public DiagnosticCriticalityService(IDiagnosticRepository diagnosticRepository, DiagnosticCriticalityCalculator calculator)
        {
            this.diagnosticRepository = diagnosticRepository;
            this.calculator = calculator;
        }

        /// <inheritdoc />
        public async ValueTask RecalculateAsync(Guid errorGroupId, DateTime calculatedAt, CancellationToken cancellationToken = default)
        {
            DiagnosticCriticalityCalculationInput input = await diagnosticRepository.GetCriticalityCalculationInputAsync(errorGroupId, cancellationToken);
            DiagnosticCriticalityCalculationResult result = calculator.Calculate(input);

            await diagnosticRepository.SaveCriticalityAsync(errorGroupId, input.StatisticsCalculatedAt, result, cancellationToken);
        }
    }
}
