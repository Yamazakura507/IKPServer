using IKP.Application.Diagnostics.Fingerprinting.Interfaces;
using IKP.Domain.ValueObjects.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace IKP.Application.Diagnostics.Fingerprinting
{
    /// <summary>
    /// Generates stable SHA-256 fingerprints for diagnostic exceptions.
    /// </summary>
    public class ErrorFingerprintService : IErrorFingerprintService
    {
        /// <inheritdoc />
        public int Version => 1;

        /// <inheritdoc />
        public string CreateFingerprint(ErrorExceptionDetails exceptionDetails)
        {
            ArgumentNullException.ThrowIfNull(exceptionDetails);

            string fingerprintSource = BuildFingerprintSource(exceptionDetails);

            byte[] sourceBytes = Encoding.UTF8.GetBytes(fingerprintSource);
            byte[] hashBytes = SHA256.HashData(sourceBytes);

            return Convert.ToHexString(hashBytes);
        }

        /// <summary>
        /// Builds the normalized source string used to calculate the fingerprint.
        /// </summary>
        /// <param name="exceptionDetails">Exception details.</param>
        /// <returns>Normalized fingerprint source.</returns>
        private static string BuildFingerprintSource(ErrorExceptionDetails exceptionDetails)
        {
            string exceptionType = Normalize(exceptionDetails.ExceptionType);
            string sourceFile = Normalize(exceptionDetails.SourceFile);
            string sourceMethod = Normalize(exceptionDetails.SourceMethod);
            string sourceLine = exceptionDetails.SourceLineNumber?.ToString() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(sourceFile) || !string.IsNullOrWhiteSpace(sourceMethod) || !string.IsNullOrWhiteSpace(sourceLine))
            {
                return string.Join("|", exceptionType, sourceFile, sourceMethod, sourceLine);
            }

            string stackTrace = NormalizeStackTrace(exceptionDetails.StackTrace);

            return string.Join("|", exceptionType, stackTrace);
        }

        /// <summary>
        /// Normalizes a fingerprint component.
        /// </summary>
        /// <param name="value">Value to normalize.</param>
        /// <returns>Normalized value.</returns>
        private static string Normalize(string? value) => value?.Trim() ?? string.Empty;

        /// <summary>
        /// Normalizes a stack trace for fingerprint generation.
        /// </summary>
        /// <param name="stackTrace">Stack trace.</param>
        /// <returns>Normalized stack trace.</returns>
        private static string NormalizeStackTrace(string? stackTrace) => 
            string.IsNullOrWhiteSpace(stackTrace) ? string.Empty : string.Join("\n", stackTrace.Split('\r', '\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
    }
}
