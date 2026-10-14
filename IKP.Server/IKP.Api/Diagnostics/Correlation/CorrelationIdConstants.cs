namespace IKP.Api.Diagnostics.Correlation
{
    /// <summary>
    /// Contains constants used by correlation ID infrastructure.
    /// </summary>
    public static class CorrelationIdConstants
    {
        /// <summary>
        /// HTTP header used to propagate correlation IDs.
        /// </summary>
        public const string HeaderName = "X-Correlation-ID";

        /// <summary>
        /// HttpContext item key used to store the current correlation ID.
        /// </summary>
        public const string HttpContextItemKey = "IKP.CorrelationId";
    }
}
