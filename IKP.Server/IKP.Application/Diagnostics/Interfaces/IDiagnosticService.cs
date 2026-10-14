using IKP.Application.Diagnostics.Models;

namespace IKP.Application.Diagnostics.Interfaces
{
    /// <summary>
    /// Provides application services for diagnostic error registration.
    /// </summary>
    public interface IDiagnosticService
    {
        /// <summary>
        /// Registers an exception in the diagnostic subsystem.
        /// </summary>
        /// <param name="exception">Exception to register.</param>
        /// <param name="context">Diagnostic context.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The identifier of the affected error group.</returns>
        ValueTask<Guid> RegisterErrorAsync(Exception exception, DiagnosticErrorContext context, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a diagnostic area identifier by group and areas codes
        /// </summary>
        /// <param name="code">Diagnostic area code</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        ValueTask<Guid> GetDiagnosticAreaIdAsync(string code, CancellationToken cancellationToken = default);
    }
}
