using IKP.Api.Diagnostics.Correlation.Interfaces;

namespace IKP.Api.Diagnostics.Correlation
{
    /// <summary>
    /// Provides access to the correlation ID stored in the current HTTP context.
    /// </summary>
    public class CorrelationIdAccessor : ICorrelationIdAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrelationIdAccessor"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">HTTP context accessor.</param>
        public CorrelationIdAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public string? CorrelationId
        {
            get => this.httpContextAccessor.HttpContext?.Items.TryGetValue(CorrelationIdConstants.HttpContextItemKey, out object? value) ?? false ? value as string : null;
        }
    }
}
