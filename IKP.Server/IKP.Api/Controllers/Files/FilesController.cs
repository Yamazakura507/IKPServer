using System.Linq.Expressions;
using System.Security.Claims;
using IKP.Api.Infrastructure.Query;
using IKP.Application.Auth.Authorization;
using IKP.Application.Common.Query;
using IKP.Application.Files.Interfaces;
using IKP.Application.Files.Models;
using IKP.Domain.Entities.Files;
using IKP.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKP.Api.Controllers.Files;

/// <summary>
/// Provides streaming file upload, metadata, download, query, and deletion operations.
/// </summary>
[ApiController]
[Route("api/files")]
[Authorize]
public sealed class FilesController : ControllerBase
{
    private static readonly IReadOnlyDictionary<string, Expression<Func<StoredFile, object?>>> Fields = new Dictionary<string, Expression<Func<StoredFile, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        ["id"] = x => x.Id,
        ["blobId"] = x => x.BlobId,
        ["originalFileName"] = x => x.OriginalFileName,
        ["contentType"] = x => x.ContentType,
        ["extension"] = x => x.Extension,
        ["size"] = x => x.Blob.Size,
        ["contentHash"] = x => x.Blob.ContentHash,
        ["createdAt"] = x => x.CreatedAt,
        ["createdBy"] = x => x.CreatedBy
    };

    private readonly IFileService service;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory;

    /// <summary>
    /// Initializes the files controller.
    /// </summary>
    public FilesController(IFileService service, IDbContextFactory<AppDbContext> dbContextFactory)
    {
        this.service = service;
        this.dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Creates an empty streaming upload session.
    /// </summary>
    [HttpPost("upload-sessions")]
    [Authorize(Policy = AuthorizationPolicyNames.FilesCreate)]
    public async Task<ActionResult<FileUploadSessionResponse>> CreateUploadSession([FromBody] CreateFileUploadRequest request, CancellationToken cancellationToken) 
        => Ok(await service.CreateUploadSessionAsync(request, GetRequiredUserId(), cancellationToken));

    /// <summary>
    /// Appends one sequential raw request-body chunk to an upload session.
    /// </summary>
    [HttpPut("upload-sessions/{uploadId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.FilesCreate)]
    public async Task<ActionResult<FileUploadSessionResponse>> AppendUpload(Guid uploadId, CancellationToken cancellationToken)
    {
        if (!Request.Headers.TryGetValue("X-Upload-Offset", out Microsoft.Extensions.Primitives.StringValues offsetValue) 
            || !long.TryParse(offsetValue.ToString(), out long offset)) return BadRequest(new { code = "UPLOAD_OFFSET_REQUIRED", title = "X-Upload-Offset is required." });

        return Ok(await service.AppendUploadAsync(uploadId, offset, Request.Body, Request.ContentLength, GetRequiredUserId(), cancellationToken));
    }

    /// <summary>
    /// Completes an upload session and creates its logical file record.
    /// </summary>
    [HttpPost("upload-sessions/{uploadId:guid}/complete")]
    [Authorize(Policy = AuthorizationPolicyNames.FilesCreate)]
    public async Task<ActionResult<StoredFileResponse>> CompleteUpload(Guid uploadId, CancellationToken cancellationToken) 
        => Ok(await service.CompleteUploadAsync(uploadId, GetRequiredUserId(), cancellationToken));

    /// <summary>
    /// Uploads a complete raw request body in one operation.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = AuthorizationPolicyNames.FilesCreate)]
    public async Task<ActionResult<StoredFileResponse>> Upload(CancellationToken cancellationToken)
    {
        if (!Request.Headers.TryGetValue("X-File-Name", out Microsoft.Extensions.Primitives.StringValues fileName) || string.IsNullOrWhiteSpace(fileName))
            return BadRequest(new { code = "FILE_NAME_REQUIRED", title = "X-File-Name is required." });

        long? expectedSize = null;

        if (Request.Headers.TryGetValue("X-File-Expected-Size", out Microsoft.Extensions.Primitives.StringValues expectedSizeValue))
        {
            if (!long.TryParse(expectedSizeValue.ToString(), out long parsedExpectedSize)) return BadRequest(new { code = "FILE_SIZE_INVALID", title = "X-File-Expected-Size is invalid." });
            
            expectedSize = parsedExpectedSize;
        }

        CreateFileUploadRequest request = new()
        {
            FileName = fileName.ToString(),
            ContentType = Request.ContentType,
            ExpectedSize = expectedSize
        };

        StoredFileResponse response = await service.UploadAsync(request, Request.Body, Request.ContentLength, GetRequiredUserId(), cancellationToken);

        return Created($"api/files/{response.Id}", response);
    }

    /// <summary>
    /// Gets logical file metadata.
    /// </summary>
    [HttpGet("{fileId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.FilesRead)]
    public async Task<ActionResult<StoredFileResponse>> Get(Guid fileId, CancellationToken cancellationToken)
    {
        StoredFileResponse? response = await service.GetAsync(fileId, cancellationToken);

        return response is null ? NotFound() : Ok(response);
    }

    /// <summary>
    /// Downloads the immutable physical content and supports HTTP range requests.
    /// </summary>
    [HttpGet("{fileId:guid}/content")]
    [Authorize(Policy = AuthorizationPolicyNames.FilesRead)]
    public async Task<IActionResult> Download(Guid fileId, CancellationToken cancellationToken)
    {
        (StoredFileResponse Metadata, Stream Content)? result = await service.OpenReadAsync(fileId, cancellationToken);

        if (result is null) return NotFound();

        return File(result.Value.Content, result.Value.Metadata.ContentType, result.Value.Metadata.OriginalFileName, enableRangeProcessing: true);
    }

    /// <summary>
    /// Queries file metadata using the universal query contract.
    /// </summary>
    [HttpPost("query")]
    [Authorize(Policy = AuthorizationPolicyNames.FilesRead)]
    public async Task<IActionResult> Query([FromBody] UniversalQueryRequest request, CancellationToken cancellationToken)
    {
        await using AppDbContext db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        return Ok(await db.Set<StoredFile>().AsNoTracking().ToPagedResponseAsync(request, Fields, x => new
        {
            x.Id,
            x.BlobId,
            x.OriginalFileName,
            x.ContentType,
            x.Extension,
            Size = x.Blob.Size,
            x.Blob.ContentHash,
            x.CreatedAt,
            x.CreatedBy
        }, cancellationToken));
    }

    /// <summary>
    /// Soft-deletes a logical file record.
    /// </summary>
    [HttpDelete("{fileId:guid}")]
    [Authorize(Policy = AuthorizationPolicyNames.FilesDelete)]
    public async Task<IActionResult> Delete(Guid fileId, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(fileId, GetRequiredUserId(), cancellationToken);

        return NoContent();
    }

    private Guid GetRequiredUserId()
    {
        string? value = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(value, out Guid userId)) throw new InvalidOperationException("The authenticated user identifier is missing.");

        return userId;
    }
}
