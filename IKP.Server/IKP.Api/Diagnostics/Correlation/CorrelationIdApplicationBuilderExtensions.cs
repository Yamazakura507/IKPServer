namespace IKP.Api.Diagnostics.Correlation
{
    /// <summary>
    /// Provides extensions for registering correlation ID middleware.
    /// </summary>
    public static class CorrelationIdApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds correlation ID processing to the HTTP pipeline.
        /// </summary>
        /// <param name="application">Web application.</param>
        /// <returns>The same application instance.</returns>
        public static WebApplication UseCorrelationId(this WebApplication application)
        {
            application.UseMiddleware<CorrelationIdMiddleware>();

            return application;
        }
    }
}
