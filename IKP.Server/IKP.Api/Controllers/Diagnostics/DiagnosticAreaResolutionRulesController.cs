using IKP.Api.Diagnostics.Models.Area;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only querying of diagnostic area resolution rules.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/area-resolution-rules")]
    public sealed class DiagnosticAreaResolutionRulesController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticAreaResolutionRulesController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticLookupResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            bool? isActive,
            bool? isSystem,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<Domain.Entities.Diagnostics.Definitions.DiagnosticAreaResolutionRule> query = db.DiagnosticAreaResolutionRules.AsNoTracking();

            if (isActive.HasValue) query = query.Where(x => x.IsActive == isActive.Value);
            if (isSystem.HasValue) query = query.Where(x => x.IsSystem == isSystem.Value);
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.Trim();
                query = query.Where(x => x.Code.Contains(search) || x.NameKey.Contains(search) || (x.DescriptionKey != null && x.DescriptionKey.Contains(search)));
            }

            IQueryable<DiagnosticLookupResponse> projected = query.Select(x => new DiagnosticLookupResponse
            {
                Id = x.Id, Code = x.Code, NameKey = x.NameKey, DescriptionKey = x.DescriptionKey, FileId = x.FileId,
                SortOrder = x.SortOrder, IsActive = x.IsActive, IsSystem = x.IsSystem
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "code" => request.SortDescending ? projected.OrderByDescending(x => x.Code) : projected.OrderBy(x => x.Code),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.SortOrder) : projected.OrderBy(x => x.SortOrder)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticLookupResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            DiagnosticLookupResponse? item = await db.DiagnosticAreaResolutionRules.AsNoTracking().Where(x => x.Id == id).Select(x => new DiagnosticLookupResponse
            {
                Id = x.Id, Code = x.Code, NameKey = x.NameKey, DescriptionKey = x.DescriptionKey, FileId = x.FileId,
                SortOrder = x.SortOrder, IsActive = x.IsActive, IsSystem = x.IsSystem
            }).SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}