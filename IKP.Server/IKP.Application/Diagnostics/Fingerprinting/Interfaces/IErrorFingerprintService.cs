using IKP.Domain.ValueObjects.Diagnostics;

namespace IKP.Application.Diagnostics.Fingerprinting.Interfaces
{
    /// <summary>
    /// Generates stable fingerprints for diagnostic exceptions.
    /// </summary>
    public interface IErrorFingerprintService
    {
        /// <summary>
        /// Gets the current fingerprint algorithm version.
        /// </summary>
        int Version { get; }

        /// <summary>
        /// Creates a stable fingerprint for the specified exception details.
        /// </summary>
        /// <param name="exceptionDetails">Exception details used to generate the fingerprint.</param>
        /// <returns>A stable fingerprint.</returns>
        string CreateFingerprint(ErrorExceptionDetails exceptionDetails);
    }
}
