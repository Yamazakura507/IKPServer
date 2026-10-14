# Diagnostics smoke test

The following checks validate the complete diagnostic pipeline after applying EF migrations.

## 1. Generate the first diagnostic error

```powershell
curl.exe -i http://localhost:5204/api/diagnostics/test-unhandled
```

Expected HTTP status: `500`.

The response must contain `status: 500`, `code: INTERNAL_ERROR`, `requestId` and `correlationId`.

## 2. Generate the same error again

```powershell
curl.exe -i http://localhost:5204/api/diagnostics/test-unhandled
```

The second request must create another `ErrorOccurrence`, but it must reuse the same `ErrorGroup` when the fingerprint is unchanged.

## 3. Verify the diagnostic chain in DBeaver

```sql
SELECT "Id", "ApplicationId", "ExceptionType", "Fingerprint", "FingerprintVersion", "FirstOccurredAt", "LastOccurredAt"
FROM diagnostics_core."ErrorGroups"
ORDER BY "LastOccurredAt" DESC;

SELECT "Id", "ErrorGroupId", "OccurredAt", "CorrelationId", "UserId"
FROM diagnostics_core."ErrorOccurrences"
ORDER BY "OccurredAt" DESC;

SELECT "ErrorGroupId", "TotalOccurrences", "UniqueUsers", "OccurrencesLastHour", "OccurrencesLastDay", "OccurrencesLastWeek", "GrowthRate", "CalculatedAt"
FROM diagnostics_statistics."ErrorStatistics"
ORDER BY "CalculatedAt" DESC;

SELECT "ErrorGroupId", "BucketStart", "BucketSize", "OccurrenceCount", "UniqueUserCount"
FROM diagnostics_statistics."ErrorMetricBuckets"
ORDER BY "BucketStart" DESC;

SELECT "ErrorGroupId", "AutomaticScore", "ManualScore", "EffectiveScore", "CalculationVersion", "CalculatedAt"
FROM diagnostics_statistics."ErrorCriticalities"
ORDER BY "CalculatedAt" DESC;

SELECT ecf."ErrorCriticalityId", dd."Code", ecf."Score", ecf."Value", ecf."IsAvailable"
FROM diagnostics_statistics."ErrorCriticalityFactors" ecf
JOIN diagnostics_lookup."DiagnosticDefinitions" dd ON dd."Id" = ecf."DiagnosticDefinitionId"
ORDER BY ecf."ErrorCriticalityId", dd."Code";
```

Expected result for the newest group after two requests:

- one `ErrorGroup`;
- two `ErrorOccurrence` rows;
- one statistics row;
- metric buckets containing the occurrences;
- one criticality row after the background worker processes the group;
- criticality factors for the configured factor definitions.

## 4. Verify the area resolution

```sql
SELECT eg."Id" AS "ErrorGroupId",
       da."Code" AS "DiagnosticArea",
       dar."Confidence",
       dar."IsExplicit",
       dar."CalculatedAt"
FROM diagnostics_core."ErrorGroups" eg
LEFT JOIN diagnostics_core."DiagnosticAreaResolutions" dar ON dar."Id" = eg."DiagnosticAreaResolutionId"
LEFT JOIN diagnostics_lookup."DiagnosticAreas" da ON da."Id" = dar."DiagnosticAreaId"
ORDER BY eg."LastOccurredAt" DESC;
```

The test exception is expected to resolve according to the currently configured diagnostic-area rules. If no rule matches it, the area may legitimately be null.

## 5. Verify the universal query and multi-sort

```powershell
$body = @'
{
  "page": 1,
  "pageSize": 20,
  "sorts": [
    { "field": "applicationId", "descending": false },
    { "field": "lastOccurredAt", "descending": true }
  ],
  "search": {
    "text": "InvalidOperationException",
    "mode": "Contains",
    "caseSensitive": false,
    "ignoreWhitespace": false
  }
}
'@

curl.exe -X POST "http://localhost:5204/api/diagnostics/error-groups/query" -H "Content-Type: application/json" -d $body
```

The response must be paginated and contain the generated group.

## 6. Verify Levenshtein search

This requires the PostgreSQL `fuzzystrmatch` extension:

```sql
CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;
SELECT levenshtein('InvalidOperationException', 'InvalidOperatonException');
```

Then:

```powershell
$body = @'
{
  "page": 1,
  "pageSize": 20,
  "search": {
    "text": "InvalidOperatonException",
    "fields": [ "exceptionType" ],
    "mode": "Levenshtein",
    "caseSensitive": false,
    "ignoreWhitespace": false,
    "levenshteinMaxDistance": 2
  }
}
'@

curl.exe -X POST "http://localhost:5204/api/diagnostics/error-groups/query" -H "Content-Type: application/json" -d $body
```

The existing `System.InvalidOperationException` group should be returned when its edit distance is within the configured threshold.

## 7. Verify aggregation

```powershell
$body = @'
{
  "page": 1,
  "pageSize": 50,
  "aggregation": {
    "groupBy": "applicationId",
    "aggregates": [
      { "alias": "groups", "function": "Count", "field": "id" }
    ]
  }
}
'@

curl.exe -X POST "http://localhost:5204/api/diagnostics/error-groups/aggregate" -H "Content-Type: application/json" -d $body
```

The result must contain one aggregate row per application ID represented in `ErrorGroups`.

## Completion criterion

Diagnostics is considered complete when tests 1-4 pass and tests 5-7 pass if universal query, Levenshtein and aggregation are enabled in the current deployment.
