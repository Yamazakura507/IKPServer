# Diagnostics API architecture

## Query contract

Diagnostic collection endpoints use:

- `page`: one-based page number, default `1`.
- `pageSize`: default `50`, maximum `250`.
- `search`: optional resource-specific text search.
- `sortBy`: resource-specific allow-listed field.
- `sortDescending`: descending order when `true`.

Every collection is projected directly in SQL into a compact response model before `Skip/Take`. Full EF entities are never returned by diagnostic collection endpoints.

The response contains:

- `page`
- `pageSize`
- `totalCount`
- `totalPages`
- `hasNextPage`
- `hasPreviousPage`
- `items`

## Read/write semantics

### Automatically generated data

The following are read-only:

- error groups
- error occurrences
- error statistics
- metric buckets
- criticality
- criticality factors
- diagnostic area resolutions
- resolution evidence

These records are produced by runtime diagnostics and background calculation. The API does not expose CRUD operations for them.

### Configurable diagnostic data

Diagnostic area resolution mappings remain configurable:

- create
- update
- delete
- activate
- deactivate

System mappings cannot be modified or deleted.

Diagnostic lookup definitions are currently exposed as read-only query endpoints. Their mutation API can be added when the administration model for lookup ownership/authorization is finalized.

## Grouping

Grouping is exposed where it has clear operational meaning rather than pretending that arbitrary grouping is valid for every resource.

`GET /api/diagnostics/error-groups/groups?groupBy=application`

Supported values:

- `application`
- `exceptionType`
- `errorCode`
- `severity`
- `status`

The same query infrastructure is intentionally shared by all collection endpoints; resource-specific filters, sorting and grouping remain explicit and allow-listed.

## Current diagnostic endpoints

- `/api/diagnostics/error-groups`
- `/api/diagnostics/error-groups/groups`
- `/api/diagnostics/error-occurrences`
- `/api/diagnostics/error-statistics`
- `/api/diagnostics/error-metric-buckets`
- `/api/diagnostics/error-criticality`
- `/api/diagnostics/areas`
- `/api/diagnostics/area-resolution-rules`
- `/api/diagnostics/area-resolution-sources`
- `/api/diagnostics/area-resolutions`
- `/api/diagnostics/area-resolution-evidence`
- `/api/diagnostics/resolution-mappings`
- `/api/diagnostics/definitions`
- `/api/diagnostics/definition-groups`

## Design boundary

The controller layer owns HTTP concerns: parsing query parameters, selecting an allow-listed sort mode and returning HTTP results.

The shared query extension owns pagination mechanics.

The domain/application layer remains the owner of diagnostic generation and automatic calculations. Automatic statistics and criticality must not be implemented in controllers.

A future application-level query service can move the EF query construction out of controllers without changing the HTTP contract.
