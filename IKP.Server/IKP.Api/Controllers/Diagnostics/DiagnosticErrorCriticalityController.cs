using IKP.Api.Diagnostics.Models.Criticality;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only access to automatically calculated diagnostic criticality.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/error-criticality")]
    public sealed class DiagnosticErrorCriticalityController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticErrorCriticalityController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticErrorCriticalityResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            decimal? minimumScore,
            decimal? maximumScore,
            bool? manualOverride,
            DateTime? calculatedFrom,
            DateTime? calculatedTo,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            IQueryable<Domain.Entities.Diagnostics.Criticality.ErrorCriticality> query = db.ErrorCriticalities.AsNoTracking();

            if (minimumScore.HasValue) query = query.Where(x => x.EffectiveScore >= minimumScore.Value);
            if (maximumScore.HasValue) query = query.Where(x => x.EffectiveScore <= maximumScore.Value);
            if (manualOverride == true) query = query.Where(x => x.ManualScore.HasValue);
            if (manualOverride == false) query = query.Where(x => !x.ManualScore.HasValue);
            if (calculatedFrom.HasValue) query = query.Where(x => x.CalculatedAt >= calculatedFrom.Value);
            if (calculatedTo.HasValue) query = query.Where(x => x.CalculatedAt <= calculatedTo.Value);

            IQueryable<DiagnosticErrorCriticalityResponse> projected = query.Select(x => new DiagnosticErrorCriticalityResponse
            {
                Id = x.Id,
                ErrorGroupId = x.ErrorGroupId,
                ErrorTitle = x.ErrorGroup.Title,
                AutomaticScore = x.AutomaticScore,
                ManualScore = x.ManualScore,
                EffectiveScore = x.EffectiveScore,
                CalculationVersion = x.CalculationVersion,
                CalculatedAt = x.CalculatedAt,
                OverrideUserId = x.OverrideUserId,
                OverrideAt = x.OverrideAt,
                OverrideReason = x.OverrideReason
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "automatic" => request.SortDescending ? projected.OrderByDescending(x => x.AutomaticScore) : projected.OrderBy(x => x.AutomaticScore),
                "effective" => request.SortDescending ? projected.OrderByDescending(x => x.EffectiveScore) : projected.OrderBy(x => x.EffectiveScore),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.CalculatedAt) : projected.OrderBy(x => x.CalculatedAt)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{errorGroupId:guid}")]
        public async Task<ActionResult<DiagnosticErrorCriticalityResponse>> GetByGroup(Guid errorGroupId, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            DiagnosticErrorCriticalityResponse? item = await db.ErrorCriticalities
                .AsNoTracking()
                .Where(x => x.ErrorGroupId == errorGroupId)
                .Select(x => new DiagnosticErrorCriticalityResponse
                {
                    Id = x.Id,
                    ErrorGroupId = x.ErrorGroupId,
                    ErrorTitle = x.ErrorGroup.Title,
                    AutomaticScore = x.AutomaticScore,
                    ManualScore = x.ManualScore,
                    EffectiveScore = x.EffectiveScore,
                    CalculationVersion = x.CalculationVersion,
                    CalculatedAt = x.CalculatedAt,
                    OverrideUserId = x.OverrideUserId,
                    OverrideAt = x.OverrideAt,
                    OverrideReason = x.OverrideReason,
                    Factors = x.Factors.OrderBy(y => y.Definition.Code).Select(y => new DiagnosticErrorCriticalityFactorResponse
                    {
                        Id = y.Id,
                        DefinitionId = y.DefinitionId,
                        DefinitionCode = y.Definition.Code,
                        Score = y.Score,
                        Weight = y.Weight,
                        Contribution = y.Contribution,
                        Value = y.Value,
                        CalculatedAt = y.CalculatedAt
                    }).ToList()
                })
                .SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}