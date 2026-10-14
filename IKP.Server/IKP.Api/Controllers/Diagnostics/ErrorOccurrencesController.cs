using System.Linq.Expressions;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Common.Query;
using IKP.Domain.Entities.Diagnostics.Errors;
using IKP.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IKP.Api.Controllers.Diagnostics;

/// <summary>Provides diagnostic error occurrence read operations.</summary>
[ApiController]
[Route("api/diagnostics/error-occurrences")]
public sealed class ErrorOccurrencesController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<ErrorOccurrence, object?>>> Fields = new Dictionary<string, Expression<Func<ErrorOccurrence, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id, ["errorGroupId"] = x => x.ErrorGroupId, ["diagnosticAreaResolutionId"] = x => x.DiagnosticAreaResolutionId, ["occurredAt"] = x => x.OccurredAt,
        ["userId"] = x => x.Actor.UserId, ["sessionId"] = x => x.Actor.SessionId, ["applicationId"] = x => x.Application.ApplicationId, ["applicationVersion"] = x => x.Application.ApplicationVersion,
        ["platform"] = x => x.Application.Platform, ["requestId"] = x => x.Request.RequestId, ["correlationId"] = x => x.Request.CorrelationId, ["endpoint"] = x => x.Request.Endpoint,
        ["httpMethod"] = x => x.Request.HttpMethod, ["route"] = x => x.Request.Route, ["exceptionType"] = x => x.Exception.ExceptionType, ["exceptionMessage"] = x => x.Exception.Message,
        ["sourceFile"] = x => x.Exception.SourceFile, ["sourceMethod"] = x => x.Exception.SourceMethod
    };
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;
    /// <summary>Initializes a new controller.</summary>
    public ErrorOccurrencesController(IDbContextFactory<AppDbContext> dbContextFactory) => this.dbContextFactory = dbContextFactory;
    /// <summary>Queries error occurrences.</summary>
    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        return Ok(await db.ErrorOccurrences.AsNoTracking().ToPagedResponseAsync(request, Fields, x => new
        {
            x.Id, x.ErrorGroupId, x.DiagnosticAreaResolutionId, x.OccurredAt, UserId = x.Actor.UserId, SessionId = x.Actor.SessionId,
            ApplicationId = x.Application.ApplicationId, ApplicationVersion = x.Application.ApplicationVersion, x.Application.Platform,
            RequestId = x.Request.RequestId, CorrelationId = x.Request.CorrelationId, Endpoint = x.Request.Endpoint, HttpMethod = x.Request.HttpMethod,
            Route = x.Request.Route, ExceptionType = x.Exception.ExceptionType, ExceptionMessage = x.Exception.Message, SourceFile = x.Exception.SourceFile,
            SourceMethod = x.Exception.SourceMethod, SourceLineNumber = x.Exception.SourceLineNumber
        }, cancellationToken));
    }
    /// <summary>Gets one error occurrence.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        object? result = await db.ErrorOccurrences.AsNoTracking().Where(x => x.Id == id).Select(x => new
        {
            x.Id, x.ErrorGroupId, x.DiagnosticAreaResolutionId, x.OccurredAt, x.Context, Actor = x.Actor, Application = x.Application, Request = x.Request, Exception = x.Exception
        }).SingleOrDefaultAsync(cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }
}
