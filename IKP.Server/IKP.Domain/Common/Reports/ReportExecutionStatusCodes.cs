namespace IKP.Domain.Common.Reports;

/// <summary>
/// Contains stable report execution status codes.
/// </summary>
public static class ReportExecutionStatusCodes
{
    public const string Queued = "QUEUED";
    public const string Running = "RUNNING";
    public const string Completed = "COMPLETED";
    public const string Failed = "FAILED";
    public const string Cancelled = "CANCELLED";
}
