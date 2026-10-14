using IKP.Api.Diagnostics.Models.Statistics;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only access to automatically calculated diagnostic statistics.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/error-statistics")]
    public sealed class DiagnosticErrorStatisticsController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticErrorStatisticsController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticErrorStatisticsResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            Guid? errorGroupId,
            DateTime? calculatedFrom,
            DateTime? calculatedTo,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            IQueryable<Domain.Entities.Diagnostics.Statistics.ErrorStatistics> query = db.ErrorStatistics.AsNoTracking();

            if (errorGroupId.HasValue) query = query.Where(x => x.ErrorGroupId == errorGroupId.Value);
            if (calculatedFrom.HasValue) query = query.Where(x => x.CalculatedAt >= calculatedFrom.Value);
            if (calculatedTo.HasValue) query = query.Where(x => x.CalculatedAt <= calculatedTo.Value);

            IQueryable<DiagnosticErrorStatisticsResponse> projected = query.Select(x => new DiagnosticErrorStatisticsResponse
            {
                Id = x.Id,
                ErrorGroupId = x.ErrorGroupId,
                ErrorTitle = x.ErrorGroup.Title,
                ExceptionType = x.ErrorGroup.ExceptionType,
                TotalOccurrences = x.TotalOccurrences,
                UniqueUsers = x.UniqueUsers,
                AffectedUserRatio = x.AffectedUserRatio,
                OccurrencesLastHour = x.OccurrencesLastHour,
                OccurrencesLastDay = x.OccurrencesLastDay,
                OccurrencesLastWeek = x.OccurrencesLastWeek,
                UsersLastHour = x.UsersLastHour,
                UsersLastDay = x.UsersLastDay,
                UsersLastWeek = x.UsersLastWeek,
                GrowthRate = x.GrowthRate,
                CalculatedAt = x.CalculatedAt
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "totaloccurrences" => request.SortDescending ? projected.OrderByDescending(x => x.TotalOccurrences) : projected.OrderBy(x => x.TotalOccurrences),
                "uniqueusers" => request.SortDescending ? projected.OrderByDescending(x => x.UniqueUsers) : projected.OrderBy(x => x.UniqueUsers),
                "growth" => request.SortDescending ? projected.OrderByDescending(x => x.GrowthRate) : projected.OrderBy(x => x.GrowthRate),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.CalculatedAt) : projected.OrderBy(x => x.CalculatedAt)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{errorGroupId:guid}")]
        public async Task<ActionResult<DiagnosticErrorStatisticsResponse>> GetByGroup(Guid errorGroupId, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            DiagnosticErrorStatisticsResponse? item = await db.ErrorStatistics
                .AsNoTracking()
                .Where(x => x.ErrorGroupId == errorGroupId)
                .Select(x => new DiagnosticErrorStatisticsResponse
                {
                    Id = x.Id,
                    ErrorGroupId = x.ErrorGroupId,
                    ErrorTitle = x.ErrorGroup.Title,
                    ExceptionType = x.ErrorGroup.ExceptionType,
                    TotalOccurrences = x.TotalOccurrences,
                    UniqueUsers = x.UniqueUsers,
                    AffectedUserRatio = x.AffectedUserRatio,
                    OccurrencesLastHour = x.OccurrencesLastHour,
                    OccurrencesLastDay = x.OccurrencesLastDay,
                    OccurrencesLastWeek = x.OccurrencesLastWeek,
                    UsersLastHour = x.UsersLastHour,
                    UsersLastDay = x.UsersLastDay,
                    UsersLastWeek = x.UsersLastWeek,
                    GrowthRate = x.GrowthRate,
                    CalculatedAt = x.CalculatedAt
                })
                .SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}