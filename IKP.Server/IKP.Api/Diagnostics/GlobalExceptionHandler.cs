using IKP.Application.Auth.Exceptions;
using IKP.Api.Diagnostics.Correlation;
using IKP.Api.Diagnostics.Correlation.Interfaces;
using IKP.Api.Diagnostics.Models.Errors;
using IKP.Api.Infrastructure.Query;
using System.Security.Claims;
using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Models;
using IKP.Application.Diagnostics.Services;
using IKP.Domain.Common.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace IKP.Api.Diagnostics
{
    /// <summary>
    /// Handles unhandled HTTP request exceptions and registers them in diagnostics.
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ICorrelationIdAccessor correlationIdAccessor;
        private readonly ILogger<GlobalExceptionHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
        /// </summary>
        /// <param name="serviceScopeFactory">Service scope factory.</param>
        /// <param name="correlationIdAccessor">Correlation ID accessor.</param>
        /// <param name="logger">Application logger.</param>
        public GlobalExceptionHandler(IServiceScopeFactory serviceScopeFactory, ICorrelationIdAccessor correlationIdAccessor, ILogger<GlobalExceptionHandler> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.correlationIdAccessor = correlationIdAccessor;
            this.logger = logger;
        }

        /// <inheritdoc />
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is AuthOperationException authException)
            {
                httpContext.Response.StatusCode = authException.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    status = authException.StatusCode,
                    code = authException.Code,
                    title = authException.Message,
                    requestId = httpContext.TraceIdentifier
                }, cancellationToken);
                return true;
            }

            if (exception is UniversalQueryException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    status = StatusCodes.Status400BadRequest,
                    code = "INVALID_QUERY",
                    title = exception.Message,
                    requestId = httpContext.TraceIdentifier
                }, cancellationToken);
                return true;
            }

            string requestId = httpContext.TraceIdentifier;
            string correlationId = this.correlationIdAccessor.CorrelationId ?? Guid.NewGuid().ToString("N");

            DiagnosticErrorContext context = new()
            {
                ApplicationId = "IKP.Api",
                ApplicationVersion = GetApplicationVersion(),
                Platform = "Server",
                RequestId = requestId,
                CorrelationId = correlationId,
                Endpoint = httpContext.GetEndpoint()?.DisplayName,
                HttpMethod = httpContext.Request.Method,
                Route = httpContext.Request.Path,
                UserId = Guid.TryParse(httpContext.User.FindFirst("sub")?.Value ?? httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid userId) ? userId : null,
                SessionId = httpContext.User.FindFirst("sid")?.Value
            };

            Guid errorGroupId = Guid.Empty;

            try
            {
                using IServiceScope scope = this.serviceScopeFactory.CreateScope();
                IDiagnosticService diagnosticService = scope.ServiceProvider.GetRequiredService<IDiagnosticService>();

                Guid diagnosticAreaId = await diagnosticService.GetDiagnosticAreaIdAsync(DiagnosticAreaCodes.Api, cancellationToken);

                context.DiagnosticAreaId = diagnosticAreaId;

                errorGroupId = await diagnosticService.RegisterErrorAsync(exception, context, cancellationToken);
            }
            catch (Exception diagnosticException)
            {
                this.logger.LogError(diagnosticException, "Failed to register unhandled exception in diagnostics. RequestId: {RequestId}, CorrelationId: {CorrelationId}", requestId, correlationId);
            }

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            ApiErrorResponse response = new()
            {
                Status = StatusCodes.Status500InternalServerError,
                Code = "INTERNAL_ERROR",
                Title = "An unexpected server error occurred.",
                RequestId = requestId,
                CorrelationId = correlationId,
                ErrorGroupId = errorGroupId == Guid.Empty ? null : errorGroupId
            };

            httpContext.Response.Headers[CorrelationIdConstants.HeaderName] = correlationId;

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }

        private static string GetApplicationVersion() => typeof(GlobalExceptionHandler).Assembly.GetName().Version?.ToString() ?? "unknown";
    }
}
