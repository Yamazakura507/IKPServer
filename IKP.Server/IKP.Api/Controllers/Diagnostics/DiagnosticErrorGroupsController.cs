using IKP.Api.Diagnostics.Models.Errors;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only querying of diagnostic error groups.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/error-groups")]
    public sealed class DiagnosticErrorGroupsController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticErrorGroupsController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticErrorGroupResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            Guid? diagnosticAreaId,
            string? applicationId,
            string? exceptionType,
            string? errorCode,
            Guid? severityId,
            Guid? statusId,
            DateTime? occurredFrom,
            DateTime? occurredTo,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            IQueryable<ErrorGroup> query = db.ErrorGroups.AsNoTracking();

            if (diagnosticAreaId.HasValue) query = query.Where(x => x.DiagnosticAreaResolution != null && x.DiagnosticAreaResolution.DiagnosticAreaId == diagnosticAreaId.Value);
            if (!string.IsNullOrWhiteSpace(applicationId)) query = query.Where(x => x.ApplicationId == applicationId);
            if (!string.IsNullOrWhiteSpace(exceptionType)) query = query.Where(x => x.ExceptionType == exceptionType);
            if (!string.IsNullOrWhiteSpace(errorCode)) query = query.Where(x => x.ErrorCode == errorCode);
            if (severityId.HasValue) query = query.Where(x => x.SeverityId == severityId.Value);
            if (statusId.HasValue) query = query.Where(x => x.StatusId == statusId.Value);
            if (occurredFrom.HasValue) query = query.Where(x => x.LastOccurredAt >= occurredFrom.Value);
            if (occurredTo.HasValue) query = query.Where(x => x.FirstOccurredAt <= occurredTo.Value);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.Trim();
                query = query.Where(x => x.Title.Contains(search) || (x.Summary != null && x.Summary.Contains(search)) || x.ExceptionType.Contains(search) || (x.ErrorCode != null && x.ErrorCode.Contains(search)));
            }

            IQueryable<DiagnosticErrorGroupResponse> projected = query.Select(x => new DiagnosticErrorGroupResponse
            {
                Id = x.Id,
                Fingerprint = x.Fingerprint,
                FingerprintVersion = x.FingerprintVersion,
                ApplicationId = x.ApplicationId,
                DiagnosticAreaId = x.DiagnosticAreaResolution == null ? null : x.DiagnosticAreaResolution.DiagnosticAreaId,
                DiagnosticAreaCode = x.DiagnosticAreaResolution == null || x.DiagnosticAreaResolution.DiagnosticArea == null ? null : x.DiagnosticAreaResolution.DiagnosticArea.Code,
                ExceptionType = x.ExceptionType,
                ErrorCode = x.ErrorCode,
                Title = x.Title,
                Summary = x.Summary,
                SeverityId = x.SeverityId,
                SeverityCode = x.SeverityDefinition.Code,
                StatusId = x.StatusId,
                StatusCode = x.StatusDefinition.Code,
                FirstOccurredAt = x.FirstOccurredAt,
                LastOccurredAt = x.LastOccurredAt,
                FirstSeenVersion = x.FirstSeenVersion,
                LastSeenVersion = x.LastSeenVersion,
                TotalOccurrences = x.Statistics == null ? null : x.Statistics.TotalOccurrences,
                UniqueUsers = x.Statistics == null ? null : x.Statistics.UniqueUsers,
                AffectedUserRatio = x.Statistics == null ? null : x.Statistics.AffectedUserRatio,
                CriticalityScore = x.Criticality == null ? null : x.Criticality.EffectiveScore,
                HasManualCriticalityOverride = x.Criticality != null && x.Criticality.ManualScore.HasValue
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "title" => request.SortDescending ? projected.OrderByDescending(x => x.Title) : projected.OrderBy(x => x.Title),
                "application" => request.SortDescending ? projected.OrderByDescending(x => x.ApplicationId) : projected.OrderBy(x => x.ApplicationId),
                "severity" => request.SortDescending ? projected.OrderByDescending(x => x.SeverityCode) : projected.OrderBy(x => x.SeverityCode),
                "status" => request.SortDescending ? projected.OrderByDescending(x => x.StatusCode) : projected.OrderBy(x => x.StatusCode),
                "criticality" => request.SortDescending ? projected.OrderByDescending(x => x.CriticalityScore) : projected.OrderBy(x => x.CriticalityScore),
                "occurrences" => request.SortDescending ? projected.OrderByDescending(x => x.TotalOccurrences) : projected.OrderBy(x => x.TotalOccurrences),
                "firstoccurredat" => request.SortDescending ? projected.OrderByDescending(x => x.FirstOccurredAt) : projected.OrderBy(x => x.FirstOccurredAt),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.LastOccurredAt) : projected.OrderBy(x => x.LastOccurredAt)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticErrorGroupResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            DiagnosticErrorGroupResponse? item = await db.ErrorGroups
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new DiagnosticErrorGroupResponse
                {
                    Id = x.Id,
                    Fingerprint = x.Fingerprint,
                    FingerprintVersion = x.FingerprintVersion,
                    ApplicationId = x.ApplicationId,
                    DiagnosticAreaId = x.DiagnosticAreaResolution == null ? null : x.DiagnosticAreaResolution.DiagnosticAreaId,
                    DiagnosticAreaCode = x.DiagnosticAreaResolution == null || x.DiagnosticAreaResolution.DiagnosticArea == null ? null : x.DiagnosticAreaResolution.DiagnosticArea.Code,
                    ExceptionType = x.ExceptionType,
                    ErrorCode = x.ErrorCode,
                    Title = x.Title,
                    Summary = x.Summary,
                    SeverityId = x.SeverityId,
                    SeverityCode = x.SeverityDefinition.Code,
                    StatusId = x.StatusId,
                    StatusCode = x.StatusDefinition.Code,
                    FirstOccurredAt = x.FirstOccurredAt,
                    LastOccurredAt = x.LastOccurredAt,
                    FirstSeenVersion = x.FirstSeenVersion,
                    LastSeenVersion = x.LastSeenVersion,
                    TotalOccurrences = x.Statistics == null ? null : x.Statistics.TotalOccurrences,
                    UniqueUsers = x.Statistics == null ? null : x.Statistics.UniqueUsers,
                    AffectedUserRatio = x.Statistics == null ? null : x.Statistics.AffectedUserRatio,
                    CriticalityScore = x.Criticality == null ? null : x.Criticality.EffectiveScore,
                    HasManualCriticalityOverride = x.Criticality != null && x.Criticality.ManualScore.HasValue
                })
                .SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }

        [HttpGet("groups")]
        public async Task<ActionResult<IReadOnlyList<object>>> GetGroups(
            string groupBy = "exceptionType",
            string? applicationId = null,
            CancellationToken cancellationToken = default)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            IQueryable<ErrorGroup> query = db.ErrorGroups.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(applicationId)) query = query.Where(x => x.ApplicationId == applicationId);

            return groupBy.ToLowerInvariant() switch
            {
                "application" => Ok(await query.GroupBy(x => x.ApplicationId).Select(x => new { Key = x.Key, Count = x.LongCount() }).OrderByDescending(x => x.Count).ToListAsync(cancellationToken)),
                "exceptiontype" => Ok(await query.GroupBy(x => x.ExceptionType).Select(x => new { Key = x.Key, Count = x.LongCount() }).OrderByDescending(x => x.Count).ToListAsync(cancellationToken)),
                "errorcode" => Ok(await query.GroupBy(x => x.ErrorCode).Select(x => new { Key = x.Key, Count = x.LongCount() }).OrderByDescending(x => x.Count).ToListAsync(cancellationToken)),
                "severity" => Ok(await query.GroupBy(x => x.SeverityId).Select(x => new { Key = x.Key, Count = x.LongCount() }).OrderByDescending(x => x.Count).ToListAsync(cancellationToken)),
                "status" => Ok(await query.GroupBy(x => x.StatusId).Select(x => new { Key = x.Key, Count = x.LongCount() }).OrderByDescending(x => x.Count).ToListAsync(cancellationToken)),
                _ => BadRequest("Unsupported groupBy value. Supported values: application, exceptionType, errorCode, severity, status.")
            };
        }
    }
}