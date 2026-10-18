# Files / Storage

## Purpose

The Files bounded context stores file metadata in PostgreSQL and physical file content outside PostgreSQL. It is the common storage foundation for future Support attachments, user avatars and icons, Updates, Reports, and other server-managed artifacts.

The API never exposes the physical storage path. Clients receive only logical file identifiers and API endpoints.

## Data model

### `files.FileBlobs`

Represents immutable physical content.

- `ContentHash`: SHA-256 hash of the complete content.
- `Size`: exact byte length.
- `StorageProvider`: provider code such as `LOCAL`.
- `StorageKey`: provider-specific immutable object key.
- `CreatedAt`: creation time.

A unique `(ContentHash, Size)` constraint provides content-level deduplication. Multiple logical files can reference the same physical blob.

### `files.Files`

Represents logical file metadata.

- original file name;
- content type;
- normalized extension;
- creator and audit metadata;
- soft-delete metadata;
- reference to `FileBlob`.

Deleting a logical file does not immediately delete the physical blob. This avoids breaking another logical reference to the same content and leaves physical garbage collection as an independent concern.

### `files.UploadSessions`

Tracks resumable sequential uploads and their temporary physical object.

Uploads are append-only and require the client to send the current `X-Upload-Offset`. The server rejects out-of-order writes.

Expired sessions are removed by a background worker together with their temporary physical object.

## Physical storage

The current provider is `LocalFileStorageProvider`. Its interface is independent of the local filesystem so the same application-level contracts can later be implemented by NAS, SAN, S3, or MinIO.

The local provider uses content-addressed immutable objects:

```text
<root>/objects/ab/cd/ABCDEF...
```

Temporary uploads are stored under:

```text
<root>/tmp/<guid>.upload
```

The production server should use a dedicated filesystem or mounted storage volume, for example `/srv/storage`. The storage root must not be placed inside the application deployment directory in production.

## File references for icons and other lookup metadata

`FullLookupLocalized` implements `IHasIcon`, which inherits `IHasFile`. Therefore every lookup entity derived from `FullLookupLocalized` exposes a nullable `FileId` representing its icon file.

The `FileId` columns are foreign keys to `files.Files.Id` with `ON DELETE SET NULL`. This keeps the lookup record valid if the logical file record is physically removed later. The relationship is intentionally configured without a domain navigation property so the lookup bounded contexts remain dependent only on the `IHasIcon`/`FileId` contract.

The relation is applied to the current lookup entities:

- `auth_lookup.AuthenticationMethods`
- `auth_lookup.Permissions`
- `auth_lookup.Roles`
- `diagnostics_lookup.DiagnosticAreas`
- `diagnostics_lookup.DiagnosticAreaResolutionRules`
- `diagnostics_lookup.DiagnosticAreaResolutionSources`
- `diagnostics_lookup.DiagnosticDefinitions`
- `diagnostics_lookup.DiagnosticDefinitionGroups`
- `diagnostics_lookup.DiagnosticStatisticsWorkItemStatuses`
- `app_logs_lookup.AuditActions`

The migration `20261018020000_AddLookupFileRelations` creates an index for every `FileId` and adds the corresponding foreign key constraints.

## Streaming and large files

The API does not bind the complete file into a DTO or byte array. Upload data is read directly from `HttpRequest.Body` into the configured storage provider with a bounded pooled buffer.

The default maximum file size is 5 GiB and can be overridden by configuration.

Download uses a streaming `FileStream` and HTTP range processing, so a large file is not loaded into application memory.

## API

### Create upload session

`POST /api/files/upload-sessions`

```json
{
  "fileName": "example.pdf",
  "contentType": "application/pdf",
  "expectedSize": 1048576
}
```

### Append upload

`PUT /api/files/upload-sessions/{id}`

Headers:

```text
X-Upload-Offset: 0
Content-Length: 1048576
```

The request body is the raw binary chunk.

### Complete upload

`POST /api/files/upload-sessions/{id}/complete`

The server calculates SHA-256 from the temporary object, performs content deduplication, moves new content to its immutable final key, creates the logical file record, and removes the upload session in one PostgreSQL transaction.

### Direct upload

`POST /api/files`

Headers:

```text
X-File-Name: example.pdf
Content-Type: application/pdf
X-File-Expected-Size: 1048576
```

The request body is the complete binary content. The endpoint internally uses the same upload-session and finalization pipeline.

### Metadata

`GET /api/files/{id}`

### Download

`GET /api/files/{id}/content`

Range requests are supported.

### Query

`POST /api/files/query`

Uses the existing Universal Query contract. Available fields include `id`, `blobId`, `originalFileName`, `contentType`, `extension`, `size`, `contentHash`, `createdAt`, and `createdBy`.

### Delete

`DELETE /api/files/{id}`

The operation is a logical soft delete.

## Authorization

The following system permissions were added:

- `files.read`
- `files.create`
- `files.delete`

The `SYSTEM_ADMINISTRATOR` role receives all three permissions through the migration. Custom roles can receive them through the existing RBAC administration API.

## Configuration

```json
"Files": {
  "Storage": {
    "RootPath": "./storage",
    "MaximumFileSizeBytes": 5368709120,
    "UploadSessionLifetimeMinutes": 120,
    "ProviderCode": "LOCAL",
    "CopyBufferSizeBytes": 1048576
  }
}
```

For the production VM, set `RootPath` to the dedicated storage mount, for example `/srv/storage`.

## Consistency model

PostgreSQL is the source of truth for file metadata. Redis is used only as optional coordination for concurrent uploads of identical content. A PostgreSQL advisory transaction lock based on content hash and size provides the authoritative serialization for deduplication even if Redis is unavailable.

Physical storage is finalized before the metadata transaction commits. If the transaction fails after a newly created physical object is moved, the service removes that object before rethrowing. Expired upload sessions are cleaned by a background worker.
