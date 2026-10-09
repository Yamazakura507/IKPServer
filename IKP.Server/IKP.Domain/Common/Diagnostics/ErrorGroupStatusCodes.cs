namespace IKP.Domain.Common.Diagnostics
{
    public static class ErrorGroupStatusCodes
    {
        public const string New = "NEW";
        public const string Investigating = "INVESTIGATING";
        public const string Known = "KNOWN";
        public const string FixPlanned = "FIX_PLANNED";
        public const string Fixed = "FIXED";
        public const string Monitoring = "MONITORING";
        public const string Resolved = "RESOLVED";
        public const string Ignored = "IGNORED";
        public const string WontFix = "WONT_FIX";
    }
}
