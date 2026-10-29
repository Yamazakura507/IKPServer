# HR domain architecture

The HR foundation is stored in `app` and `app_lookup`.

## Identity and employee

`Employee` is a business/person record and is not an authentication account. `Employee.UserId` is nullable and points to `auth.Users`. One authentication user may therefore be linked to multiple employee records.

## Organization and history

- `Departments` — hierarchical organization units with optional head employee.
- `Positions` and `Grades` — catalogs in `app_lookup`.
- `EmployeeEmploymentHistory` — immutable business history of department, position, grade, manager and effective dates. Dismissal is represented by a `Dismissal` record rather than overwriting the previous assignment.

## Personal data

`Employee` contains name, date of birth, hire date, optional user link and photo file link. Contacts, education and qualifications are separate records so they can have their own history/audit semantics.

## Competency matrix

The matrix is versioned:

`CompetencyMatrix -> CompetencyMatrixVersion -> Section -> Item -> Target(Position/Grade)`.

The two source sections are represented by `Professional` and `Personal`.

`CompetencyAssessment` always points to a concrete matrix version. Every assessment item stores a snapshot of the reference value/text together with manager and employee values. This prevents later matrix edits from changing historical analytics.

## Probation / development / attestation

- `ProbationPlan` + `ProbationTask` support the control sheet and three-month task/progress workflow.
- `DevelopmentPlan` supports `Confirmation` and `Promotion`; items support `Professional`, `Personal` and `Project` types.
- `Attestation` is generated from an immutable competency assessment and contains test, practical, oral, personal-development and project sections.
- `AttestationQuestion` explicitly supports AI drafts. AI-generated questions remain drafts until HR approves them.

## Email

`IEmailSender` is a reusable application abstraction. `SmtpEmailSender` is the infrastructure implementation and can later be used for contact verification, notifications, reports, password recovery and monitoring alerts. Email is disabled by default in configuration.

## Migration

The current execution environment does not contain the .NET SDK, so an EF Core migration was not generated here. After copying the changes into the development environment run:

```powershell
dotnet ef migrations add HrFoundation --project IKP.Infrastructure --startup-project IKP.Api
dotnet ef database update --project IKP.Infrastructure --startup-project IKP.Api
```

Before applying, inspect the generated migration, especially the foreign keys between `Employees`, `Departments`, and employment history.
