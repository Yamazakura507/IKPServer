using IKP.Application.Auth.Authorization;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Support;

/// <summary>
/// Provides active support lookup values.
/// </summary>
[ApiController]
[Route("api/support/lookups")]
[Authorize(Policy = AuthorizationPolicyNames.SupportRead)]
public sealed class SupportLookupsController : ControllerBase
{
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes the lookup controller.
    /// </summary>
    public SupportLookupsController(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Gets active support statuses.
    /// </summary>
    [HttpGet("statuses")]
    public async Task<IActionResult> GetStatuses(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.SupportTicketStatuses.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.SortOrder).Select(x => new { x.Id, x.Code, x.NameKey, x.DescriptionKey, x.SortOrder, x.IsTerminal }).ToListAsync(cancellationToken));
    }

    /// <summary>
    /// Gets active support priorities.
    /// </summary>
    [HttpGet("priorities")]
    public async Task<IActionResult> GetPriorities(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.SupportTicketPriorities.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Weight).Select(x => new { x.Id, x.Code, x.NameKey, x.DescriptionKey, x.SortOrder, x.Weight }).ToListAsync(cancellationToken));
    }

    /// <summary>
    /// Gets active support categories.
    /// </summary>
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.SupportTicketCategories.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.SortOrder).Select(x => new { x.Id, x.Code, x.NameKey, x.DescriptionKey, x.SortOrder }).ToListAsync(cancellationToken));
    }
}
