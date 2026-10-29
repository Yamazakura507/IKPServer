namespace IKP.Domain.Common.Codes.Diagnostics
{
    /// <summary>
    /// Contains diagnostic area resolution rule codes.
    /// </summary>
    public static class DiagnosticAreaResolutionRuleCodes
    {
        /// <summary>
        /// Indicates that the area was explicitly supplied in the diagnostic context.
        /// </summary>
        public const string ExplicitContext = "EXPLICIT_CONTEXT";

        /// <summary>
        /// Indicates that the area was resolved from the exception type.
        /// </summary>
        public const string ExceptionType = "EXCEPTION_TYPE";

        /// <summary>
        /// Indicates that the area was resolved from a namespace.
        /// </summary>
        public const string Namespace = "NAMESPACE";

        /// <summary>
        /// Indicates that the area was resolved from an endpoint.
        /// </summary>
        public const string Endpoint = "ENDPOINT";

        /// <summary>
        /// Indicates that the area was resolved from the HTTP pipeline.
        /// </summary>
        public const string HttpPipeline = "HTTP_PIPELINE";
    }
}
