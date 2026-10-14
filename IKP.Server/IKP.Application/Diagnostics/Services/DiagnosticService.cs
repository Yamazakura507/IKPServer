using IKP.Application.Diagnostics.Exceptions.Interfaces;
using IKP.Application.Diagnostics.Fingerprinting.Interfaces;
using IKP.Application.Diagnostics.Interfaces;
using IKP.Application.Diagnostics.Models;
using IKP.Application.Diagnostics.Resolvers.Interfaces;
using IKP.Application.Diagnostics.Resolvers.Models;
using IKP.Domain.Common.Diagnostics;
using IKP.Domain.Entities.Diagnostics.Areas;
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
        private const string ExplicitResolutionSourceCode = DiagnosticAreaResolutionSourceCodes.Explicit;
        private const string ExplicitContextResolutionRuleCode = DiagnosticAreaResolutionRuleCodes.ExplicitContext;

        private const string SeverityGroupCode = DiagnosticDefinitionGroupCodes.Severity;
        private const string DefaultSeverityCode = DiagnosticSeverityCodes.Error;

        private const string StatusGroupCode = DiagnosticDefinitionGroupCodes.ErrorGroupStatus;
        private const string DefaultStatusCode = ErrorGroupStatusCodes.New;

        private const decimal ExplicitResolutionConfidence = 100m;
        private const int DiagnosticAreaResolutionVersion = 1;

        private readonly IDiagnosticRepository diagnosticRepository;
        private readonly IErrorFingerprintService fingerprintService;
        private readonly IExceptionDetailsExtractor exceptionDetailsExtractor;
        private readonly IDiagnosticAreaResolver diagnosticAreaResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticService"/> class.
        /// </summary>
        /// <param name="diagnosticRepository">Diagnostic repository</param>
        /// <param name="fingerprintService">Fingerprint service</param>
        /// <param name="exceptionDetailsExtractor">Exception details extractor.</param>
        public DiagnosticService(IDiagnosticRepository diagnosticRepository, IErrorFingerprintService fingerprintService, IExceptionDetailsExtractor exceptionDetailsExtractor, IDiagnosticAreaResolver diagnosticAreaResolver)
        {
            this.diagnosticRepository = diagnosticRepository;
            this.fingerprintService = fingerprintService;
            this.exceptionDetailsExtractor = exceptionDetailsExtractor;
            this.diagnosticAreaResolver = diagnosticAreaResolver;
        }

        /// <inheritdoc />
        public async ValueTask<Guid> RegisterErrorAsync(Exception exception, DiagnosticErrorContext context, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(exception);
            ArgumentNullException.ThrowIfNull(context);

            ErrorExceptionDetails exceptionDetails = exceptionDetailsExtractor.Extract(exception);
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
                await UpdateErrorGroupAsync(errorGroup, exceptionDetails, context, occurredAt, cancellationToken);
            }

            ErrorOccurrence errorOccurrence = CreateErrorOccurrence(errorGroup, exceptionDetails, context, occurredAt);

            diagnosticRepository.AddErrorOccurrence(errorOccurrence);
            await diagnosticRepository.ScheduleStatisticsWorkItemAsync(errorGroup.Id, occurredAt, cancellationToken);
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
            DiagnosticAreaResolution? diagnosticAreaResolution = await ResolveDiagnosticAreaAsync(exceptionDetails, context, cancellationToken);

            ErrorGroup errorGroup = new()
            {
                Fingerprint = fingerprint,
                FingerprintVersion = fingerprintService.Version,
                ApplicationId = context.ApplicationId,
                DiagnosticAreaResolution = diagnosticAreaResolution,
                ExceptionType = CreateExceptionType(exceptionDetails),
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

        /// <inheritdoc />
        public ValueTask<Guid> GetDiagnosticAreaIdAsync(string code, CancellationToken cancellationToken = default) => diagnosticRepository.GetDiagnosticAreaIdAsync(code, cancellationToken);

        /// <summary>
        /// Updates an existing error group after another occurrence.
        /// </summary>
        /// <param name="errorGroup">Error group to update.</param>
        /// <param name="exceptionDetails">Extracted exception details.</param>
        /// <param name="context">Diagnostic context.</param>
        /// <param name="occurredAt">Occurrence timestamp.</param>
        private async ValueTask UpdateErrorGroupAsync(ErrorGroup errorGroup, ErrorExceptionDetails exceptionDetails, DiagnosticErrorContext context, DateTime occurredAt, CancellationToken cancellationToken)
        {
            errorGroup.LastOccurredAt = occurredAt;
            errorGroup.LastSeenVersion = context.ApplicationVersion;

            if (!string.IsNullOrWhiteSpace(context.ApplicationId)) errorGroup.ApplicationId = context.ApplicationId;
            if (!string.IsNullOrWhiteSpace(context.ErrorCode)) errorGroup.ErrorCode = context.ErrorCode;
            if (context.DiagnosticAreaId.HasValue)
            {
                errorGroup.DiagnosticAreaResolution = await CreateExplicitResolutionAsync(context.DiagnosticAreaId, cancellationToken);
            }
            else if (errorGroup.DiagnosticAreaResolution is null)
            {
                errorGroup.DiagnosticAreaResolution = await ResolveDiagnosticAreaAsync(exceptionDetails, context, cancellationToken);
            }
        }

        /// <summary>
        /// Resolves a diagnostic area using explicit context first and configured automatic rules second.
        /// </summary>
        /// <param name="exceptionDetails">Extracted exception details.</param>
        /// <param name="context">Diagnostic context.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The resolved diagnostic area or null when it cannot be resolved.</returns>
        private async ValueTask<DiagnosticAreaResolution?> ResolveDiagnosticAreaAsync(ErrorExceptionDetails exceptionDetails, DiagnosticErrorContext context, CancellationToken cancellationToken)
        {
            if (context.DiagnosticAreaId.HasValue) return await CreateExplicitResolutionAsync(context.DiagnosticAreaId, cancellationToken);

            DiagnosticAreaResolutionInputModel input = new()
            {
                ExceptionType = exceptionDetails.ExceptionType,
                Namespace = CreateExceptionNamespace(exceptionDetails.ExceptionType),
                Endpoint = context.Endpoint,
                Route = context.Route,
                HttpMethod = context.HttpMethod
            };

            return await diagnosticAreaResolver.ResolveAsync(input, cancellationToken);
        }

        /// <summary>
        /// Gets the namespace portion of an exception type.
        /// </summary>
        /// <param name="exceptionType">Exception type name.</param>
        /// <returns>The namespace or null.</returns>
        private static string? CreateExceptionNamespace(string? exceptionType)
        {
            if (string.IsNullOrWhiteSpace(exceptionType)) return null;

            int separatorIndex = exceptionType.LastIndexOf('.');

            return separatorIndex <= 0 ? null : exceptionType[..separatorIndex];
        }

        /// <summary>
        /// Creates an explicit diagnostic area resolution when a diagnostic area was supplied by the caller.
        /// </summary>
        /// <param name="diagnosticAreaId">Diagnostic area identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created resolution or null when no diagnostic area was supplied.</returns>
        private async ValueTask<DiagnosticAreaResolution?> CreateExplicitResolutionAsync(Guid? diagnosticAreaId, CancellationToken cancellationToken) =>
            !diagnosticAreaId.HasValue ? null : await diagnosticRepository.CreateDiagnosticAreaResolutionAsync(
                                                    diagnosticAreaId,
                                                    ExplicitResolutionSourceCode,
                                                    ExplicitContextResolutionRuleCode,
                                                    ExplicitResolutionConfidence,
                                                    DiagnosticAreaResolutionVersion,
                                                    cancellationToken);

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
                DiagnosticAreaResolution = errorGroup.DiagnosticAreaResolution,
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

        /// Creates an exception type value for an error group.
        /// </summary>
        /// <param name="exceptionDetails">Exception details.</param>
        /// <returns>Exception type.</returns>
        private static string CreateExceptionType(ErrorExceptionDetails exceptionDetails) => 
            string.IsNullOrWhiteSpace(exceptionDetails.ExceptionType) ? "UnknownException" : exceptionDetails.ExceptionType;

        /// <summary>
        /// Creates a title for an error group.
        /// </summary>
        /// <param name="exceptionDetails">Exception details.</param>
        /// <returns>Error title.</returns>
        private static string? CreateTitle(ErrorExceptionDetails exceptionDetails)
        {
            string exceptionType = CreateExceptionType(exceptionDetails);

            return exceptionType.Length <= 500 ? exceptionType : exceptionType[..500];
        }

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
