using IKP.Application.Diagnostics.Fingerprinting.Interfaces;
using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Models;
using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Domain.ValueObjects.Diagnostics;
using System.Text.Json;

namespace IKP.Application.Diagnostics.Services
{
    /// <summary>
    /// Registers application errors in the diagnostic subsystem.
    /// </summary>
    public class DiagnosticService : IDiagnosticService
    {
        private const string SeverityGroupCode = "DIAGNOSTIC_SEVERITY";
        private const string DefaultSeverityCode = DiagnosticSeverityCodes.Error;

        private const string StatusGroupCode = "ERROR_GROUP_STATUS";
        private const string DefaultStatusCode = ErrorGroupStatusCodes.New;

        private readonly IDiagnosticRepository diagnosticRepository;
        private readonly IErrorFingerprintService fingerprintService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticService"/> class.
        /// </summary>
        /// <param name="diagnosticRepository">Diagnostic repository.</param>
        /// <param name="fingerprintService">Fingerprint service.</param>
        public DiagnosticService(IDiagnosticRepository diagnosticRepository, IErrorFingerprintService fingerprintService)
        {
            this.diagnosticRepository = diagnosticRepository;
            this.fingerprintService = fingerprintService;
        }

        /// <inheritdoc />
        public async ValueTask<Guid> RegisterErrorAsync(Exception exception, DiagnosticErrorContext context, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(exception);
            ArgumentNullException.ThrowIfNull(context);

            ErrorExceptionDetails exceptionDetails = CreateExceptionDetails(exception);
            string fingerprint = fingerprintService.CreateFingerprint(exceptionDetails);
            ErrorGroup? errorGroup = await diagnosticRepository.FindErrorGroupAsync(fingerprint, fingerprintService.Version, cancellationToken);

            DateTime occurredAt = DateTime.UtcNow;

            if (errorGroup is null)
            {
                errorGroup = await CreateErrorGroupAsync(fingerprint, exceptionDetails, context, occurredAt, cancellationToken);

                diagnosticRepository.AddErrorGroup(errorGroup);
            }
            else
            {
                UpdateErrorGroup(errorGroup, context, occurredAt);
            }

            ErrorOccurrence errorOccurrence = CreateErrorOccurrence(errorGroup, exceptionDetails, context, occurredAt);

            diagnosticRepository.AddErrorOccurrence(errorOccurrence);
            await diagnosticRepository.SaveChangesAsync(cancellationToken);

            return errorGroup.Id;
        }

        /// <summary>
        /// Creates a new error group.
        /// </summary>
        /// <param name="fingerprint">Error fingerprint.</param>
        /// <param name="exceptionDetails">Exception details.</param>
        /// <param name="context">Diagnostic context.</param>
        /// <param name="occurredAt">Occurrence timestamp.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A new error group.</returns>
        private async ValueTask<ErrorGroup> CreateErrorGroupAsync(string fingerprint, ErrorExceptionDetails exceptionDetails, DiagnosticErrorContext context, DateTime occurredAt, CancellationToken cancellationToken)
        {
            Guid severityId = await diagnosticRepository.GetDefinitionIdAsync(SeverityGroupCode, DefaultSeverityCode, cancellationToken);
            Guid statusId = await diagnosticRepository.GetDefinitionIdAsync(StatusGroupCode, DefaultStatusCode, cancellationToken);

            ErrorGroup errorGroup = new()
            {
                Fingerprint = fingerprint,
                FingerprintVersion = fingerprintService.Version,
                ApplicationId = context.ApplicationId,
                DiagnosticAreaId = context.DiagnosticAreaId,
                ExceptionType = exceptionDetails.ExceptionType,
                ErrorCode = context.ErrorCode,
                Title = CreateTitle(exceptionDetails),
                Summary = CreateSummary(exceptionDetails),
                SeverityId = severityId,
                StatusId = statusId,
                FirstOccurredAt = occurredAt,
                LastOccurredAt = occurredAt,
                FirstSeenVersion = context.ApplicationVersion,
                LastSeenVersion = context.ApplicationVersion
            };

            return errorGroup;
        }

