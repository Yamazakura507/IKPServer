# Authentication / RBAC — next stage

This stage builds on the authentication foundation and adds role-based authorization and administrator-only account management.

## Implemented

- `auth.Roles` — system and custom authorization roles.
- `auth.Permissions` — stable machine-readable permission definitions.
- `auth.UserRoles` — user-to-role assignments.
- `auth.RolePermissions` — role-to-permission assignments.
- System role `SYSTEM_ADMINISTRATOR`.
- System permissions for users, roles, permissions, audit, diagnostics and employees.
- JWT `role` and `permission` claims are issued from the current PostgreSQL RBAC state.
- Refresh sessions are revoked when user roles or role permissions change.
- Existing access tokens remain valid until their configured 10-minute expiry; newly issued access tokens receive the new authorization state.
- Bootstrap account is protected from deactivation and must retain `SYSTEM_ADMINISTRATOR`.
- Existing bootstrap accounts created before this RBAC migration are automatically repaired with the system administrator role when a new session is created after the RBAC migration is applied.
- Administrator-only user creation, profile update, activation/deactivation, password reset and role assignment.
- Administrator-only custom role creation/update and role-permission management.
- Existing employee, audit and diagnostics controllers are now protected by permissions.

## Authorization model

Authorization is permission-based. Roles are containers for permissions, and users receive roles.

The API uses policies named exactly like permission codes. A JWT contains one `permission` claim per effective permission and one `role` claim per active role.

PostgreSQL remains the authorization source of truth. JWTs are deliberately short-lived. Changes to roles or role permissions revoke refresh sessions so the changed authorization state is picked up on the next access-token issuance. An already issued access token can remain valid until its normal expiry.

## System administrator

`SYSTEM_ADMINISTRATOR` is system-defined and cannot be renamed, deactivated, or have its permissions modified through the administrator API.

The bootstrap account cannot be deactivated and cannot lose the `SYSTEM_ADMINISTRATOR` role.

## Migration

The uploaded project already contains the authentication migration. Generate the next migration from the updated model:

```powershell
dotnet ef migrations add AuthorizationAndAdministration --project IKP.Infrastructure --startup-project IKP.Api
```

Then inspect the generated migration and apply it:

```powershell
dotnet build

dotnet ef migrations list --project IKP.Infrastructure --startup-project IKP.Api

dotnet ef database update --project IKP.Infrastructure --startup-project IKP.Api
```

The new migration should create:

- `auth.Roles`
- `auth.Permissions`
- `auth.UserRoles`
- `auth.RolePermissions`

and seed the system role and system permissions.

## Smoke tests

After applying the migration, log in again so a fresh access token is issued.

### 1. Verify the bootstrap account

```sql
SELECT u."Id", u."UserName", u."IsBootstrapAccount", u."IsActive", r."Code" AS "RoleCode"
FROM auth."Users" u
LEFT JOIN auth."UserRoles" ur ON ur."UserId" = u."Id"
LEFT JOIN auth."Roles" r ON r."Id" = ur."RoleId"
WHERE u."IsBootstrapAccount" = TRUE;
```

The bootstrap account must have `SYSTEM_ADMINISTRATOR`.

### 2. Verify seeded authorization

```sql
SELECT "Code", "IsActive", "IsSystem"
FROM auth."Roles"
ORDER BY "SortOrder";

SELECT "Code", "IsActive", "IsSystem"
FROM auth."Permissions"
ORDER BY "SortOrder";
```

### 3. Get users

`GET /api/auth/admin/users`

Expected: `200` with the bootstrap user and `SYSTEM_ADMINISTRATOR`.

### 4. Get roles

`GET /api/auth/admin/roles`

Expected: `200` and the system administrator role with all seeded permissions.

### 5. Get permissions

`GET /api/auth/admin/permissions`

Expected: `200` with the seeded permission catalog.

### 6. Create a test user

`POST /api/auth/admin/users`

```json
{
  "userName": "operator",
  "email": "operator@example.invalid",
  "displayName": "Test Operator",
  "password": "A-long-unique-password-12",
  "roleIds": []
}
```

The initial password is marked `MustChangePassword = true`.

### 7. Assign roles

Use `PUT /api/auth/admin/users/{id}/roles` with a custom role or the system role during controlled testing. Production administrators should normally use custom roles with only the required permissions.

### 8. Authorization negative test

Create a custom role containing only `users.read`, assign it to the test user, log in as that user and verify:

- `GET /api/auth/admin/users` => `200`.
- `POST /api/auth/admin/users` => `403`.
- `PUT /api/auth/admin/users/{id}/status` => `403`.
- `GET /api/auth/admin/roles` => `403` unless `roles.read` is also granted.

### 9. Existing endpoints

A token without the corresponding permission must receive `403` from protected audit, diagnostics and employee endpoints.

### 10. Role-change invalidation

Change a user's roles. Existing refresh sessions are revoked. The user's current access token may remain usable until its normal expiry, but a refresh using a revoked refresh token must fail with `401`.
