# Support smoke tests

Assume `$token` contains a valid access token with the seeded system administrator permissions.

## 1. Read lookups

```powershell
curl.exe -k -H "Authorization: Bearer $token" "https://localhost:7200/api/support/lookups/statuses"
curl.exe -k -H "Authorization: Bearer $token" "https://localhost:7200/api/support/lookups/priorities"
curl.exe -k -H "Authorization: Bearer $token" "https://localhost:7200/api/support/lookups/categories"
```

## 2. Create ticket

Use the IDs returned by the lookup endpoints.

```powershell
$body = @{
    subject = "Support smoke test"
    categoryId = "<category-guid>"
    priorityId = "<priority-guid>"
    message = "Initial support message."
    attachmentFileIds = @()
} | ConvertTo-Json

$ticket = Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets" `
    -Method Post `
    -Headers @{ Authorization = "Bearer $token" } `
    -ContentType "application/json" `
    -Body $body
```

Verify that `number` is generated and the ticket starts in `NEW`.

## 3. Read ticket

```powershell
Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/$($ticket.id)" `
    -Headers @{ Authorization = "Bearer $token" } |
    ConvertTo-Json -Depth 20
```

## 4. Add public message

```powershell
$body = @{ message = "Second support message."; attachmentFileIds = @() } | ConvertTo-Json
Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/$($ticket.id)/messages" `
    -Method Post `
    -Headers @{ Authorization = "Bearer $token" } `
    -ContentType "application/json" `
    -Body $body
```

## 5. Add internal note

```powershell
$body = @{ message = "Internal support note."; attachmentFileIds = @() } | ConvertTo-Json
Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/$($ticket.id)/internal-notes" `
    -Method Post `
    -Headers @{ Authorization = "Bearer $token" } `
    -ContentType "application/json" `
    -Body $body
```

A caller without `support.internal_notes.read` must not receive this note from GET ticket.

## 6. Universal Query

```powershell
$query = @{
    page = 1
    pageSize = 25
    sort = @(@{ field = "lastMessageAt"; descending = $true })
} | ConvertTo-Json -Depth 10

Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/query" `
    -Method Post `
    -Headers @{ Authorization = "Bearer $token" } `
    -ContentType "application/json" `
    -Body $query | ConvertTo-Json -Depth 20
```

## 7. Status change

Use the `RESOLVED` or `CLOSED` lookup ID.

```powershell
$body = @{ statusId = "<status-guid>" } | ConvertTo-Json
Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/$($ticket.id)/status" `
    -Method Put `
    -Headers @{ Authorization = "Bearer $token" } `
    -ContentType "application/json" `
    -Body $body
```

Verify `ResolvedAt` and `ClosedAt` are maintained according to the selected status.

## 8. Assignment

```powershell
$body = @{ assignedToUserId = "<user-guid>" } | ConvertTo-Json
Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/$($ticket.id)/assignment" `
    -Method Put `
    -Headers @{ Authorization = "Bearer $token" } `
    -ContentType "application/json" `
    -Body $body
```

Repeat with `assignedToUserId = $null` to unassign. The database must keep the previous assignment history and only one active assignment.

## 9. Attachment

Upload a file through Files first. Use the resulting logical file ID in `attachmentFileIds` when creating a ticket or adding a message.

## 10. Delete and restore

```powershell
Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/$($ticket.id)" `
    -Method Delete `
    -Headers @{ Authorization = "Bearer $token" }

Invoke-RestMethod `
    -Uri "https://localhost:7200/api/support/tickets/$($ticket.id)/restore" `
    -Method Post `
    -Headers @{ Authorization = "Bearer $token" }
```

The ticket is excluded from normal query results while soft-deleted.
