using IKP.Application.Diagnostics.Resolvers.Models;
using IKP.Domain.Entities.Diagnostics.Areas;

namespace IKP.Application.Diagnostics.Resolvers.Interfaces
{
    /// <summary>
    /// Resolves diagnostic areas automatically from diagnostic context and configured rules.
    /// </summary>
    public interface IDiagnosticAreaResolver
    {
        /// <summary>
        /// Resolves a diagnostic area from available diagnostic values.
        /// </summary>
        /// <param name="input">Diagnostic values available to the resolver.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created resolution or null when no mapping matched.</returns>
        ValueTask<DiagnosticAreaResolution?> ResolveAsync(DiagnosticAreaResolutionInputModel input, CancellationToken cancellationToken = default);
    }
}
