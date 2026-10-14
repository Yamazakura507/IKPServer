using IKP.Api.Diagnostics.Models.Area;
using IKP.Api.Infrastructure.Query;
using IKP.Api.Models.Query;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics
{
    /// <summary>
    /// Provides read-only querying of diagnostic definitions.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/definitions")]
    public sealed class DiagnosticDefinitionsController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticDefinitionsController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticDefinitionResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            Guid? groupId,
            bool? isActive,
            bool? isSystem,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<Domain.Entities.Diagnostics.Definitions.DiagnosticDefinition> query = db.DiagnosticDefinitions.AsNoTracking();

            if (groupId.HasValue) query = query.Where(x => x.GroupId == groupId.Value);
            if (isActive.HasValue) query = query.Where(x => x.IsActive == isActive.Value);
            if (isSystem.HasValue) query = query.Where(x => x.IsSystem == isSystem.Value);
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.Trim();
                query = query.Where(x => x.Code.Contains(search) || x.NameKey.Contains(search) || (x.DescriptionKey != null && x.DescriptionKey.Contains(search)));
            }

            IQueryable<DiagnosticDefinitionResponse> projected = query.Select(x => new DiagnosticDefinitionResponse
            {
                Id = x.Id, GroupId = x.GroupId, GroupCode = x.Group.Code, Code = x.Code, NameKey = x.NameKey,
                DescriptionKey = x.DescriptionKey, FileId = x.FileId, SortOrder = x.SortOrder, IsActive = x.IsActive, IsSystem = x.IsSystem
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "group" => request.SortDescending ? projected.OrderByDescending(x => x.GroupCode) : projected.OrderBy(x => x.GroupCode),
                "code" => request.SortDescending ? projected.OrderByDescending(x => x.Code) : projected.OrderBy(x => x.Code),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.SortOrder) : projected.OrderBy(x => x.SortOrder)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticDefinitionResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            DiagnosticDefinitionResponse? item = await db.DiagnosticDefinitions.AsNoTracking().Where(x => x.Id == id).Select(x => new DiagnosticDefinitionResponse
            {
                Id = x.Id, GroupId = x.GroupId, GroupCode = x.Group.Code, Code = x.Code, NameKey = x.NameKey,
                DescriptionKey = x.DescriptionKey, FileId = x.FileId, SortOrder = x.SortOrder, IsActive = x.IsActive, IsSystem = x.IsSystem
            }).SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}