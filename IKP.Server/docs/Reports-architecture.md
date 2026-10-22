# Reports architecture

Reports are a separate bounded context.

## Database

```text
reports_lookup.ReportCategories
        |
        v
reports.ReportDefinitions
        |
        +---- reports.ReportTemplateVersions ---- files.Files
        |
        +---- reports.ReportParameters
        |
        +---- reports.ReportExecutions
```

Report templates are stored through the existing Files subsystem. PostgreSQL stores metadata only.

A report definition identifies the logical report. Template versions allow the FastReport template (or a future replacement renderer) to evolve without replacing the report identity.

Parameters are typed metadata. The server must validate parameter values before a report renderer receives them.

`ReportExecutions` is intentionally a lifecycle/history model. The actual generation worker and renderer are a later implementation step; no arbitrary SQL or template execution is exposed through the API in this stage.

## Security

The Reports context does not store arbitrary SQL in report definitions. Data providers will be implemented as server-side, allow-listed providers. This is important before any FastReport integration is enabled.

Permissions:

- `reports.read`
- `reports.manage`
- `reports.generate`

The system administrator receives all three through the authorization seed.