        /// <summary>
        /// Updates an existing error group after another occurrence.
        /// </summary>
        /// <param name="errorGroup">Error group to update.</param>
        /// <param name="context">Diagnostic context.</param>
        /// <param name="occurredAt">Occurrence timestamp.</param>
        private static void UpdateErrorGroup(ErrorGroup errorGroup, DiagnosticErrorContext context, DateTime occurredAt)
        {
            errorGroup.LastOccurredAt = occurredAt;
            errorGroup.LastSeenVersion = context.ApplicationVersion;

            if (!string.IsNullOrWhiteSpace(context.ApplicationId)) errorGroup.ApplicationId = context.ApplicationId;
            if (context.DiagnosticAreaId.HasValue) errorGroup.DiagnosticAreaId = context.DiagnosticAreaId;
            if (!string.IsNullOrWhiteSpace(context.ErrorCode)) errorGroup.ErrorCode = context.ErrorCode;
        }

        /// <summary>
        /// Creates an error occurrence.
        /// </summary>
        /// <param name="errorGroup">Error group.</param>
        /// <param name="exceptionDetails">Exception details.</param>
        /// <param name="context">Diagnostic context.</param>
        /// <param name="occurredAt">Occurrence timestamp.</param>
        /// <returns>A new error occurrence.</returns>
        private static ErrorOccurrence CreateErrorOccurrence(ErrorGroup errorGroup, ErrorExceptionDetails exceptionDetails, DiagnosticErrorContext context, DateTime occurredAt)
        {
            ErrorOccurrence errorOccurrence = new()
            {
                ErrorGroupId = errorGroup.Id,
                OccurredAt = occurredAt,
                Actor = new ErrorActor
                {
                    UserId = context.UserId,
                    SessionId = context.SessionId
                },
                Application = new ErrorApplicationContext
                {
                    ApplicationId = context.ApplicationId,
                    ApplicationVersion = context.ApplicationVersion,
                    Platform = context.Platform,
                    MachineId = context.MachineId,
                    ClientInstanceId = context.ClientInstanceId
                },
                Request = new ErrorRequestContext
                {
                    RequestId = context.RequestId,
                    CorrelationId = context.CorrelationId,
                    Endpoint = context.Endpoint,
                    HttpMethod = context.HttpMethod,
                    Route = context.Route
                },
                Exception = exceptionDetails,
                Context = CreateContextDocument(context.Context)
            };

            return errorOccurrence;
        }

        /// <summary>
        /// Creates exception details from an exception hierarchy.
        /// </summary>
        /// <param name="exception">Exception to convert.</param>
        /// <returns>Diagnostic exception details.</returns>
        private static ErrorExceptionDetails CreateExceptionDetails(Exception exception)
        {
            Exception rootException = GetRootException(exception);

            return new ErrorExceptionDetails
            {
                ExceptionType = rootException.GetType().FullName ?? rootException.GetType().Name,
                Message = rootException.Message,
                StackTrace = rootException.StackTrace ?? exception.StackTrace,
                SourceFile = null,
                SourceMethod = null,
                SourceLineNumber = null
            };
        }

        /// <summary>
        /// Gets the deepest inner exception.
        /// </summary>
        /// <param name="exception">Exception.</param>
        /// <returns>The deepest inner exception.</returns>
        private static Exception GetRootException(Exception exception)
        {
            Exception current = exception;

            while (current.InnerException is not null)
            {
                current = current.InnerException;
            }

            return current;
        }

        /// <summary>
        /// Creates a title for an error group.
        /// </summary>
        /// <param name="exceptionDetails">Exception details.</param>
        /// <returns>Error title.</returns>
        private static string? CreateTitle(ErrorExceptionDetails exceptionDetails) => 
            exceptionDetails?.ExceptionType?.Length <= 500 ? exceptionDetails.ExceptionType : exceptionDetails?.ExceptionType?[..500];

        /// <summary>
        /// Creates a summary for an error group.
        /// </summary>
        /// <param name="exceptionDetails">Exception details.</param>
        /// <returns>Error summary.</returns>
        private static string? CreateSummary(ErrorExceptionDetails exceptionDetails) => 
            string.IsNullOrWhiteSpace(exceptionDetails.Message) 
                ? exceptionDetails.ExceptionType 
                : exceptionDetails.Message.Length <= 4000
                    ? exceptionDetails.Message
                    : exceptionDetails.Message[..4000];

        /// <summary>
        /// Creates a JSON context document.
        /// </summary>
        /// <param name="context">Additional context.</param>
        /// <returns>JSON context document.</returns>
        private static JsonDocument? CreateContextDocument(object? context) => context is null ? null : JsonDocument.Parse(JsonSerializer.Serialize(context));
    }
}
