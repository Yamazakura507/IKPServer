namespace IKP.Domain.Common.Audit;

/// <summary>Defines audit action codes.</summary>
public static class AuditActionCodes
{
    /// <summary>Entity creation.</summary>
    public const string Create = "CREATE";
    /// <summary>Entity update.</summary>
    public const string Update = "UPDATE";
    /// <summary>Entity deletion.</summary>
    public const string Delete = "DELETE";
    /// <summary>Rollback of an update audit event.</summary>
    public const string RestoreUpdate = "RESTORE_UPDATE";
    /// <summary>Rollback or restoration of a delete audit event.</summary>
    public const string RestoreDelete = "RESTORE_DELETE";
}
