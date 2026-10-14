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
    /// Provides read-only querying of individual diagnostic error occurrences.
    /// </summary>
    [ApiController]
    [Route("api/diagnostics/error-occurrences")]
    public sealed class DiagnosticErrorOccurrencesController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;

        public DiagnosticErrorOccurrencesController(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiagnosticErrorOccurrenceResponse>>> Get(
            [FromQuery] PagedQueryRequest request,
            Guid? errorGroupId,
            Guid? userId,
            string? applicationId,
            string? endpoint,
            string? httpMethod,
            DateTime? occurredFrom,
            DateTime? occurredTo,
            CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            IQueryable<ErrorOccurrence> query = db.ErrorOccurrences.AsNoTracking();

            if (errorGroupId.HasValue) query = query.Where(x => x.ErrorGroupId == errorGroupId.Value);
            if (userId.HasValue) query = query.Where(x => x.Actor.UserId == userId.Value);
            if (!string.IsNullOrWhiteSpace(applicationId)) query = query.Where(x => x.Application.ApplicationId == applicationId);
            if (!string.IsNullOrWhiteSpace(endpoint)) query = query.Where(x => x.Request.Endpoint == endpoint);
            if (!string.IsNullOrWhiteSpace(httpMethod)) query = query.Where(x => x.Request.HttpMethod == httpMethod);
            if (occurredFrom.HasValue) query = query.Where(x => x.OccurredAt >= occurredFrom.Value);
            if (occurredTo.HasValue) query = query.Where(x => x.OccurredAt <= occurredTo.Value);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                string search = request.Search.Trim();
                query = query.Where(x => (x.Request.Endpoint != null && x.Request.Endpoint.Contains(search))
                    || (x.Request.Route != null && x.Request.Route.Contains(search))
                    || (x.Exception.ExceptionType != null && x.Exception.ExceptionType.Contains(search))
                    || (x.Exception.Message != null && x.Exception.Message.Contains(search)));
            }

            IQueryable<DiagnosticErrorOccurrenceResponse> projected = query.Select(x => new DiagnosticErrorOccurrenceResponse
            {
                Id = x.Id,
                ErrorGroupId = x.ErrorGroupId,
                OccurredAt = x.OccurredAt,
                UserId = x.Actor.UserId,
                SessionId = x.Actor.SessionId,
                ApplicationId = x.Application.ApplicationId,
                ApplicationVersion = x.Application.ApplicationVersion,
                Platform = x.Application.Platform,
                MachineId = x.Application.MachineId,
                ClientInstanceId = x.Application.ClientInstanceId,
                RequestId = x.Request.RequestId,
                CorrelationId = x.Request.CorrelationId,
                Endpoint = x.Request.Endpoint,
                HttpMethod = x.Request.HttpMethod,
                Route = x.Request.Route,
                ExceptionType = x.Exception.ExceptionType,
                Message = x.Exception.Message,
                SourceFile = x.Exception.SourceFile,
                SourceMethod = x.Exception.SourceMethod,
                SourceLineNumber = x.Exception.SourceLineNumber
            });

            projected = request.SortBy?.ToLowerInvariant() switch
            {
                "group" => request.SortDescending ? projected.OrderByDescending(x => x.ErrorGroupId) : projected.OrderBy(x => x.ErrorGroupId),
                "application" => request.SortDescending ? projected.OrderByDescending(x => x.ApplicationId) : projected.OrderBy(x => x.ApplicationId),
                "endpoint" => request.SortDescending ? projected.OrderByDescending(x => x.Endpoint) : projected.OrderBy(x => x.Endpoint),
                _ => request.SortDescending ? projected.OrderByDescending(x => x.OccurredAt) : projected.OrderBy(x => x.OccurredAt)
            };

            return Ok(await projected.ToPagedResponseAsync(request, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DiagnosticErrorOccurrenceResponse>> Get(Guid id, CancellationToken cancellationToken)
        {
            await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

            DiagnosticErrorOccurrenceResponse? item = await db.ErrorOccurrences
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new DiagnosticErrorOccurrenceResponse
                {
                    Id = x.Id,
                    ErrorGroupId = x.ErrorGroupId,
                    OccurredAt = x.OccurredAt,
                    UserId = x.Actor.UserId,
                    SessionId = x.Actor.SessionId,
                    ApplicationId = x.Application.ApplicationId,
                    ApplicationVersion = x.Application.ApplicationVersion,
                    Platform = x.Application.Platform,
                    MachineId = x.Application.MachineId,
                    ClientInstanceId = x.Application.ClientInstanceId,
                    RequestId = x.Request.RequestId,
                    CorrelationId = x.Request.CorrelationId,
                    Endpoint = x.Request.Endpoint,
                    HttpMethod = x.Request.HttpMethod,
                    Route = x.Request.Route,
                    ExceptionType = x.Exception.ExceptionType,
                    Message = x.Exception.Message,
                    SourceFile = x.Exception.SourceFile,
                    SourceMethod = x.Exception.SourceMethod,
                    SourceLineNumber = x.Exception.SourceLineNumber
                })
                .SingleOrDefaultAsync(cancellationToken);

            return item is null ? NotFound() : Ok(item);
        }
    }
}