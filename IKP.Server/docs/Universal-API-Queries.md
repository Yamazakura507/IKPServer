# Universal API queries

Lists use `POST /query` so the client can send arbitrarily nested filters, multiple sort expressions, configurable text search and grouped aggregates without encoding a complex object into a query string.

## Search

Supported modes:

- `Contains`
- `Exact`
- `StartsWith`
- `EndsWith`
- `Levenshtein`

`CaseSensitive` selects case-sensitive comparison. `IgnoreWhitespace` removes ordinary spaces from the compared value and search text. For example, `вкусный хлеб` is treated as `вкусныйхлеб` for exact/Levenshtein comparison and as `%вкусный%хлеб%` for LIKE-style contains search.

`LevenshteinMaxDistance` controls the maximum allowed edit distance. The default is `2`; the API currently accepts `0..20`.

The Levenshtein mode is executed by PostgreSQL through the `fuzzystrmatch` extension and never falls back to in-memory filtering.

Enable it in PostgreSQL with:

```sql
CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;
```

The current EF migration also attempts to create the extension automatically. The database account must have permission to create extensions.

## Filtering and sorting

Filters support recursive `AND`/`OR` groups and:

`Equal`, `NotEqual`, `Contains`, `StartsWith`, `EndsWith`, `GreaterThan`, `GreaterThanOrEqual`, `LessThan`, `LessThanOrEqual`, `In`, `NotIn`, `IsNull`, `IsNotNull`, `Between`.

Sorting accepts an ordered list, so a request can sort by several columns in sequence.

Each controller supplies a whitelist of fields. Unknown fields are rejected rather than translated into arbitrary SQL/property access.

## Grouping and aggregates

A query can optionally contain `aggregation`:

```json
{
  "page": 1,
  "pageSize": 50,
  "aggregation": {
    "groupBy": "applicationId",
    "aggregates": [
      { "alias": "count", "function": "Count" },
      { "alias": "uniqueUsers", "function": "CountDistinct", "field": "userId" },
      { "alias": "totalOccurrences", "function": "Sum", "field": "occurrences" },
      { "alias": "averageOccurrences", "function": "Average", "field": "occurrences" },
      { "alias": "minimumOccurrences", "function": "Minimum", "field": "occurrences" },
      { "alias": "maximumOccurrences", "function": "Maximum", "field": "occurrences" }
    ]
  }
}
```

The current universal aggregation engine supports one grouping field and up to eight aggregates per request. `Sum`, `Average`, `Minimum` and `Maximum` require a numeric whitelisted field. Aggregation is executed by PostgreSQL.

## Audit logs

- `POST /api/logs/audit/query` — paged audit list.
- `GET /api/logs/audit/{id}` — one audit event with property changes.
- `POST /api/logs/audit/{id}/rollback` — guarded rollback.

Audit restore actions are now split into `RESTORE_UPDATE` and `RESTORE_DELETE`.

- Rolling back `UPDATE` changes the entity back to the old values and creates a `RESTORE_UPDATE` log with `RestorLogId` pointing to the original `UPDATE` log.
- Rolling back `DELETE` restores a soft-deleted entity and creates a `RESTORE_DELETE` log with `RestorLogId` pointing to the original `DELETE` log.
- An `UPDATE` or `DELETE` that already has a corresponding restore log cannot be rolled back again.
- Rolling back `RESTORE_UPDATE` reapplies the original update, refreshes the original log's `CreatedAt`, and removes the restore log.
- Rolling back `RESTORE_DELETE` reapplies the original delete, refreshes the original log's `CreatedAt`, and removes the restore log.
- Rolling back `CREATE` hard-deletes the created entity and removes its original audit log instead of converting it to a soft delete.

## Diagnostic controllers

Diagnostic read endpoints are split by responsibility. Lookup/reference controllers are under `Controllers/Diagnostics/Lookup`; operational diagnostic controllers are under `Controllers/Diagnostics`.
