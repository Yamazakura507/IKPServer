# System / Background

The system context introduces durable background job metadata without coupling the API to one concrete worker implementation.

`system.BackgroundJobs` stores job type, payload, status, retry count, schedule and lifecycle timestamps. Future workers can claim jobs using the existing PostgreSQL/Redis infrastructure. Existing diagnostics and file workers remain independently specialized.

This stage deliberately does not expose arbitrary code execution or arbitrary job types from clients. Job types must be registered by server code.
