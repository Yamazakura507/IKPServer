namespace IKP.Domain.Common.System;

/// <summary>
/// Stable built-in background job type codes.
/// </summary>
public static class BackgroundJobTypeCodes
{
    public const string FileUploadCleanup = "FILES_UPLOAD_CLEANUP";
    public const string DiagnosticStatistics = "DIAGNOSTICS_STATISTICS";
    public const string DiagnosticCriticality = "DIAGNOSTICS_CRITICALITY";
}
