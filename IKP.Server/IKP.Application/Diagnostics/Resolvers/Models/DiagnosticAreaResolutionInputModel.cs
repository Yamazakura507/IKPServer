namespace IKP.Application.Diagnostics.Resolvers.Models
{
    /// <summary>
    /// Contains values available to automatic diagnostic area resolution rules.
    /// </summary>
    public class DiagnosticAreaResolutionInputModel
    {
        /// <summary>
        /// Gets or sets the exception type.
        /// </summary>
        public string? ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the exception namespace.
        /// </summary>
        public string? Namespace { get; set; }

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        public string? Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        public string? Route { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method.
        /// </summary>
        public string? HttpMethod { get; set; }
    }
}
