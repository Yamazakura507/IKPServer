namespace IKP.Domain.Common.System;

/// <summary>Stable background job status codes.</summary>
public static class BackgroundJobStatusCodes
{
    public const string Queued = "QUEUED";
    public const string Running = "RUNNING";
    public const string Completed = "COMPLETED";
    public const string Failed = "FAILED";
    public const string Cancelled = "CANCELLED";
}
