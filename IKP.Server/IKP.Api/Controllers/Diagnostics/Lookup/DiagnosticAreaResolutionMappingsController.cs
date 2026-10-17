using IKP.Application.Auth.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Api.Diagnostics.Models.Area;
using IKP.Domain.Entities.Diagnostics.Definitions;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics.Lookup
{
    /// <summary>
    /// Provides management operations for diagnostic area resolution mappings.
    /// </summary>
    [ApiController]
[Authorize(Policy = AuthorizationPolicyNames.DiagnosticsRead)]
    [Route("api/diagnostics/resolution-mappings")]
    public sealed class DiagnosticAreaResolutionMappingsController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticAreaResolutionMappingsController"/> class.
        /// </summary>
        /// <param name="dbContextFactory">Application database context factory.</param>
        public DiagnosticAreaResolutionMappingsController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        private static readonly IReadOnlyDictionary<string, Expression<Func<DiagnosticAreaResolutionMapping, object?>>> Fields =
            new Dictionary<string, Expression<Func<DiagnosticAreaResolutionMapping, object?>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = x => x.Id,
                ["diagnosticAreaId"] = x => x.DiagnosticAreaId,
                ["diagnosticAreaCode"] = x => x.DiagnosticArea.Code,
                ["ruleId"] = x => x.RuleId,
                ["ruleCode"] = x => x.Rule.Code,
                ["pattern"] = x => x.Pattern,
                ["priority"] = x => x.Priority,
                ["confidence"] = x => x.Confidence,
                ["isActive"] = x => x.IsActive,
                ["isSystem"] = x => x.IsSystem
            };

        /// <summary>
        /// Queries diagnostic area resolution mappings with recursive filters, multi-column sorting, search and pagination.
        /// </summary>
        [HttpPost("query")]
        public async Task<ActionResult<PagedResponse<DiagnosticAreaResolutionMappingResponse>>> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            PagedResponse<DiagnosticAreaResolutionMappingResponse> response = await db.DiagnosticAreaResolutionMappings
                .AsNoTracking()
                .ToPagedResponseAsync(
                    request,
                    Fields,
                    x => new DiagnosticAreaResolutionMappingResponse
                    {
                        Id = x.Id,
                        DiagnosticAreaId = x.DiagnosticAreaId,
                        DiagnosticAreaCode = x.DiagnosticArea.Code,
                        RuleId = x.RuleId,
                        RuleCode = x.Rule.Code,
                        Pattern = x.Pattern,
                        Priority = x.Priority,
                        Confidence = x.Confidence,
                        IsActive = x.IsActive,
                        IsSystem = x.IsSystem
                    },
                    cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets lookup values used to create and edit mappings.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Available diagnostic areas and resolution rules.</returns>
        [HttpGet("options")]
        public async Task<ActionResult<DiagnosticAreaResolutionMappingOptionsResponse>> GetOptions(CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            List<DiagnosticAreaOptionResponse> areas = await db.DiagnosticAreas
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Code)
                .Select(x => new DiagnosticAreaOptionResponse
                {
                    Id = x.Id,
                    Code = x.Code
                })
                .ToListAsync(cancellationToken);

            List<DiagnosticAreaResolutionRuleOptionResponse> rules = await db.DiagnosticAreaResolutionRules
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.Code)
                .Select(x => new DiagnosticAreaResolutionRuleOptionResponse
                {
                    Id = x.Id,
                    Code = x.Code
                })
                .ToListAsync(cancellationToken);

            return Ok(new DiagnosticAreaResolutionMappingOptionsResponse
            {
                Areas = areas,
                Rules = rules
            });
        }

        /// <summary>
        /// Gets a diagnostic area resolution mapping by identifier.
        /// </summary>
        /// <param name="id">Mapping identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The mapping.</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticAreaResolutionMappingResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            DiagnosticAreaResolutionMappingResponse? mapping = await db.DiagnosticAreaResolutionMappings
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new DiagnosticAreaResolutionMappingResponse
                {
                    Id = x.Id,
                    DiagnosticAreaId = x.DiagnosticAreaId,
                    DiagnosticAreaCode = x.DiagnosticArea.Code,
                    RuleId = x.RuleId,
                    RuleCode = x.Rule.Code,
                    Pattern = x.Pattern,
                    Priority = x.Priority,
                    Confidence = x.Confidence,
                    IsActive = x.IsActive,
                    IsSystem = x.IsSystem
                })
                .SingleOrDefaultAsync(cancellationToken);

            return mapping is null ? NotFound() : Ok(mapping);
        }

        /// <summary>
        /// Creates a user-defined diagnostic area resolution mapping.
        /// </summary>
        /// <param name="request">Mapping creation request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The created mapping.</returns>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicyNames.DiagnosticsManage)]
        public async Task<ActionResult<DiagnosticAreaResolutionMappingResponse>> Create([FromBody] CreateDiagnosticAreaResolutionMappingRequest request, CancellationToken cancellationToken)
        {
            ValidationProblemDetails? validation = ValidateRequest(request.Pattern, request.Priority, request.Confidence);
            if (validation is not null) return BadRequest(validation);

            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            if (!await db.DiagnosticAreas.AnyAsync(x => x.Id == request.DiagnosticAreaId && x.IsActive, cancellationToken))
                return BadRequest("The specified diagnostic area does not exist or is inactive.");

            if (!await db.DiagnosticAreaResolutionRules.AnyAsync(x => x.Id == request.RuleId && x.IsActive, cancellationToken))
                return BadRequest("The specified diagnostic area resolution rule does not exist or is inactive.");

            bool duplicate = await db.DiagnosticAreaResolutionMappings.AnyAsync(
                x => x.DiagnosticAreaId == request.DiagnosticAreaId && x.RuleId == request.RuleId && x.Pattern == request.Pattern,
                cancellationToken);

            if (duplicate) return Conflict("A mapping with the same diagnostic area, rule and pattern already exists.");

            DiagnosticAreaResolutionMapping mapping = new()
            {
                DiagnosticAreaId = request.DiagnosticAreaId,
                RuleId = request.RuleId,
                Pattern = request.Pattern,
                Priority = request.Priority,
                Confidence = request.Confidence,
                IsActive = request.IsActive,
                IsSystem = false
            };

            db.DiagnosticAreaResolutionMappings.Add(mapping);
            await db.SaveChangesAsync(cancellationToken);

            DiagnosticAreaResolutionMappingResponse response = await GetResponseAsync(db, mapping.Id, cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = mapping.Id }, response);
        }

        /// <summary>
        /// Updates a user-defined diagnostic area resolution mapping.
        /// </summary>
        /// <param name="id">Mapping identifier.</param>
        /// <param name="request">Mapping update request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The updated mapping.</returns>
        [HttpPut("{id:guid}")]
        [Authorize(Policy = AuthorizationPolicyNames.DiagnosticsManage)]
        public async Task<ActionResult<DiagnosticAreaResolutionMappingResponse>> Update(Guid id, [FromBody] UpdateDiagnosticAreaResolutionMappingRequest request, CancellationToken cancellationToken)
        {
            ValidationProblemDetails? validation = ValidateRequest(request.Pattern, request.Priority, request.Confidence);

            if (validation is not null) return BadRequest(validation);

            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            DiagnosticAreaResolutionMapping? mapping = await db.DiagnosticAreaResolutionMappings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (mapping is null) return NotFound();
            if (mapping.IsSystem) return Conflict("System diagnostic area resolution mappings cannot be modified.");

            if (!await db.DiagnosticAreas.AnyAsync(x => x.Id == request.DiagnosticAreaId && x.IsActive, cancellationToken))
                return BadRequest("The specified diagnostic area does not exist or is inactive.");

            if (!await db.DiagnosticAreaResolutionRules.AnyAsync(x => x.Id == request.RuleId && x.IsActive, cancellationToken))
                return BadRequest("The specified diagnostic area resolution rule does not exist or is inactive.");

            bool duplicate = await db.DiagnosticAreaResolutionMappings.AnyAsync(
                x => x.Id != id && x.DiagnosticAreaId == request.DiagnosticAreaId && x.RuleId == request.RuleId && x.Pattern == request.Pattern,
                cancellationToken);

            if (duplicate) return Conflict("A mapping with the same diagnostic area, rule and pattern already exists.");

            mapping.DiagnosticAreaId = request.DiagnosticAreaId;
            mapping.RuleId = request.RuleId;
            mapping.Pattern = request.Pattern;
            mapping.Priority = request.Priority;
            mapping.Confidence = request.Confidence;
            mapping.IsActive = request.IsActive;

            await db.SaveChangesAsync(cancellationToken);

            return Ok(await GetResponseAsync(db, mapping.Id, cancellationToken));
        }

        /// <summary>
        /// Deletes a user-defined diagnostic area resolution mapping.
        /// </summary>
        /// <param name="id">Mapping identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content when deletion succeeds.</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Policy = AuthorizationPolicyNames.DiagnosticsManage)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            DiagnosticAreaResolutionMapping? mapping = await db.DiagnosticAreaResolutionMappings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (mapping is null) return NotFound();
            if (mapping.IsSystem) return Conflict("System diagnostic area resolution mappings cannot be deleted.");

            db.DiagnosticAreaResolutionMappings.Remove(mapping);
            await db.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Activates a user-defined diagnostic area resolution mapping.
        /// </summary>
        /// <param name="id">Mapping identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The activated mapping.</returns>
        [HttpPost("{id:guid}/activate")]
        [Authorize(Policy = AuthorizationPolicyNames.DiagnosticsManage)]
        public Task<ActionResult<DiagnosticAreaResolutionMappingResponse>> Activate(Guid id, CancellationToken cancellationToken) => SetActiveStateAsync(id, true, cancellationToken);

        /// <summary>
        /// Deactivates a user-defined diagnostic area resolution mapping.
        /// </summary>
        /// <param name="id">Mapping identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The deactivated mapping.</returns>
        [HttpPost("{id:guid}/deactivate")]
        [Authorize(Policy = AuthorizationPolicyNames.DiagnosticsManage)]
        public Task<ActionResult<DiagnosticAreaResolutionMappingResponse>> Deactivate(Guid id, CancellationToken cancellationToken) => SetActiveStateAsync(id, false, cancellationToken);

        private async Task<ActionResult<DiagnosticAreaResolutionMappingResponse>> SetActiveStateAsync(Guid id, bool isActive, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
            DiagnosticAreaResolutionMapping? mapping = await db.DiagnosticAreaResolutionMappings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (mapping is null) return NotFound();
            if (mapping.IsSystem) return Conflict("System diagnostic area resolution mappings cannot be modified.");

            mapping.IsActive = isActive;
            await db.SaveChangesAsync(cancellationToken);

            return Ok(await GetResponseAsync(db, mapping.Id, cancellationToken));
        }

        private static ValidationProblemDetails? ValidateRequest(string pattern, int priority, decimal confidence)
        {
            Dictionary<string, string[]> errors = new();

            if (string.IsNullOrWhiteSpace(pattern)) errors["pattern"] = ["Pattern is required."];
            else if (pattern.Length > 2000) errors["pattern"] = ["Pattern cannot exceed 2000 characters."];
            else
            {
                try
                {
                    _ = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));
                }
                catch (ArgumentException)
                {
                    errors["pattern"] = ["Pattern is not a valid regular expression."];
                }
            }

            if (priority is < 0 or > 100) errors["priority"] = ["Priority must be between 0 and 100."];
            if (confidence < 0m || confidence > 100m) errors["confidence"] = ["Confidence must be between 0 and 100."];

            return errors.Count == 0 ? null : new ValidationProblemDetails(errors);
        }

        private static async Task<DiagnosticAreaResolutionMappingResponse> GetResponseAsync(AppDbContext db, Guid id, CancellationToken cancellationToken)
        {
            return await db.DiagnosticAreaResolutionMappings
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new DiagnosticAreaResolutionMappingResponse
                {
                    Id = x.Id,
                    DiagnosticAreaId = x.DiagnosticAreaId,
                    DiagnosticAreaCode = x.DiagnosticArea.Code,
                    RuleId = x.RuleId,
                    RuleCode = x.Rule.Code,
                    Pattern = x.Pattern,
                    Priority = x.Priority,
                    Confidence = x.Confidence,
                    IsActive = x.IsActive,
                    IsSystem = x.IsSystem
                })
                .SingleAsync(cancellationToken);
        }
    }
}
