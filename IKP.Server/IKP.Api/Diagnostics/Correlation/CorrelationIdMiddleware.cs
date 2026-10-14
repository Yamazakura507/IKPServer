using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.RegularExpressions;

namespace IKP.Api.Diagnostics.Correlation
{
    /// <summary>
    /// Provides and validates a correlation identifier for the current HTTP request.
    /// </summary>
    public class CorrelationIdMiddleware
    {
        private const int MaximumCorrelationIdLength = 128;
        private const char InvalidCharacterReplacement = '_';
        private const bool ReplaceInvalidCharacters = true;
        private const bool ReplaceWhitespaceCharacters = true;
        private static readonly Regex CorrelationIdPattern = new(@"^[\p{L}\p{N}._-]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="CorrelationIdMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the HTTP pipeline.</param>
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Processes the current HTTP request and ensures that it has a valid correlation identifier.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>A task representing the asynchronous middleware operation.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string correlationId = GetOrCreateCorrelationId(httpContext);

            httpContext.Items[CorrelationIdConstants.HttpContextItemKey] = correlationId;

            httpContext.Response.OnStarting(
                () =>
                {
                    httpContext.Response.Headers[CorrelationIdConstants.HeaderName] = correlationId;

                    return Task.CompletedTask;
                });

            await this.next(httpContext);
        }

        /// <summary>
        /// Gets a valid client-provided correlation identifier or creates a new one.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>A normalized and validated correlation identifier.</returns>
        private static string GetOrCreateCorrelationId(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue(CorrelationIdConstants.HeaderName, out StringValues values)) return CreateCorrelationId();

            string correlationId = values.ToString().Trim();

            if (string.IsNullOrWhiteSpace(correlationId)) return CreateCorrelationId();

            correlationId = NormalizeCorrelationId(correlationId);

            if (!IsValidCorrelationId(correlationId)) return CreateCorrelationId();

            return correlationId;
        }

        /// <summary>
        /// Normalizes a correlation identifier according to the configured normalization rules.
        /// </summary>
        /// <param name="correlationId">The correlation identifier to normalize.</param>
        /// <returns>The normalized correlation identifier.</returns>
        private static string NormalizeCorrelationId(string correlationId) => 
            string.IsNullOrEmpty(correlationId) ? string.Empty : string.Create(correlationId.Length, correlationId, (buffer, str) =>
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char character = str[i];
                    bool shouldReplace = char.IsWhiteSpace(character) ? ReplaceWhitespaceCharacters : (!IsAllowedCharacter(character) && ReplaceInvalidCharacters);
                    buffer[i] = shouldReplace ? InvalidCharacterReplacement : character;
                }
            });

        /// <summary>
        /// Determines whether a character is allowed in a correlation identifier.
        /// </summary>
        /// <param name="character">The character to check.</param>
        /// <returns><see langword="true"/> when the character is allowed; otherwise, <see langword="false"/>.</returns>
        private static bool IsAllowedCharacter(char character) => char.IsLetterOrDigit(character) || character is '.' or '-' or '_';

        /// <summary>
        /// Determines whether a correlation identifier satisfies the application requirements.
        /// </summary>
        /// <param name="correlationId">The correlation identifier to validate.</param>
        /// <returns><see langword="true"/> when the identifier is valid; otherwise, <see langword="false"/>.</returns>
        private static bool IsValidCorrelationId(string correlationId) => !(string.IsNullOrWhiteSpace(correlationId) || correlationId.Length > MaximumCorrelationIdLength || correlationId.Any(c => !IsAllowedCharacter(c)));

        /// <summary>
        /// Creates a new server-generated correlation identifier.
        /// </summary>
        /// <returns>A new correlation identifier.</returns>
        private static string CreateCorrelationId() => Guid.NewGuid().ToString("N");
    }
}
