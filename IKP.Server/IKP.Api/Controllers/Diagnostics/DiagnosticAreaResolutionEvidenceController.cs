using IKP.Api.Diagnostics.Models.Area;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only querying of immutable diagnostic area resolution evidence.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/area-resolution-evidence")]
    public sealed class DiagnosticAreaResolutionEvidenceController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticAreaResolutionEvidenceController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticAreaResolutionEvidenceResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            Guid? resolutionId,
            Guid? ruleId,
            Guid? mappingId,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<Domain.Entities.Diagnostics.Areas.DiagnosticAreaResolutionEvidence> query = db.DiagnosticAreaResolutionEvidences.AsNoTracking();

            if (resolutionId.HasValue) query = query.Where(x => x.DiagnosticAreaResolutionId == resolutionId.Value);
            if (ruleId.HasValue) query = query.Where(x => x.RuleId == ruleId.Value);
            if (mappingId.HasValue) query = query.Where(x => x.MappingId == mappingId.Value);
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.Trim();
                query = query.Where(x => x.Pattern.Contains(search) || x.MatchedValue.Contains(search));
            }

            IQueryable<DiagnosticAreaResolutionEvidenceResponse> projected = query.Select(x => new DiagnosticAreaResolutionEvidenceResponse
            {
                Id = x.Id,
                DiagnosticAreaResolutionId = x.DiagnosticAreaResolutionId,
                MappingId = x.MappingId,
                RuleId = x.RuleId,
                RuleCode = x.Rule.Code,
                Pattern = x.Pattern,
                MatchedValue = x.MatchedValue,
                Priority = x.Priority,
                Confidence = x.Confidence,
                Contribution = x.Contribution
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "priority" => request.SortDescending ? projected.OrderByDescending(x => x.Priority) : projected.OrderBy(x => x.Priority),
                "confidence" => request.SortDescending ? projected.OrderByDescending(x => x.Confidence) : projected.OrderBy(x => x.Confidence),
                "contribution" => request.SortDescending ? projected.OrderByDescending(x => x.Contribution) : projected.OrderBy(x => x.Contribution),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.Id) : projected.OrderBy(x => x.Id)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticAreaResolutionEvidenceResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            DiagnosticAreaResolutionEvidenceResponse? item = await db.DiagnosticAreaResolutionEvidences.AsNoTracking().Where(x => x.Id == id).Select(x => new DiagnosticAreaResolutionEvidenceResponse
            {
                Id = x.Id, DiagnosticAreaResolutionId = x.DiagnosticAreaResolutionId, MappingId = x.MappingId, RuleId = x.RuleId,
                RuleCode = x.Rule.Code, Pattern = x.Pattern, MatchedValue = x.MatchedValue, Priority = x.Priority,
                Confidence = x.Confidence, Contribution = x.Contribution
            }).SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}