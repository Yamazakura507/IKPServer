# Support API architecture

## Scope

The Support bounded context manages authenticated-user support tickets, public conversation, internal support notes, assignment history, lookup values and references to logical files from the Files bounded context.

Support does not store physical file paths. Attachments reference `files.Files` through `support.TicketMessageAttachments`.

## Database schemas

- `support` — tickets, messages, assignments and attachment links.
- `support_lookup` — system statuses, priorities and categories.

## Main entities

### Tickets

`support.Tickets` stores the current ticket state. It uses a GUID primary key and a database-generated bigint `Number` for the human-facing ticket number.

Important relationships:

- creator → `auth.Users` (`RESTRICT`)
- current assignee → `auth.Users` (`SET NULL`)
- category → `support_lookup.TicketCategories` (`RESTRICT`)
- priority → `support_lookup.TicketPriorities` (`RESTRICT`)
- status → `support_lookup.TicketStatuses` (`RESTRICT`)

Tickets are soft-deletable.

### TicketMessages

`support.TicketMessages` contains public messages and internal notes. `IsInternal` separates the two visibility classes. Public messages are visible to normal Support readers; internal notes require the internal-notes read permission.

### TicketMessageAttachments

Links a message to an existing `files.Files` record. A message can reference up to 20 unique active logical files.

### TicketAssignments

Stores assignment history. Only one active assignment is permitted for a ticket at a time through a PostgreSQL partial unique index on `TicketId WHERE UnassignedAt IS NULL`.

## Built-in lookup values

Statuses:

- `NEW`
- `OPEN`
- `IN_PROGRESS`
- `WAITING_FOR_USER`
- `RESOLVED`
- `CLOSED`

Priorities:

- `LOW`
- `NORMAL`
- `HIGH`
- `CRITICAL`

Categories:

- `GENERAL`
- `TECHNICAL`
- `ACCESS`
- `FILES`
- `OTHER`

## API

- `POST /api/support/tickets/query`
- `GET /api/support/tickets/{ticketId}`
- `POST /api/support/tickets`
- `PUT /api/support/tickets/{ticketId}`
- `PUT /api/support/tickets/{ticketId}/status`
- `PUT /api/support/tickets/{ticketId}/assignment`
- `POST /api/support/tickets/{ticketId}/messages`
- `POST /api/support/tickets/{ticketId}/internal-notes`
- `DELETE /api/support/tickets/{ticketId}`
- `POST /api/support/tickets/{ticketId}/restore`
- `GET /api/support/lookups/statuses`
- `GET /api/support/lookups/priorities`
- `GET /api/support/lookups/categories`

Ticket listing uses the existing Universal Query engine.

## Authorization

- `support.read`
- `support.create`
- `support.update`
- `support.assign`
- `support.status.manage`
- `support.delete`
- `support.messages.create`
- `support.internal_notes.read`
- `support.internal_notes.create`

All Support permissions are granted to the built-in system administrator role by the Support authorization seed.

## Audit and diagnostics

Support entities implementing `IAuditable` and/or `ISoftDelete` automatically participate in the existing AuditSaveChangesInterceptor. Unhandled Support exceptions are returned through the existing error contract and registered by the existing Diagnostics pipeline.
