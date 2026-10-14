using IKP.Api.Diagnostics.Models.Area;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only querying of persisted diagnostic area resolutions.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/area-resolutions")]
    public sealed class DiagnosticAreaResolutionsController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticAreaResolutionsController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticAreaResolutionResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            Guid? diagnosticAreaId,
            Guid? ruleId,
            Guid? sourceId,
            decimal? minimumConfidence,
            decimal? maximumConfidence,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<Domain.Entities.Diagnostics.Areas.DiagnosticAreaResolution> query = db.DiagnosticAreaResolutions.AsNoTracking();

            if (diagnosticAreaId.HasValue) query = query.Where(x => x.DiagnosticAreaId == diagnosticAreaId.Value);
            if (ruleId.HasValue) query = query.Where(x => x.RuleId == ruleId.Value);
            if (sourceId.HasValue) query = query.Where(x => x.SourceId == sourceId.Value);
            if (minimumConfidence.HasValue) query = query.Where(x => x.Confidence >= minimumConfidence.Value);
            if (maximumConfidence.HasValue) query = query.Where(x => x.Confidence <= maximumConfidence.Value);

            IQueryable<DiagnosticAreaResolutionResponse> projected = query.Select(x => new DiagnosticAreaResolutionResponse
            {
                Id = x.Id,
                DiagnosticAreaId = x.DiagnosticAreaId,
                DiagnosticAreaCode = x.DiagnosticArea == null ? null : x.DiagnosticArea.Code,
                SourceId = x.SourceId,
                SourceCode = x.Source == null ? null : x.Source.Code,
                RuleId = x.RuleId,
                RuleCode = x.Rule == null ? null : x.Rule.Code,
                Confidence = x.Confidence,
                Version = x.Version
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "confidence" => request.SortDescending ? projected.OrderByDescending(x => x.Confidence) : projected.OrderBy(x => x.Confidence),
                "version" => request.SortDescending ? projected.OrderByDescending(x => x.Version) : projected.OrderBy(x => x.Version),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.Id) : projected.OrderBy(x => x.Id)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticAreaResolutionResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            DiagnosticAreaResolutionResponse? item = await db.DiagnosticAreaResolutions.AsNoTracking().Where(x => x.Id == id).Select(x => new DiagnosticAreaResolutionResponse
            {
                Id = x.Id, DiagnosticAreaId = x.DiagnosticAreaId, DiagnosticAreaCode = x.DiagnosticArea == null ? null : x.DiagnosticArea.Code,
                SourceId = x.SourceId, SourceCode = x.Source == null ? null : x.Source.Code, RuleId = x.RuleId,
                RuleCode = x.Rule == null ? null : x.Rule.Code, Confidence = x.Confidence, Version = x.Version
            }).SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}