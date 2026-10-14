namespace IKP.Domain.Common.Diagnostics
{
    /// <summary>
    /// Contains system codes of diagnostic areas.
    /// </summary>
    public static class DiagnosticAreaCodes
    {
        /// <summary>
        /// API and HTTP server infrastructure.
        /// </summary>
        public const string Api = "API";

        /// <summary>
        /// Diagnostic subsystem.
        /// </summary>
        public const string Diagnostics = "DIAGNOSTICS";

        /// <summary>
        /// Encryption and cryptographic operations.
        /// </summary>
        public const string Encryption = "ENCRYPTION";

        /// <summary>
        /// User authentication.
        /// </summary>
        public const string Authentication = "AUTHENTICATION";

        /// <summary>
        /// User authorization and access control.
        /// </summary>
        public const string Authorization = "AUTHORIZATION";

        /// <summary>
        /// Administrative user registration and initial access assignment.
        /// </summary>
        public const string Registration = "REGISTRATION";

        /// <summary>
        /// User and technical support.
        /// </summary>
        public const string Support = "SUPPORT";
    }
}
